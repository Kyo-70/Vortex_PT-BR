module.exports =
/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = "./src/index.ts");
/******/ })
/************************************************************************/
/******/ ({

/***/ "./src/getSafe.ts":
/*!************************!*\
  !*** ./src/getSafe.ts ***!
  \************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
function getSafe(state, path, fallback) {
    let current = state;
    for (const segment of path) {
        if ((current === undefined) || (current === null) || !current.hasOwnProperty(segment)) {
            return fallback;
        }
        else {
            current = current[segment];
        }
    }
    return current;
}
exports.default = getSafe;


/***/ }),

/***/ "./src/index.ts":
/*!**********************!*\
  !*** ./src/index.ts ***!
  \**********************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const getSafe_1 = __webpack_require__(/*! ./getSafe */ "./src/getSafe.ts");
const vortex_api_1 = __webpack_require__(/*! vortex-api */ "vortex-api");
const nexus_integration_1 = __webpack_require__(/*! ./nexus-integration */ "./src/nexus-integration.ts");
const path = __webpack_require__(/*! path */ "path");
const fs = __webpack_require__(/*! fs */ "fs");
const activeProfile = (state) => {
    const profileId = state.settings.profiles.activeProfileId;
    return getSafe_1.default(state, ["persistent", "profiles", profileId], undefined);
};
const getActiveGameId = (state) => {
    const profile = activeProfile(state);
    return profile !== undefined ? profile.gameId : undefined;
};
const transformModFormat = (mod, activeProfile) => {
    var _a;
    return ({
        name: mod.attributes.modName,
        game: mod.attributes.downloadGame,
        modId: mod.attributes.modId,
        fileId: mod.attributes.fileId,
        source: mod.attributes.source,
        enabled: !!activeProfile.modState &&
            ((_a = activeProfile.modState[mod.id]) === null || _a === void 0 ? void 0 : _a.enabled) === true,
        vortexId: mod.id,
    });
};
const getInstalledMods = (state) => {
    const profile = activeProfile(state);
    return Object.values(state.persistent.mods)
        .map((game) => Object.values(game).map((mod) => transformModFormat(mod, profile)))
        .reduce((result, current) => result.concat(current), []);
};
const init = (context) => {
    const { api } = context;
    const modlistNamespace = "modlist-backup";
    const i18n = api.getI18n();
    const translationsReady = Promise.resolve(
        i18n && typeof i18n.loadNamespaces === "function"
            ? i18n.loadNamespaces(modlistNamespace)
            : undefined
    ).catch((error) => {
        console.log("Failed to load Modlist Backup translations", error);
    });
    const translateModlist = (key, options) =>
        api.translate(key, Object.assign({}, options, { ns: modlistNamespace }));
    const integrations = [];
    context.once(() => {
        nexus_integration_1.registerNexusIntegration(api);
    });
    context.registerAPI("addSourceToModlistBackup", (source, installMods) => {
        console.log("Source being registered", source);
        if (integrations.some((existing) => existing.source == source))
            return;
        integrations.push({
            source,
            installMods,
        });
    }, {});
    const backupMods = (thisGameOnly = false, thisProfileOnly = false) => {
        return () => {
            const state = api.store.getState();
            let mods = getInstalledMods(state);
            api
                .selectFile({ create: true, title: translateModlist("Select file to export to") })
                .then((fileName) => {
                const activeGameId = getActiveGameId(state);
                mods = mods
                    .filter((mod) => mod.modId && mod.fileId && mod.game);
                if (thisGameOnly) {
                    mods = mods.filter((mod) => mod.game === activeGameId);
                }
                if (thisProfileOnly) {
                    mods = mods.filter((mod) => mod.enabled);
                }
                if (fs.existsSync(fileName) && fs.readFileSync(fileName).length > 2) {
                    try {
                        const existingMods = JSON.parse(fs.readFileSync(fileName)).filter((mod) => mod.game !== activeGameId);
                        mods = mods
                            .filter((mod) => mod.game === activeGameId)
                            .concat(existingMods);
                    }
                    catch (error) {
                        console.log(error);
                    }
                }
                fs.writeFile(path.resolve(fileName), JSON.stringify(mods, null, 4), (error) => {
                    if (error) {
                        api.showErrorNotification(error, error);
                        return;
                    }
                    api.sendNotification({
                        type: "success",
                        title: translateModlist("Backup Complete"),
                        message: translateModlist("Modlist backed up to {{path}}", { replace: { path: path.resolve(fileName) } }),
                    });
                });
            })
                .catch((error) => console.log(error));
        };
    };
    const restoreMods = () => {
        const state = api.store.getState();
        let shouldInstall = state.settings.automation.install;
        const doInstall = translateModlist("Yes, download and install");
        const dontInstall = translateModlist("No, download only");
        if (!getSafe_1.default(state, ["persistent", "nexus", "userInfo", "isPremium"], false)) {
            api.showErrorNotification(translateModlist("You need to be a premium member to restore a list of mods"), translateModlist("You need to be a premium member to restore a list of mods"));
            return;
        }
        api
            .selectFile({ create: false, title: translateModlist("Select your backup file to import") })
            .then((fileName) => {
            fs.readFile(path.resolve(fileName), (error, jsonString) => __awaiter(void 0, void 0, void 0, function* () {
                const activeGameId = getActiveGameId(state);
                if (error) {
                    api.showErrorNotification(error, error);
                    return;
                }
                shouldInstall =
                    state.settings.automation.install ||
                        (yield api
                            .showDialog("question", translateModlist("Automatically install?"), {
                            text: translateModlist("In your settings, you've disabled automatic installation on download. Would you like to install these mods when they download anyway?"),
                        }, [
                            {
                                label: dontInstall,
                            },
                            {
                                label: doInstall,
                            },
                        ])
                            .then((result) => (result === null || result === void 0 ? void 0 : result.action) === doInstall));
                let mods = JSON.parse(jsonString);
                mods = mods
                    .filter((mod) => mod.game === activeGameId)
                    .map((mod) => {
                    if (!mod.source) {
                        mod.source = "nexus";
                    }
                    return mod;
                })
                    .filter((mod) => integrations.some(({ source }) => mod.source === source));
                const modsFound = mods.length;
                const installedMods = getInstalledMods(state);
                const modsToInstall = mods.filter((mod) => !installedMods.some((installedMod) => installedMod.game === mod.game &&
                    installedMod.modId === mod.modId &&
                    installedMod.fileId === mod.fileId));
                const modsToChangeState = mods
                    .filter((mod) => mod.enabled !== undefined)
                    .map((mod) => installedMods.filter((installedMod) => installedMod.game === mod.game &&
                    installedMod.modId === mod.modId &&
                    installedMod.fileId === mod.fileId &&
                    installedMod.enabled !== undefined &&
                    installedMod.enabled !== mod.enabled)[0])
                    .filter((mod) => !!mod);
                const installs = {};
                for (const mod of modsToInstall) {
                    const integration = integrations.find(({ source }) => mod.source === source);
                    if (!integration)
                        continue;
                    if (!installs[integration.source])
                        installs[integration.source] = [];
                    installs[integration.source].push(mod);
                }
                Object.keys(installs).forEach((installSource) => {
                    const integration = integrations.find(({ source }) => installSource === source);
                    integration.installMods(api, installs[installSource]);
                });
                modsToChangeState.forEach((mod) => api.store.dispatch(vortex_api_1.actions.setModEnabled(vortex_api_1.selectors.activeProfile(api.getState()).id, mod.vortexId, !mod.enabled)));
                api.sendNotification({
                    type: "success",
                    title: translateModlist("Restoring {{count}} mods for {{game}}", { replace: { count: modsToInstall.length, game: activeGameId } }),
                    message: translateModlist("{{found}} mods found for {{game}}, but only {{installCount}} mods installed", { replace: { found: modsFound, game: activeGameId, installCount: modsToInstall.length } }),
                });
            }));
        })
            .catch((error) => console.log(error));
    };
    translationsReady.then(() => {
        context.registerAction("mod-icons", 999, "show", {}, translateModlist("Modlist Backup: Restore"), restoreMods);
        context.registerAction("mod-icons", 999, "show", {}, translateModlist("Modlist Backup All Games"), backupMods());
        context.registerAction("mod-icons", 999, "show", {}, translateModlist("Modlist Backup Only This Game"), backupMods(true));
        context.registerAction("mod-icons", 999, "show", {}, translateModlist("Modlist Backup Only This Profile"), backupMods(true, true));
    });
};
module.exports = { default: init };


/***/ }),

/***/ "./src/nexus-integration.ts":
/*!**********************************!*\
  !*** ./src/nexus-integration.ts ***!
  \**********************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
exports.registerNexusIntegration = void 0;
exports.registerNexusIntegration = (api) => {
    var _a, _b;
    (_b = (_a = api.ext).addSourceToModlistBackup) === null || _b === void 0 ? void 0 : _b.call(_a, "nexus", (api, mods) => {
        const state = api.store.getState();
        let shouldInstall = state.settings.automation.install;
        mods.forEach((mod) => {
            if (shouldInstall && mod.enabled !== false)
                api.events.emit("mod-update", mod.game, mod.modId, mod.fileId, mod.source);
            else {
                api.emitAndAwait("nexus-download", mod.game, mod.modId, mod.fileId, undefined, false);
            }
        });
    });
};


/***/ }),

/***/ "fs":
/*!*********************!*\
  !*** external "fs" ***!
  \*********************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = require("fs");

/***/ }),

/***/ "path":
/*!***********************!*\
  !*** external "path" ***!
  \***********************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = require("path");

/***/ }),

/***/ "vortex-api":
/*!*****************************!*\
  !*** external "vortex-api" ***!
  \*****************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = require("vortex-api");

/***/ })

/******/ });
//# sourceMappingURL=modlist-backup.js.map