using BeatSaberMarkupLanguage.Attributes;
using BS_Utils.Utilities;
using UnityEngine;

namespace SnapTurn
{
    internal class PluginConfig
    {
        public bool RegenerateConfig = true;
    }



    class Settings : PersistentSingleton<Settings>
    {
        private Config config;

        [UIAction("reset-rotation")]
        private void ResetRotation()
        {
            Logger.Log("Reseting Rotation angle...", IPA.Logging.Logger.Level.Info);
            TurnManager tMan = FindObjectOfType<TurnManager>();
            tMan.transform.rotation = Quaternion.Euler(0, 0, 0);
            tMan.SetSceneRotations();
        }

        [UIValue("enable-in-gamecore")]
        public bool EnableInSongs
        {
            get => config.GetBool("General", "Enable in Gameplay", false);
            set => config.SetBool("General", "Enable in Gameplay", value);
        }

        [UIValue("rotation-step")]
        public int RotationStep
        {
            get => config.GetInt("Rotation", "Rotation Step", 15);
            set => config.SetInt("Rotation", "Rotation Step", value);
        }

        [UIValue("enable-smoothturn")]
        public bool SmoothTurn
        {
            get => config.GetBool("Rotation", "Smooth Turn", false);
            set => config.SetBool("Rotation", "Smooth Turn", value);
        }

        [UIValue("max-step")]
        private int MaxStep
        {
            get => config.GetBool("Rotation", "Smooth Turn", false) ? 5 : 60;
        }

        [UIValue("enable-snapturn")]
        public bool EnableTurning
        {
            get => config.GetBool("General", "Enable Joystick Turns", true);
            set => config.SetBool("General", "Enable Joystick Turns", value);
        }

        public bool RegenerateConfig { get; internal set; }

        public void Awake()
        {
            config = new Config("SnapTurn");
        }
    }

}