using GameNetcodeStuff;
using HarmonyLib;
using KillBindNS;
using System;
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
            if (BasePlugin.ModEnabled.Value && BasePlugin.InputActionInstance.ExplodeKey.triggered && !__instance.isPlayerDead && !__instance.isTypingChat && !HUDManager.Instance.typingIndicator.enabled && (!UnityEngine.Object.FindObjectOfType<Terminal>().terminalInUse && !__instance.inTerminalMenu) && __instance == GameNetworkManager.Instance.localPlayerController)
            {
                if (BasePlugin.DeathCause.Value > Enum.GetValues(typeof(CauseOfDeath)).Length) { BasePlugin.DeathCause.Value = 0; }

                int BlowHeadOff = 0;
                if (BasePlugin.BlowHeadOff.Value) { BlowHeadOff = 1; } else { BlowHeadOff = 0; }

                __instance.KillPlayer(Vector3.zero, spawnBody: true, (CauseOfDeath)BasePlugin.DeathCause.Value, BlowHeadOff);
            }
        }
    }
}
