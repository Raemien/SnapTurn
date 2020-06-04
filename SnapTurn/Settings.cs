using BeatSaberMarkupLanguage.Attributes;
using BS_Utils.Utilities;

namespace SnapTurn
{
    internal class PluginConfig
    {
        public bool RegenerateConfig = true;
    }



    class Settings : PersistentSingleton<Settings>
    {
        private Config config;

        [UIValue("enable-snapturn")]
        public bool EnableSnapTurn
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
        public bool RegenerateConfig { get; internal set; }

        public void Awake()
        {
            config = new Config("SnapTurn");
            if (config.GetBool("General", "Enable in Gameplay", false) == true)
            {
                Logger.logger.Info("snapturn enabled in gameplay");
            }
        }
    }

}