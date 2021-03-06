﻿using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.Settings;
using BS_Utils.Gameplay;
using IPA;
using IPA.Config;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace SnapTurn
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class SnapTurn
    {
        public const string assemblyName = "SnapTurn";

        private TurnManager turnManager;

        [Init]
        public SnapTurn(IPALogger logger, [IPA.Config.Config.Prefer("json")] IConfigProvider cfgProvider)
        {
            Logger.log = logger;
        }


        [OnStart]
        public void Start()
        {
            PersistentSingleton<Settings>.TouchInstance();

            BSMLSettings.instance.AddSettingsMenu("SnapTurn", "SnapTurn.Views.SnapTurnSettings.bsml", Settings.instance);
            GameplaySetup.instance.AddTab("SnapTurn", "SnapTurn.Views.SnapTurnModifiers.bsml", Settings.instance);

            SceneManager.sceneLoaded += OnSceneLoaded;

            StartTurnManager();
        }


        private void OnSceneLoaded(Scene newScene, LoadSceneMode sceneMode)
        {
            StartTurnManager();
            if (newScene.name != "GameCore")
            {
                ScoreSubmission.RemoveProlongedDisable("SnapTurn");
            }
        }


        private void StartTurnManager()
        {
            string curscene = SceneManager.GetActiveScene().name;
            var config = Settings.instance;
            Logger.Log("Preparing SnapTurn...", IPA.Logging.Logger.Level.Info);
            if (turnManager == null)
            {
                Logger.Log(turnManager == null ? "TurnManager found" : "TurnManager not found", IPA.Logging.Logger.Level.Debug);
                turnManager = new GameObject(nameof(TurnManager)).AddComponent<TurnManager>();
            }
            turnManager.enabled = (config.EnableTurning && !(curscene == "GameCore" && !config.EnableInSongs));
        }

    }
}