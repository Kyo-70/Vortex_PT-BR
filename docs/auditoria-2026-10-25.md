# Atualização da tradução — 25/10/2026

## Referências

- Vortex oficial, `master`: [`cf009c405974332195f68a76d0fb386f1ecb5d98`](https://github.com/Nexus-Mods/Vortex/tree/cf009c405974332195f68a76d0fb386f1ecb5d98).
- Revisão oficial usada na auditoria anterior: [`ce5c0063e94b6ff795f2f03bdcceca78f7245e77`](https://github.com/Nexus-Mods/Vortex/tree/ce5c0063e94b6ff795f2f03bdcceca78f7245e77).
- Base deste repositório: versão `1.6.15`.

## Comparação

Os 11 catálogos em `locales/en` do Vortex foram comparados entre as duas revisões. As novas chaves estão em `extension_manager.json` (6), `mod_management.json` (14) e `health_check.json` (1). As duas chaves antigas removidas do catálogo inglês de Verificação de integridade continuam na tradução para preservar a compatibilidade com versões anteriores.

O diff do código em `src/` e `extensions/` foi analisado em busca de chamadas literais de `t(...)`, `translate(...)` e `laterT(...)` adicionadas nesse intervalo. Foram incluídas 122 entradas novas em `common.json`, além das 21 entradas estruturadas citadas acima. Quatro traduções existentes tiveram a redação revista. Chamadas que fazem referência a chaves de `collection.json` e a outras chaves estruturadas já existentes foram conferidas sem duplicá-las.

## Validação e limites

- Os JSONs são validados por `scripts/validate-locales.py`, que verifica sintaxe e chaves duplicadas.
- As novas traduções preservam as variáveis de interpolação `{{...}}` presentes nas mensagens de origem.
- O levantamento de chamadas literais no diff pode deixar de fora textos montados dinamicamente, concatenados de outras formas ou provenientes de extensões instaladas separadamente. Portanto, esta atualização não certifica cobertura de 100% da interface.
- A validação estática não substitui a revisão visual dentro do Vortex.
