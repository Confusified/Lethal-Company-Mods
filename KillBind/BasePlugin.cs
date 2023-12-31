using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using LethalCompanyInputUtils.Api;
using UnityEngine.InputSystem;

namespace KillBindNS
{
    public class KillBind : LcInputActions
    {
        [InputAction("<Keyboard>/k", Name = "Suicide", GamepadPath = "<Gamepad>/Button North", ActionType = InputActionType.Button)]
        public InputAction ExplodeKey { get; set; }
    }

    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("com.rune580.LethalCompanyInputUtils", BepInDependency.DependencyFlags.HardDependency)]
    public class BasePlugin : BaseUnityPlugin
    {

        //Mod Defining Vars
        private const string modGUID = "Confusified.KillBind";
        private const string modName = "Kill Bind";
        private const string modVersion = "1.1.1";
        private readonly Harmony _harmony = new Harmony(modGUID);

        //Mod Config Vars
        public ConfigFile Configuration = new ConfigFile(Utility.CombinePaths(Paths.ConfigPath + "\\Confusified\\", $"{modGUID.Substring(12)}.cfg"), false);
        public static ConfigEntry<bool> ModEnabled;
        public static ConfigEntry<int> DeathCause;
        public static ConfigEntry<int> HeadType;

        //Mod Non-Config Vars
        internal static KillBind InputActionInstance = new KillBind();

        //Mod Functions
        private void Awake()
        {
            SetModConfig();
            _harmony.PatchAll();
            Logger.LogInfo($"{modName} {modVersion} has loaded succesfully");
        }

        private void SetModConfig()
        {
            ModEnabled = Configuration.Bind<bool>("Global Settings", "Enabled", true, "Toggle the mod");
            DeathCause = Configuration.Bind<int>("Mod Settings", "DeathType", 0, "Decide what cause of death you will have when pressing your kill bind.\n" +
                "There are currently 13 death causes in the game:\n" +
                "(0) Unknown (Default)\n(1) Bludgeoning\n(2) Gravity\n(3) Blast\n(4) Strangulation\n(5) Suffocation\n(6) Mauling\n(7) Gunshots\n(8) Crushing\n(9) Drowning\n(10) Abandoned\n(11) Electrocution\n(12) Kicking" +
                "\n\nDeath types (1)Bludgeoning, (6)Mauling and (7)Gunshots will cover the body with blood."
                );
            HeadType = Configuration.Bind<int>("Mod Settings", "HeadType", 1, "Decide what will happen with your head.\n" +
                "There are currently 3 head types in the game:\n" +
                "(0) Default - Your head will stay on your body\n" +
                "(1) Decapitate (Default) - Your head will be blown off of your body\n" +
                "(2) Coilhead - Your head will be replaced with a coil"
                );
        }
    }
}
