# Auditoria da tradução PT-BR do Vortex

Atualizada em 30/09/2026.

## Commits comparados

- Vortex oficial, branch `master`: [`ce5c0063e94b6ff795f2f03bdcceca78f7245e77`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/README.md).
- Repositório de tradução antes das adições: [`04e2d1dc88c1b4fce069ded669658ecc921989e3`](https://github.com/Kyo-70/Vortex_PT-BR/tree/04e2d1dc88c1b4fce069ded669658ecc921989e3).
- A branch de tradução já continha as 39 mensagens mapeadas no commit [`8167bcb`](https://github.com/Kyo-70/Vortex_PT-BR/commit/8167bcb82604dce312027e35fef23e5d6714ebb5); esta auditoria preserva essas alterações e registra suas origens.

## Onde ficam as mensagens traduzíveis

- Os 11 catálogos oficiais em inglês ficam em [`locales/en/`](https://github.com/Nexus-Mods/Vortex/tree/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/locales/en): `collection.json`, `common.json`, `download_management.json`, `extension_manager.json`, `gamebryo-plugin-management.json`, `gamemode_management.json`, `health_check.json`, `mod_management.json`, `nexus_integration.json`, `profile_management.json` e `support_bundle.json`.
- [`translation-strings.txt`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/translation-strings.txt) indexa chamadas e referências em código.
- Muitas mensagens são literais em chamadas `t(...)`, `api.translate(...)` e `laterT(...)` no renderer, processo principal, código compartilhado e extensões; `locales/en/common.json` é apenas um catálogo pequeno de padrões e plurais.
- Há catálogos adicionais em [`extensions/mod-dependency-manager/src/language.json`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/extensions/mod-dependency-manager/src/language.json) e [`src/renderer/src/extensions/collections/language.json`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/src/renderer/src/extensions/collections/language.json).
- Os arquivos instaláveis em PT-BR ficam em `resources/locales/pt-BR/` no repositório de tradução.

## 39 mensagens novas encontradas na comparação

Na comparação com `04e2d1dc88c1b4fce069ded669658ecc921989e3`, faltavam 12 entradas de Verificação de integridade, 14 mensagens do Vortex e 13 da extensão oficial de The Witcher 3. As 12 entradas estruturadas ficam em `health_check.json`; as outras 27 ficam em `common.json`, usado como fallback de namespace.

### Verificação de integridade — 12 entradas

| Chave do catálogo inglês e linha | Destino PT-BR |
| --- | --- |
| [`listing.section.warning.title`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/locales/en/health_check.json#L27) — L27 | `resources/locales/pt-BR/health_check.json` |
| [`listing.section.warning.description`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/locales/en/health_check.json#L28) — L28 | `resources/locales/pt-BR/health_check.json` |
| [`listing.section.suggestion.title`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/locales/en/health_check.json#L31) — L31 | `resources/locales/pt-BR/health_check.json` |
| [`listing.section.suggestion.description`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/locales/en/health_check.json#L32) — L32 | `resources/locales/pt-BR/health_check.json` |
| [`listing.section.collapse`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/locales/en/health_check.json#L34) — L34 | `resources/locales/pt-BR/health_check.json` |
| [`listing.section.expand`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/locales/en/health_check.json#L35) — L35 | `resources/locales/pt-BR/health_check.json` |
| [`author_notes_modal.title`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/locales/en/health_check.json#L127) — L127 | `resources/locales/pt-BR/health_check.json` |
| [`author_notes_modal.description`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/locales/en/health_check.json#L128) — L128 | `resources/locales/pt-BR/health_check.json` |
| [`author_notes_modal.prompt`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/locales/en/health_check.json#L129) — L129 | `resources/locales/pt-BR/health_check.json` |
| [`author_notes_modal.required_for`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/locales/en/health_check.json#L130) — L130 | `resources/locales/pt-BR/health_check.json` |
| [`author_notes_modal.buttons.cancel`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/locales/en/health_check.json#L132) — L132 | `resources/locales/pt-BR/health_check.json` |
| [`author_notes_modal.buttons.install`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/locales/en/health_check.json#L133) — L133 | `resources/locales/pt-BR/health_check.json` |

### Vortex e gerenciamento de plugins — 14 mensagens

| Mensagem localizada | Referência no código | Destino PT-BR |
| --- | --- | --- |
| (Required) | [`src/renderer/src/ui/components/form/field/Label.tsx:26`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/src/renderer/src/ui/components/form/field/Label.tsx#L26) | `resources/locales/pt-BR/common.json` |
| Category name | [`src/renderer/src/extensions/category_management/views/CategoryAddParent.tsx:60`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/src/renderer/src/extensions/category_management/views/CategoryAddParent.tsx#L60) | `resources/locales/pt-BR/common.json` |
| Failed to change the plugin's light flag | [`src/renderer/src/extensions/gamebryo_plugin_management/util/pluginLight.ts:37`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/src/renderer/src/extensions/gamebryo_plugin_management/util/pluginLight.ts#L37) | `resources/locales/pt-BR/common.json` |
| Files could not be deployed | [`src/renderer/src/extensions/mod_management/util/deploymentFailures.ts:78`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/src/renderer/src/extensions/mod_management/util/deploymentFailures.ts#L78) | `resources/locales/pt-BR/common.json` |
| Loading plugins | [`src/renderer/src/extensions/gamebryo_plugin_management/views/PluginList.tsx:646`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/src/renderer/src/extensions/gamebryo_plugin_management/views/PluginList.tsx#L646) | `resources/locales/pt-BR/common.json` |
| Only the first {{count}} are listed | [`src/renderer/src/extensions/mod_management/util/deploymentFailures.ts:89`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/src/renderer/src/extensions/mod_management/util/deploymentFailures.ts#L89) | `resources/locales/pt-BR/common.json` |
| Plugin file is in use | [`src/renderer/src/extensions/gamebryo_plugin_management/util/pluginLight.ts:31`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/src/renderer/src/extensions/gamebryo_plugin_management/util/pluginLight.ts#L31) | `resources/locales/pt-BR/common.json` |
| Some files could not be deployed | [`src/renderer/src/extensions/mod_management/util/deploymentFailures.ts:60`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/src/renderer/src/extensions/mod_management/util/deploymentFailures.ts#L60) | `resources/locales/pt-BR/common.json` |
| Files could not be written to the game directory | [`src/renderer/src/extensions/mod_management/util/deploymentFailures.ts:81`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/src/renderer/src/extensions/mod_management/util/deploymentFailures.ts#L81) | `resources/locales/pt-BR/common.json` |
| Waiting for mod changes to finish | [`src/renderer/src/extensions/gamebryo_plugin_management/views/PluginList.tsx:652`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/src/renderer/src/extensions/gamebryo_plugin_management/views/PluginList.tsx#L652) | `resources/locales/pt-BR/common.json` |
| Waiting for deployment to finish | [`src/renderer/src/extensions/gamebryo_plugin_management/views/PluginList.tsx:650`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/src/renderer/src/extensions/gamebryo_plugin_management/views/PluginList.tsx#L650) | `resources/locales/pt-BR/common.json` |
| LOOT could not load {{master}} | [`src/renderer/src/extensions/gamebryo_plugin_management/util/missingMasters.ts:387`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/src/renderer/src/extensions/gamebryo_plugin_management/util/missingMasters.ts#L387) | `resources/locales/pt-BR/common.json` |
| Enabled plugins depend on unavailable plugins | [`src/renderer/src/extensions/gamebryo_plugin_management/util/missingMasters.ts:361`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/src/renderer/src/extensions/gamebryo_plugin_management/util/missingMasters.ts#L361) | `resources/locales/pt-BR/common.json` |
| a plugin {{requirement}} | [`src/renderer/src/extensions/gamebryo_plugin_management/util/missingMasters.ts:392`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/src/renderer/src/extensions/gamebryo_plugin_management/util/missingMasters.ts#L392) | `resources/locales/pt-BR/common.json` |

### Extensão oficial de The Witcher 3 — 13 mensagens

| Mensagem localizada | Referência no código | Destino PT-BR |
| --- | --- | --- |
| Failed to change the game setting | [`extensions/games/game-witcher3/src/healthChecks.ts:214`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/extensions/games/game-witcher3/src/healthChecks.ts#L214) | `resources/locales/pt-BR/common.json` |
| Failed to sort alphabetically | [`extensions/games/game-witcher3/src/iconbarActions.ts:155`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/extensions/games/game-witcher3/src/iconbarActions.ts#L155) | `resources/locales/pt-BR/common.json` |
| Merged scripts not restored | [`extensions/games/game-witcher3/src/mergeBackup.ts:287`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/extensions/games/game-witcher3/src/mergeBackup.ts#L287) | `resources/locales/pt-BR/common.json` |
| Merged scripts were not restored | [`extensions/games/game-witcher3/src/mergeBackup.ts:277`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/extensions/games/game-witcher3/src/mergeBackup.ts#L277) | `resources/locales/pt-BR/common.json` |
| Run anyway | [`extensions/games/game-witcher3/src/eventHandlers.ts:266`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/extensions/games/game-witcher3/src/eventHandlers.ts#L266) | `resources/locales/pt-BR/common.json` |
| Script Merger and Remastered edition | [`extensions/games/game-witcher3/src/eventHandlers.ts:258`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/extensions/games/game-witcher3/src/eventHandlers.ts#L258) | `resources/locales/pt-BR/common.json` |
| Script Merger setup required again | [`extensions/games/game-witcher3/src/scriptmerger.ts:507`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/extensions/games/game-witcher3/src/scriptmerger.ts#L507) | `resources/locales/pt-BR/common.json` |
| Setting applied; restart the game | [`extensions/games/game-witcher3/src/healthChecks.ts:210`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/extensions/games/game-witcher3/src/healthChecks.ts#L210) | `resources/locales/pt-BR/common.json` |
| Remastered encoding guidance | [`extensions/games/game-witcher3/src/eventHandlers.ts:261`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/extensions/games/game-witcher3/src/eventHandlers.ts#L261) | `resources/locales/pt-BR/common.json` |
| Remastered edition detected | [`extensions/games/game-witcher3/src/eventHandlers.ts:52`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/extensions/games/game-witcher3/src/eventHandlers.ts#L52) | `resources/locales/pt-BR/common.json` |
| Merged scripts from a different edition | [`extensions/games/game-witcher3/src/mergeBackup.ts:290`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/extensions/games/game-witcher3/src/mergeBackup.ts#L290) | `resources/locales/pt-BR/common.json` |
| Alphabetical sort confirmation | [`extensions/games/game-witcher3/src/iconbarActions.ts:115`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/extensions/games/game-witcher3/src/iconbarActions.ts#L115) | `resources/locales/pt-BR/common.json` |
| Why Script Merger is still required | [`extensions/games/game-witcher3/src/util.ts:158`](https://github.com/Nexus-Mods/Vortex/blob/ce5c0063e94b6ff795f2f03bdcceca78f7245e77/extensions/games/game-witcher3/src/util.ts#L158) | `resources/locales/pt-BR/common.json` |

## Manutenção de qualidade

- Corrigidas duas traduções de `common.json` com conteúdo danificado ou variável indevida.
- Corrigidos trechos em espanhol e erros de redação em `collection.json`.
- Preservada a variável `{{ urlString }}` em `issue-tracker.json`.

## Pendências e limites

- A análise anterior encontrou 951 referências antigas sem correspondência direta nos catálogos consultados. São candidatas para revisão, não 951 erros confirmados: algumas podem pertencer a outro namespace, ser mensagens internas ou depender de dados definidos em tempo de execução.
- 138 chamadas com argumentos dinâmicos ficaram sem resolução estática.
- `Dialog.tsx` exibe `content.bbcode` sem tradução automática; o aviso Remastered iniciado em `eventHandlers.ts:59` não é alcançado apenas pelos arquivos JSON.
- A revisão foi estática; o Vortex não foi executado nesta sessão.
