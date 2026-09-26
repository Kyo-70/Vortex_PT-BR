# Como contribuir

## Adicionar traduções do Vortex

1. Consulte as strings atuais do Vortex em **locales/en** no repositório oficial.
2. Coloque as traduções do programa em **resources/locales/pt-BR**.
3. Mantenha as strings do programa em seus arquivos originais. Use **common.json** somente para as strings do núcleo que pertencem a esse arquivo.
4. Preserve exatamente as variáveis usadas pelo Vortex, como **{{game}}**, **{{path}}** e **{{count}}**, além de tags como **[list]**, **[*]** e **[br][/br]**.
5. Não duplique chaves. Remova a chave repetida e mantenha uma única tradução correta.
6. Execute **python scripts/validate-locales.py** antes de enviar a alteração.

## Adicionar traduções de extensões

Não misture as strings de uma extensão em **common.json**. Crie um JSON com namespace próprio em **resources/locales/pt-BR**, por exemplo **minha-extensao.json**.

O código da extensão precisa carregar esse namespace antes de usar as traduções. Veja o exemplo do Modlist Backup em **patches/modlist-backup/index.js**. Uma extensão que não carrega um namespace separado não usará automaticamente qualquer arquivo JSON só por ele existir.

Faça um teste no Vortex antes de considerar uma tradução de extensão pronta. Extensões podem mudar o código e as chaves entre versões.

## Gerar e publicar uma versão

1. Atualize as traduções.
2. Atualize o número de versão em **resources/locales/pt-BR/info.json**.
3. Execute **scripts/build-package.bat** no Windows ou **python scripts/build_package.py**.
4. Confira os arquivos criados em **dist**.
5. Envie as alterações e crie uma tag no formato **v1.6.14**. O fluxo do GitHub valida novamente os JSONs e cria a release com os pacotes para download.
