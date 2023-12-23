using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using static TerminalApi.Events.Events;
using static TerminalApi.TerminalApi;

namespace OldFallDamageMod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("atomic.terminalapi", MinimumDependencyVersion: "1.2.0")]
    public class FallDamagePatchBase : BaseUnityPlugin
    {
        //Mod Defining Vars
        private const string modGUID = "Confusified.v40FallDamage";
        private const string modName = "v40 Fall Damage";
        private const string modVersion = "1.1.0";
        private readonly Harmony _harmony = new Harmony(modGUID);

        //Mod Config Vars
        public ConfigFile Configuration = new ConfigFile(Utility.CombinePaths(Paths.ConfigPath + "\\Confusified\\", $"{modGUID.Substring(12)}.cfg"), false);
        public static ConfigEntry<bool> ModEnabled;

        //Mod Functions
        private void Awake()
        {
            SetModConfig();

            TerminalAwake += TerminalIsAwake;
            TerminalParsedSentence += TextSubmitted;
            Logger.LogInfo("Loaded Terminal Commands");

            _harmony.PatchAll();
            Logger.LogInfo($"{modName} {modVersion} has loaded");
        }

        private void SetModConfig()
        {
            ModEnabled = Configuration.Bind<bool>("Mod Settings", "ModEnabled", true, "Toggle the mod");
        }

        private void TerminalIsAwake(object sender, TerminalEventArgs e)
        {
            AddCommand("v40falldamage", $"{modName} has been toggled\n\n", "toggle", true);
        }

        private void TextSubmitted(object sender, TerminalParseSentenceEventArgs e)
        {
            if (e.SubmittedText.ToLower().Contains("v40falldamage"))
            {
                ModEnabled.Value = !ModEnabled.Value;
                Logger.LogMessage($"Toggled v40 Fall Damage, enabled: {ModEnabled.Value}");
                return;
            }
        }
    }
}
