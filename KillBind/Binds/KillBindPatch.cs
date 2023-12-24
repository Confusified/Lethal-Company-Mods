using GameNetcodeStuff;
using HarmonyLib;
using KillBindNS;
using UnityEngine;

namespace KillBind.Binds
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class KillBindPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        private static void onPressKill(PlayerControllerB __instance)
        {
            if (BasePlugin.ModEnabled.Value && BasePlugin.InputActionInstance.ExplodeKey.triggered && !__instance.isPlayerDead && !__instance.isTypingChat && !HUDManager.Instance.typingIndicator.enabled && (!UnityEngine.Object.FindObjectOfType<Terminal>().terminalInUse && !__instance.inTerminalMenu))
            {
                __instance.KillPlayer(Vector3.zero, spawnBody: true, CauseOfDeath.Unknown, 1);
            }
        }
    }
}
