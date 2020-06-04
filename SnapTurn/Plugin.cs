using BeatSaberMarkupLanguage.Settings;
using BS_Utils.Gameplay;
using IPA;
using IPA.Config;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnapTurn
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class SnapTurn
    {
        public const string assemblyName = "SnapTurn";

        public static IPA.Logging.Logger Logger { get; private set; }

        private TurnManager turnManager;

        [Init]
        public SnapTurn(IPA.Logging.Logger logger, [IPA.Config.Config.Prefer("json")] IConfigProvider cfgProvider)
        {
            SnapTurn.Logger = logger;
        }


        [OnStart]
        public void Start()
        {
            PersistentSingleton<Settings>.TouchInstance();

            BSMLSettings.instance.AddSettingsMenu("SnapTurn", "SnapTurn.Views.SnapTurnSettings.bsml", Settings.instance);

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
            Logger.Log(IPA.Logging.Logger.Level.Info, "Preparing SnapTurn...");
            if (turnManager == null)
            {
                Logger.Log(IPA.Logging.Logger.Level.Debug, turnManager == null ? "TurnManager found" : "TurnManager not found");
                turnManager = new GameObject(nameof(TurnManager)).AddComponent<TurnManager>();
                turnManager.InitSnapTurnScene();
            }
        }

    }
}