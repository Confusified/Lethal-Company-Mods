using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace OldFallDamageMod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class FallDamagePatchBase : BaseUnityPlugin
    {
        //Mod Defining Vars
        private const string modGUID = "Confusified.v40FallDamageStandalone";
        private const string modName = "v40 Fall Damage (Standalone version)";
        private const string modVersion = "1.0.0";
        private readonly Harmony _harmony = new Harmony(modGUID);

        //Mod Config Vars
        public ConfigFile Configuration = new ConfigFile(Utility.CombinePaths(Paths.ConfigPath + "\\Confusified\\", $"{modGUID.Substring(12)}.cfg"), false);
        public static ConfigEntry<bool> ModEnabled;

        //Mod Functions
        private void Awake()
        {
            SetModConfig();

            _harmony.PatchAll();
            Logger.LogInfo($"{modName} {modVersion} has loaded");
        }

        private void SetModConfig()
        {
            ModEnabled = Configuration.Bind<bool>("Mod Settings", "Enabled", true, "Toggle the mod");
        }
    }
}
