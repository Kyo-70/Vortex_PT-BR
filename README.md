# Vortex em Português do Brasil

Projeto comunitário para manter a tradução PT-BR do Vortex organizada, simples de instalar e atualizada com as mudanças da interface.

## O que está incluído

- Arquivos de idioma do Vortex em `resources/locales/pt-BR`.
- Traduções mantidas nos arquivos JSON e namespaces correspondentes do Vortex; `common.json` contém as strings principais.
- Launcher gráfico para localizar o Vortex, verificar atualizações e instalar a tradução no Windows.
- Scripts para validar os JSONs, gerar o pacote e instalar os arquivos localmente.

Créditos da tradução base: Rikintosh e PabloFub. Manutenção deste repositório: Kyo-70.

## Instalar pelo launcher

1. Baixe `Vortex_PT-BR_Launcher.exe` em [Releases](https://github.com/Kyo-70/Vortex_PT-BR/releases).
2. Abra o launcher. Ele verifica a versão disponível e tenta localizar automaticamente a instalação do Vortex.
3. Se necessário, clique em **Localizar...** e selecione a pasta do Vortex ou `resources/locales`.
4. Clique em **Instalar / Atualizar**. Se já existir uma pasta `pt-BR`, o launcher guarda uma cópia antes de substituí-la.
5. Reinicie o Vortex e selecione **Português (Brasil)** nas configurações de idioma.

O launcher é uma janela gráfica, sem terminal. Ele precisa de internet para consultar a release e baixar o pacote. Se o Vortex estiver em uma pasta protegida do Windows, o launcher pode solicitar permissão de administrador.

## Instalar pelo ZIP

Baixe `Vortex_PT-BR_*.zip` em [Releases](https://github.com/Kyo-70/Vortex_PT-BR/releases), extraia o conteúdo e copie a pasta `pt-BR` para `resources/locales` da instalação do Vortex. O arquivo `info.json` deve ficar em `resources/locales/pt-BR/info.json`, ao lado da pasta `en`.

O script `scripts/install-local.bat` também copia os arquivos e cria cópias de segurança dos JSONs que já existirem.

## Estrutura

```text
resources/
  read-me.txt
  locales/
    pt-BR/
      common.json
      info.json
      ...
scripts/
  build-package.bat
  build_package.py
  install-local.bat
  validate-locales.py
installer/
  Program.cs
  VortexPtBrLauncher.csproj
  app.manifest
  vortex-ptbr.ico
```

## Manutenção e releases

Consulte `CONTRIBUTING.md` para adicionar traduções, preservar variáveis e validar as alterações.

O pacote ZIP é criado por `scripts/build-package.bat` ou `python scripts/build_package.py`. Cada release contém dois anexos: o ZIP da tradução e `Vortex_PT-BR_Launcher.exe`. O GitHub também exibe automaticamente links separados para baixar o código-fonte.

Para publicar uma nova versão, atualize `version` em `resources/locales/pt-BR/info.json`, envie as alterações e crie uma tag como `v1.6.15`. Para atualizar uma tag existente, execute **Actions > Validate and build translation > Run workflow** e informe a tag em `release_tag`.

## Origem

- [Repositório oficial do Vortex](https://github.com/Nexus-Mods/Vortex)
- [Arquivos de idioma em inglês do Vortex](https://github.com/Nexus-Mods/Vortex/tree/master/locales/en)
