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
                if (BasePlugin.DeathCause.Value > Enum.GetValues(typeof(CauseOfDeath)).Length || BasePlugin.DeathCause.Value < 0) { BasePlugin.DeathCause.Value = 0; } //If your choice is invalid, set to default (unknown death cause)
                if (BasePlugin.HeadType.Value > 3 || BasePlugin.HeadType.Value < 0) { BasePlugin.HeadType.Value = 1; } //If your choice is invalid, set to default (explode head)

                __instance.KillPlayer(Vector3.zero, true, (CauseOfDeath)BasePlugin.DeathCause.Value, BasePlugin.HeadType.Value);
            }
        }
    }
}
