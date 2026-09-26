# Vortex em Português do Brasil

Projeto comunitário para manter a tradução PT-BR do Vortex organizada, fácil de instalar e atualizada junto com as mudanças da interface.

## O que está incluído

- Arquivos de idioma do Vortex em **resources/locales/pt-BR**.
- Arquivo **common.json** reservado às strings do Vortex.
- Tradução experimental da extensão **Modlist Backup** em **modlist-backup.json**, separada do idioma principal.
- Patch opcional para a extensão Modlist Backup.
- Launcher gráfico para verificar atualizações e instalar a tradução no Windows.
- Scripts para validar os JSONs, gerar o pacote e instalar os arquivos localmente.

Créditos da tradução base: Rikintosh e PabloFub. Manutenção e organização deste repositório: Kyo-70.

## Instalação da tradução do Vortex

### Pelo launcher (recomendado)

1. Baixe **Vortex_PT-BR_Launcher.exe** em [Releases](https://github.com/Kyo-70/Vortex_PT-BR/releases).
2. Abra o launcher. Ele consulta a última versão, mostra a versão instalada e tenta localizar automaticamente o Vortex.
3. Se a pasta não for localizada, clique em **Localizar...** e selecione a pasta do Vortex ou **resources/locales**.
4. Clique em **Instalar / Atualizar**. Se já existir uma pasta **pt-BR**, ela será preservada em uma cópia de segurança.
5. Reinicie o Vortex e selecione **Português (Brasil)** nas configurações de idioma.

O launcher é uma janela gráfica, sem terminal. Ele precisa de conexão com a internet para consultar e baixar a versão mais recente. Se o Vortex estiver em uma pasta protegida do Windows, ele oferece a opção de reiniciar como administrador.

O patch experimental do **Modlist Backup** é opcional. Marque essa opção no launcher e selecione o **index.js** da extensão. O arquivo original será preservado em backup.

### Pelo ZIP (instalação manual)

Baixe **Vortex_PT-BR_*.zip** em [Releases](https://github.com/Kyo-70/Vortex_PT-BR/releases), extraia o conteúdo e copie a pasta **pt-BR** para **resources/locales** da instalação do Vortex. O resultado deve ser **resources/locales/pt-BR/info.json**.

Se estiver trabalhando a partir do código-fonte deste repositório, copie **resources/locales/pt-BR** para dentro de **Vortex/resources/locales**. A pasta **pt-BR** precisa ficar ao lado da pasta **en**.

O script **scripts/install-local.bat** também copia os arquivos e cria cópias de segurança dos JSONs que já existirem. Ele solicita a pasta **resources/locales** da instalação do Vortex.

## Tradução da extensão Modlist Backup

A extensão tem um namespace separado para que suas strings não sejam misturadas com as do Vortex:

- **resources/locales/pt-BR/modlist-backup.json**
- **patches/modlist-backup/index.js**

O patch altera o arquivo **index.js** da extensão para carregar esse namespace. Como o patch depende da estrutura e da versão da extensão, faça uma cópia de segurança do arquivo original antes de aplicá-lo. O instalador local oferece essa opção e guarda o original como **index.js.ptbr.bak**.

Para aplicar manualmente:

1. Copie **modlist-backup.json** para a pasta **pt-BR** dentro de **resources/locales** do Vortex.
2. Faça uma cópia do **index.js** original da extensão.
3. Substitua o **index.js** da extensão pelo arquivo deste repositório em **patches/modlist-backup/index.js**.
4. Reinicie o Vortex.

O suporte à extensão é experimental. Se uma atualização da extensão mudar seu código, reaplique o patch apenas depois de conferir as alterações com a nova versão.

## Estrutura do repositório

    resources/
      read-me.txt
      locales/
        pt-BR/
          common.json
          info.json
          modlist-backup.json
          ...
    patches/
      modlist-backup/
        index.js
    scripts/
      build-package.bat
      build_package.py
      install-local.bat
      validate-locales.py
    installer/
      Program.cs
      VortexPtBrLauncher.csproj
      app.manifest

## Manutenção

Consulte **CONTRIBUTING.md** para adicionar strings, preservar variáveis de substituição e validar alterações.

O pacote ZIP é criado em **dist** por **scripts/build-package.bat**. O GitHub Actions compila o launcher para Windows x64 e o anexa às releases como **Vortex_PT-BR_Launcher.exe**.

Ao publicar uma versão, atualize o campo **version** de **resources/locales/pt-BR/info.json** e envie uma tag no formato **v1.6.15**. O GitHub Actions valida os arquivos, cria os pacotes, publica a release e compila o launcher.

Para anexar o launcher a uma release que já existe, envie as alterações para **main** e execute **Actions > Validate and build translation > Run workflow**. Informe a tag no campo **release_tag** (por exemplo, **v1.6.14**). O workflow compila o launcher e o adiciona à release escolhida.

## Origem

- [Repositório oficial do Vortex](https://github.com/Nexus-Mods/Vortex)
- [Pasta de idiomas em inglês do Vortex](https://github.com/Nexus-Mods/Vortex/tree/master/locales/en)
- [Repositório da extensão Vortex Games](https://github.com/ChemGuy1611/ChemBoy1-Vortex-Games)
