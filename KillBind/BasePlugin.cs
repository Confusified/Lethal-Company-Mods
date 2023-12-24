using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using LethalCompanyInputUtils.Api;
using UnityEngine.InputSystem;

namespace KillBindNS
{
    public class KillBind : LcInputActions
    {
        [InputAction("<Keyboard>/k", Name = "Explode Head", GamepadPath = "<Gamepad>/Button North", ActionType = InputActionType.Button)]
        public InputAction ExplodeKey { get; set; }
    }

    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("com.rune580.LethalCompanyInputUtils", BepInDependency.DependencyFlags.HardDependency)]
    public class BasePlugin : BaseUnityPlugin
    {

        //Mod Defining Vars
        private const string modGUID = "Confusified.KillBind";
        private const string modName = "Kill Bind";
        private const string modVersion = "1.0.0";
        private readonly Harmony _harmony = new Harmony(modGUID);

        //Mod Config Vars
        public ConfigFile Configuration = new ConfigFile(Utility.CombinePaths(Paths.ConfigPath + "\\Confusified\\", $"{modGUID.Substring(12)}.cfg"), false);
        public static ConfigEntry<bool> ModEnabled;

        //Mod Non-Config Vars
        internal static KillBind InputActionInstance = new KillBind();

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
