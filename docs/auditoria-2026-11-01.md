# Atualização da tradução — 01/11/2026

## Referências

- Vortex oficial, `master`: [`9482caf996f16eb51c5e0664349fefc5bc072327`](https://github.com/Nexus-Mods/Vortex/tree/9482caf996f16eb51c5e0664349fefc5bc072327).
- Revisão oficial da auditoria anterior: [`cf009c405974332195f68a76d0fb386f1ecb5d98`](https://github.com/Nexus-Mods/Vortex/tree/cf009c405974332195f68a76d0fb386f1ecb5d98).
- Repositório PT-BR antes desta atualização: `738450d469a6e56e1344203f673050a669e14336` (`v1.6.16`).
- Instalação local consultada: Vortex `2.9.1`, em `C:\Modding\Vortex\resources\locales\pt-BR`.

## Comparação

Os 11 catálogos em `locales/en` do Vortex foram comparados por caminho de chave com os arquivos PT-BR. A única diferença de cobertura estava em `mod_management.json`: 15 valores do novo `manualInstaller`. Os demais catálogos ingleses já tinham todas as chaves correspondentes no repositório PT-BR.

O diff de `src/` entre as duas revisões oficiais foi analisado em busca de novas chamadas literais de `t(...)` e `translate(...)`. Foram cobertas 51 mensagens novas em `common.json`: 45 traduções redigidas nesta atualização e seis recuperadas da instalação local. Isso inclui as mensagens completas concatenadas de erro do LOOT e o título do aviso de conteúdo adulto. As chamadas estruturadas `collection:browse.*` já estavam cobertas em `collection.json`; a função `collectionT` usa os separadores de namespace próprios dessas chaves.

A instalação local tinha somente 11 arquivos PT-BR, mas continha 2.066 valores que não estavam no repositório. Eles foram incorporados sem substituir traduções já existentes; o arquivo `settings_workarounds.json` foi recuperado. O pacote passa a conter 53 arquivos de idioma. A instalação local é da linha estável 2.9.1; algumas novas mensagens do `master` podem aparecer somente em versões futuras do Vortex.

## Validação e limites

- `python scripts/validate-locales.py`: sintaxe JSON, chaves duplicadas e metadados da tradução.
- Comparação por caminho de chave: nenhuma chave ausente nos 11 catálogos ingleses da revisão oficial acima.
- As variáveis `{{...}}` das entradas recuperadas e das novas mensagens foram conferidas contra as chaves inglesas ou as mensagens de origem.
- A análise estática do diff não cobre textos montados dinamicamente fora das chamadas literais examinadas nem extensões distribuídas separadamente.
- A revisão visual e funcional da tradução dentro do Vortex não foi executada nesta auditoria.
