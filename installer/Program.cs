using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Windows.Forms;

namespace VortexPtBrLauncher;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new LauncherForm());
    }
}

internal sealed class LauncherForm : Form
{
    private const string Repository = "Kyo-70/Vortex_PT-BR";
    private const string LatestReleaseApi = "https://api.github.com/repos/Kyo-70/Vortex_PT-BR/releases/latest";
    private static readonly HttpClient Client = CreateHttpClient();

    private readonly TextBox _localesPath = new();
    private readonly Label _installedVersion = new();
    private readonly Label _latestVersion = new();
    private readonly Label _status = new();
    private readonly ProgressBar _progress = new();
    private readonly Button _checkButton = new();
    private readonly Button _installButton = new();

    private ReleaseInfo? _release;
    private bool _busy;

    public LauncherForm()
    {
        Text = "Vortex PT-BR | Atualizador";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = true;
        ClientSize = new Size(690, 386);
        Font = new Font("Segoe UI", 9F);

        var heading = new Label
        {
            Text = "Vortex em Português do Brasil",
            Font = new Font("Segoe UI", 17F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(22, 18)
        };
        Controls.Add(heading);

        var subtitle = new Label
        {
            Text = "Consulte a versão mais recente e instale ou atualize o idioma com segurança.",
            AutoSize = true,
            Location = new Point(24, 55)
        };
        Controls.Add(subtitle);

        var versionGroup = new GroupBox
        {
            Text = "Versões",
            Location = new Point(22, 88),
            Size = new Size(646, 82)
        };
        _installedVersion.Location = new Point(16, 25);
        _installedVersion.AutoSize = true;
        _installedVersion.Text = "Instalada: não verificada";
        _latestVersion.Location = new Point(16, 50);
        _latestVersion.AutoSize = true;
        _latestVersion.Text = "Disponível: consultando...";
        versionGroup.Controls.Add(_installedVersion);
        versionGroup.Controls.Add(_latestVersion);
        Controls.Add(versionGroup);

        var destinationGroup = new GroupBox
        {
            Text = "Instalação do Vortex",
            Location = new Point(22, 181),
            Size = new Size(646, 96)
        };
        var destinationLabel = new Label
        {
            Text = "Pasta resources\\locales (localizada automaticamente quando possível):",
            AutoSize = true,
            Location = new Point(14, 23)
        };
        _localesPath.Location = new Point(17, 49);
        _localesPath.Size = new Size(497, 25);
        _localesPath.ReadOnly = true;
        var browseVortexButton = new Button
        {
            Text = "Localizar...",
            Location = new Point(523, 47),
            Size = new Size(104, 29)
        };
        browseVortexButton.Click += (_, _) => BrowseVortexFolder();
        destinationGroup.Controls.Add(destinationLabel);
        destinationGroup.Controls.Add(_localesPath);
        destinationGroup.Controls.Add(browseVortexButton);
        Controls.Add(destinationGroup);

        _status.Location = new Point(24, 294);
        _status.Size = new Size(642, 20);
        _status.AutoEllipsis = true;
        _status.Text = "Pronto para verificar atualizações.";
        Controls.Add(_status);

        _progress.Location = new Point(22, 320);
        _progress.Size = new Size(646, 15);
        _progress.Style = ProgressBarStyle.Continuous;
        Controls.Add(_progress);

        _checkButton.Text = "Verificar atualizações";
        _checkButton.Location = new Point(22, 346);
        _checkButton.Size = new Size(180, 30);
        _checkButton.Click += async (_, _) => await CheckForUpdatesAsync();
        Controls.Add(_checkButton);

        _installButton.Text = "Instalar / Atualizar";
        _installButton.Location = new Point(488, 346);
        _installButton.Size = new Size(180, 30);
        _installButton.Enabled = false;
        _installButton.Click += async (_, _) => await InstallLatestAsync();
        Controls.Add(_installButton);

        var releaseLink = new LinkLabel
        {
            Text = "Abrir página de releases",
            AutoSize = true,
            Location = new Point(258, 354)
        };
        releaseLink.LinkClicked += (_, _) => OpenReleasePage();
        Controls.Add(releaseLink);

        Shown += async (_, _) =>
        {
            DetectVortexInstallation();
            RefreshInstalledVersion();
            await CheckForUpdatesAsync();
        };
    }

    private static HttpClient CreateHttpClient()
    {
        var client = new HttpClient { Timeout = TimeSpan.FromMinutes(3) };
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Vortex-PT-BR-Launcher", "1.0"));
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
        return client;
    }

    private async Task CheckForUpdatesAsync()
    {
        if (_busy) return;
        SetBusy(true);
        SetStatus("Consultando a última versão no GitHub...");
        try
        {
            using var response = await Client.GetAsync(LatestReleaseApi);
            response.EnsureSuccessStatusCode();
            using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var root = document.RootElement;
            var tag = root.GetProperty("tag_name").GetString() ?? throw new InvalidDataException("A release não informa uma tag.");
            var assets = root.GetProperty("assets").EnumerateArray().ToArray();
            var localeAsset = assets.FirstOrDefault(asset =>
            {
                var name = asset.GetProperty("name").GetString() ?? "";
                return name.StartsWith("Vortex_PT-BR_", StringComparison.OrdinalIgnoreCase)
                    && name.EndsWith(".zip", StringComparison.OrdinalIgnoreCase);
            });
            if (localeAsset.ValueKind == JsonValueKind.Undefined)
                throw new InvalidDataException("A última release ainda não contém o pacote ZIP do idioma.");

            var version = tag.TrimStart('v', 'V');
            var zipUrl = localeAsset.GetProperty("browser_download_url").GetString()
                ?? throw new InvalidDataException("Não foi encontrado o link do pacote ZIP.");
            _release = new ReleaseInfo(version, zipUrl, root.GetProperty("html_url").GetString());
            _latestVersion.Text = $"Disponível: {_release.Version}";
            RefreshInstalledVersion();
            var installed = ReadInstalledVersion();
            if (installed is not null && CompareVersions(installed, _release.Version) >= 0)
            {
                SetStatus($"Você já tem a versão {_release.Version} ou uma versão mais recente.");
                _installButton.Text = "Reinstalar versão";
            }
            else
            {
                SetStatus($"A versão {_release.Version} está pronta para instalar.");
                _installButton.Text = installed is null ? "Instalar tradução" : "Atualizar tradução";
            }
            _installButton.Enabled = !string.IsNullOrWhiteSpace(_localesPath.Text);
        }
        catch (Exception ex)
        {
            _release = null;
            _latestVersion.Text = "Disponível: não foi possível consultar";
            _installButton.Enabled = false;
            SetStatus($"Falha ao consultar o GitHub: {ex.Message}");
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async Task InstallLatestAsync()
    {
        if (_busy) return;
        if (_release is null)
        {
            await CheckForUpdatesAsync();
            if (_release is null) return;
        }

        var localePath = VortexLocator.ResolveLocalesPath(_localesPath.Text);
        if (localePath is null)
        {
            MessageBox.Show(this,
                "Não encontrei a pasta resources\\locales\\en. Use Localizar... e selecione a pasta do Vortex ou resources\\locales.",
                "Pasta do Vortex não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        _localesPath.Text = localePath;

        if (IsVortexRunning())
        {
            var close = MessageBox.Show(this,
                "Feche o Vortex antes de instalar. Depois de fechá-lo, escolha Sim para continuar.",
                "Feche o Vortex", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (close != DialogResult.Yes || IsVortexRunning()) return;
        }

        var confirm = MessageBox.Show(this,
            $"Instalar a tradução PT-BR {_release.Version} em:\r\n{localePath}\r\n\r\n" +
            "A pasta pt-BR atual será preservada em uma cópia de segurança.\r\n\r\nContinuar?",
            "Confirmar instalação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirm != DialogResult.Yes) return;

        SetBusy(true);
        _progress.Value = 0;
        try
        {
            SetStatus("Baixando os arquivos da tradução...");
            var archiveBytes = await DownloadAsync(_release.LocaleZipUrl);
            SetStatus("Instalando os arquivos do Vortex...");
            var installedVersion = await Task.Run(() => InstallLocaleArchive(archiveBytes, localePath));

            RefreshInstalledVersion();
            _latestVersion.Text = $"Disponível: {_release.Version}";
            SetStatus($"Instalação da versão {installedVersion} concluída. Reinicie o Vortex.");
            MessageBox.Show(this, $"Tradução PT-BR {installedVersion} instalada.\r\n\r\nReinicie o Vortex para carregar o idioma.",
                "Instalação concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            SetStatus($"Não foi possível concluir a instalação: {ex.Message}");
            var elevate = IsAccessDenied(ex);
            if (elevate && MessageBox.Show(this,
                    "O Windows bloqueou a gravação na pasta do Vortex. Deseja reabrir o launcher como administrador?",
                    "Permissão necessária", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                RelaunchAsAdministrator();
            else
                MessageBox.Show(this, ex.Message, "Falha na instalação", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _progress.Value = 0;
            SetBusy(false);
        }
    }

    private async Task<byte[]> DownloadAsync(string url)
    {
        using var response = await Client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();
        var total = response.Content.Headers.ContentLength;
        await using var input = await response.Content.ReadAsStreamAsync();
        using var output = new MemoryStream();
        var buffer = new byte[64 * 1024];
        long readTotal = 0;
        int read;
        while ((read = await input.ReadAsync(buffer.AsMemory(), CancellationToken.None)) > 0)
        {
            await output.WriteAsync(buffer.AsMemory(0, read));
            readTotal += read;
            if (total is > 0)
                _progress.Value = Math.Clamp((int)(readTotal * 100 / total.Value), 0, 100);
        }
        return output.ToArray();
    }

    private static string InstallLocaleArchive(byte[] bytes, string localesPath)
    {
        using var input = new MemoryStream(bytes);
        using var archive = new ZipArchive(input, ZipArchiveMode.Read);
        const string prefix = "pt-BR/";
        var localeEntries = archive.Entries
            .Where(entry => entry.FullName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
                && !entry.FullName.EndsWith("/", StringComparison.Ordinal))
            .ToArray();
        if (localeEntries.Length == 0)
            throw new InvalidDataException("O pacote baixado não contém a pasta pt-BR.");

        // Stage on the same volume as resources/locales so Directory.Move remains valid
        // even when Windows TEMP is on another drive from the Vortex installation.
        var tempRoot = Path.Combine(localesPath, ".Vortex_PT-BR_" + Guid.NewGuid().ToString("N"));
        var stagedLocale = Path.Combine(tempRoot, "pt-BR");
        string? version = null;
        try
        {
            Directory.CreateDirectory(stagedLocale);
            foreach (var entry in localeEntries)
            {
                var relative = entry.FullName[prefix.Length..].Replace('/', Path.DirectorySeparatorChar);
                var destination = Path.GetFullPath(Path.Combine(stagedLocale, relative));
                if (!destination.StartsWith(Path.GetFullPath(stagedLocale) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidDataException("O pacote contém um caminho inválido.");
                Directory.CreateDirectory(Path.GetDirectoryName(destination)!);
                entry.ExtractToFile(destination, overwrite: true);
                if (string.Equals(Path.GetFileName(destination), "info.json", StringComparison.OrdinalIgnoreCase))
                {
                    using var json = JsonDocument.Parse(File.ReadAllText(destination));
                    version = json.RootElement.GetProperty("version").GetString();
                }
            }
            if (string.IsNullOrWhiteSpace(version))
                throw new InvalidDataException("O pacote não contém uma versão válida em info.json.");

            var target = Path.Combine(localesPath, "pt-BR");
            var backup = Directory.Exists(target) ? GetBackupPath(target, "pt-BR.backup-") : null;
            if (backup is not null) Directory.Move(target, backup);
            try
            {
                Directory.Move(stagedLocale, target);
            }
            catch
            {
                if (backup is not null && !Directory.Exists(target)) Directory.Move(backup, target);
                throw;
            }
            return version;
        }
        finally
        {
            if (Directory.Exists(tempRoot)) Directory.Delete(tempRoot, recursive: true);
        }
    }

    private void DetectVortexInstallation()
    {
        var found = VortexLocator.FindLocalesPath();
        _localesPath.Text = found ?? "";
        if (found is null)
            SetStatus("Não localizei o Vortex automaticamente. Use Localizar... para escolher a pasta.");
        else
            SetStatus($"Vortex encontrado em: {found}");
        _installButton.Enabled = _release is not null && found is not null;
    }

    private void BrowseVortexFolder()
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Selecione a pasta do Vortex ou a pasta resources\\locales.",
            UseDescriptionForTitle = true,
            ShowNewFolderButton = false,
            SelectedPath = Directory.Exists(_localesPath.Text) ? _localesPath.Text : ""
        };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        var resolved = VortexLocator.ResolveLocalesPath(dialog.SelectedPath);
        if (resolved is null)
        {
            MessageBox.Show(this, "Não encontrei uma subpasta en no local selecionado. Escolha a pasta do Vortex ou resources\\locales.",
                "Pasta inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        _localesPath.Text = resolved;
        RefreshInstalledVersion();
        _installButton.Enabled = _release is not null;
        SetStatus($"Pasta do Vortex selecionada: {resolved}");
    }

    private void RefreshInstalledVersion()
    {
        var version = ReadInstalledVersion();
        _installedVersion.Text = version is null ? "Instalada: não encontrada" : $"Instalada: {version}";
    }

    private string? ReadInstalledVersion()
    {
        var localePath = VortexLocator.ResolveLocalesPath(_localesPath.Text);
        if (localePath is null) return null;
        var info = Path.Combine(localePath, "pt-BR", "info.json");
        if (!File.Exists(info)) return null;
        try
        {
            using var json = JsonDocument.Parse(File.ReadAllText(info));
            return json.RootElement.GetProperty("version").GetString();
        }
        catch
        {
            return null;
        }
    }

    private void OpenReleasePage()
    {
        var url = _release?.ReleasePageUrl ?? "https://github.com/" + Repository + "/releases";
        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
    }

    private static bool IsVortexRunning() => Process.GetProcessesByName("Vortex").Length > 0;

    private static bool IsAccessDenied(Exception exception)
    {
        if (exception is UnauthorizedAccessException) return true;
        return exception is IOException io && (io.HResult & 0xFFFF) == 5;
    }

    private void RelaunchAsAdministrator()
    {
        try
        {
            Process.Start(new ProcessStartInfo(Application.ExecutablePath) { UseShellExecute = true, Verb = "runas" });
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Não foi possível reabrir como administrador: {ex.Message}", "Permissão necessária",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SetBusy(bool busy)
    {
        _busy = busy;
        _checkButton.Enabled = !busy;
        _installButton.Enabled = !busy && _release is not null && !string.IsNullOrWhiteSpace(_localesPath.Text);
        UseWaitCursor = busy;
    }

    private void SetStatus(string value) => _status.Text = value;

    private static int CompareVersions(string left, string right)
    {
        return Version.TryParse(left, out var leftVersion) && Version.TryParse(right, out var rightVersion)
            ? leftVersion.CompareTo(rightVersion)
            : string.Compare(left, right, StringComparison.OrdinalIgnoreCase);
    }

    private static string GetBackupPath(string original, string prefix)
    {
        var parent = Path.GetDirectoryName(original)!;
        var stamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");
        var backup = Path.Combine(parent, prefix + stamp);
        var suffix = 1;
        while (File.Exists(backup) || Directory.Exists(backup))
            backup = Path.Combine(parent, prefix + stamp + "-" + suffix++);
        return backup;
    }
}

internal sealed record ReleaseInfo(string Version, string LocaleZipUrl, string? ReleasePageUrl);

internal static class VortexLocator
{
    public static string? FindLocalesPath()
    {
        foreach (var candidate in GetInstallCandidates())
        {
            var resolved = ResolveLocalesPath(candidate);
            if (resolved is not null) return resolved;
        }
        return null;
    }

    public static string? ResolveLocalesPath(string? candidate)
    {
        if (string.IsNullOrWhiteSpace(candidate)) return null;
        var path = Environment.ExpandEnvironmentVariables(candidate.Trim().Trim('"'));
        if (File.Exists(path)) path = Path.GetDirectoryName(path) ?? path;
        if (!Directory.Exists(path)) return null;

        var possibilities = new[]
        {
            path,
            Path.Combine(path, "resources", "locales"),
            Path.Combine(path, "locales")
        };
        foreach (var possibility in possibilities)
            if (Directory.Exists(Path.Combine(possibility, "en"))) return Path.GetFullPath(possibility);
        return null;
    }

    private static IEnumerable<string> GetInstallCandidates()
    {
        var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        var programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        var fixedPaths = new[]
        {
            Path.Combine(local, "Programs", "Vortex"),
            Path.Combine(local, "Programs", "Black Tree Gaming Ltd", "Vortex"),
            Path.Combine(programFiles, "Black Tree Gaming Ltd", "Vortex"),
            Path.Combine(programFilesX86, "Black Tree Gaming Ltd", "Vortex")
        };
        foreach (var path in fixedPaths) yield return path;

        foreach (var root in new[] { RegistryHive.CurrentUser, RegistryHive.LocalMachine })
        foreach (var view in new[] { RegistryView.Registry64, RegistryView.Registry32 })
        {
            RegistryKey baseKey;
            try { baseKey = RegistryKey.OpenBaseKey(root, view); }
            catch { continue; }
            using (baseKey)
            using (var uninstall = baseKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
            {
                if (uninstall is null) continue;
                foreach (var subKeyName in uninstall.GetSubKeyNames())
                {
                    using var app = uninstall.OpenSubKey(subKeyName);
                    if (app is null) continue;
                    var displayName = app.GetValue("DisplayName") as string;
                    if (displayName is null || !displayName.Contains("Vortex", StringComparison.OrdinalIgnoreCase)) continue;
                    var installLocation = app.GetValue("InstallLocation") as string;
                    if (!string.IsNullOrWhiteSpace(installLocation)) yield return installLocation;
                    foreach (var valueName in new[] { "DisplayIcon", "UninstallString" })
                    {
                        var executable = ExtractExecutablePath(app.GetValue(valueName) as string);
                        if (executable is not null) yield return executable;
                    }
                }
            }
        }
    }

    private static string? ExtractExecutablePath(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        var match = Regex.Match(value, @"^\s*""(?<path>[^""]+\.exe)""", RegexOptions.IgnoreCase);
        if (!match.Success) match = Regex.Match(value, @"^\s*(?<path>[^,]+?\.exe)(?:,\d+)?(?:\s|$)", RegexOptions.IgnoreCase);
        return match.Success ? match.Groups["path"].Value : null;
    }
}
