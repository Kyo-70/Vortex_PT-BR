# Vortex em Português do Brasil

Projeto comunitário para manter a tradução PT-BR do Vortex organizada, fácil de instalar e atualizada junto com as mudanças da interface.

## O que está incluído

- Arquivos de idioma do Vortex em **resources/locales/pt-BR**.
- Arquivo **common.json** reservado às strings do Vortex.
- Tradução experimental da extensão **Modlist Backup** em **modlist-backup.json**, separada do idioma principal.
- Patch opcional para a extensão Modlist Backup.
- Scripts para validar os JSONs, gerar o pacote e instalar os arquivos localmente.

Créditos da tradução base: Rikintosh e PabloFub. Manutenção e organização deste repositório: Kyo-70.

## Instalação da tradução do Vortex

1. Baixe o arquivo ZIP mais recente em [Releases](https://github.com/Kyo-70/Vortex_PT-BR/releases).
2. Extraia o conteúdo.
3. Copie a pasta **pt-BR** para a pasta **resources/locales** da instalação do Vortex. O resultado deve ser **resources/locales/pt-BR/info.json**.
4. Reinicie o Vortex.
5. Abra as configurações de idioma e escolha **Português (Brasil)**.

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

## Manutenção

Consulte **CONTRIBUTING.md** para adicionar strings, preservar variáveis de substituição e validar alterações.

O pacote ZIP para instalar pelo Vortex é criado em **dist** por **scripts/build-package.bat**. No Windows, também é possível executar **scripts/install-local.bat** para copiar o idioma diretamente para a instalação local.

Ao publicar uma versão, atualize o campo version de **resources/locales/pt-BR/info.json** e envie uma tag no formato **v1.6.14**. O GitHub Actions valida os arquivos, cria os pacotes e publica os anexos da release.

## Origem

- [Repositório oficial do Vortex](https://github.com/Nexus-Mods/Vortex)
- [Pasta de idiomas em inglês do Vortex](https://github.com/Nexus-Mods/Vortex/tree/master/locales/en)
- [Repositório da extensão Vortex Games](https://github.com/ChemGuy1611/ChemBoy1-Vortex-Games)
