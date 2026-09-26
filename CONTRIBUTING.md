# Como contribuir

## Adicionar traduções do Vortex

1. Consulte as strings atuais do Vortex em `locales/en` no repositório oficial.
2. Coloque cada tradução no arquivo JSON correspondente em `resources/locales/pt-BR`.
3. Mantenha as strings nos namespaces usados pelo Vortex. Use `common.json` para as strings principais que pertencem a esse arquivo.
4. Preserve exatamente variáveis como `{{game}}`, `{{path}}` e `{{count}}`, além de tags como `[list]`, `[*]` e `[br][/br]`.
5. Não duplique chaves. Remova a chave repetida e mantenha uma tradução correta.
6. Execute `python scripts/validate-locales.py` antes de enviar a alteração.

## Gerar e publicar uma versão

1. Atualize os arquivos de tradução.
2. Atualize o número de versão em `resources/locales/pt-BR/info.json`.
3. Execute `scripts/build-package.bat` no Windows ou `python scripts/build_package.py`.
4. Confira o ZIP criado em `dist`.
5. Envie as alterações e publique uma tag no formato `v1.6.15`. O GitHub Actions valida os JSONs, cria o ZIP e compila o launcher para a release.
