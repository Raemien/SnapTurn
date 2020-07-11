using BeatSaberMarkupLanguage.Attributes;
using BS_Utils.Utilities;
using System.Collections.Generic;
using System.Linq;
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

        [UIValue("enable-snapturn")]
        public bool EnableTurning
        {
            get => config.GetBool("General", "Enable SnapTurn", true);
            set => config.SetBool("General", "Enable SnapTurn", value);
        }

        [UIValue("enable-in-gamecore")]
        public bool EnableInSongs
        {
            get => config.GetBool("General", "Enable in Gameplay", false);
            set => config.SetBool("General", "Enable in Gameplay", value);
        }

        [UIValue("enable-smoothturn")]
        public bool SmoothTurn
        {
            get => config.GetBool("Rotation", "Smooth Turn", false);
            set => config.SetBool("Rotation", "Smooth Turn", value);
        }

        [UIValue("rotation-step")]
        public int RotationStep
        {
            get => config.GetInt("Rotation", "Rotation Step", 15);
            set => config.SetInt("Rotation", "Rotation Step", value);
        }

        [UIValue("controller-type")]
        public string SelectedController
        {
            get => config.GetString("Input", "Selected Controller", "Left");
            set => config.SetString("Input", "Selected Controller", value);
        }

        [UIValue("input-type")]
        public string InputType
        {
            get => config.GetString("Input", "Input Type", "Default");
            set => config.SetString("Input", "Input Type", value);
        }

        [UIValue("max-step")]
        private int MaxStep
        {
            get => config.GetBool("Rotation", "Smooth Turn", false) ? 5 : 60;
        }

        [UIValue("inputtype-options")]
        private List<object> InputTypes = new object[] { "Default", "Press", "Touch" }.ToList();

        [UIValue("controller-options")]
        private List<object> ControllerOptions = new object[] { "Left", "Right" }.ToList();


        public bool RegenerateConfig { get; internal set; }

        public void Awake()
        {
            config = new Config("SnapTurn");
        }
    }

}