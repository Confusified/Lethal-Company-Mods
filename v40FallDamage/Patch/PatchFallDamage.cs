using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace OldFallDamageMod.Patch
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PatchFallDamage
    {
        [HarmonyPatch("PlayerHitGroundEffects")]
        [HarmonyPrefix]
        //Majority of the code is taken from the decompiled code 
        private static void PatchedFallDamage(PlayerControllerB __instance)
        {

            __instance.GetCurrentMaterialStandingOn();
            if (__instance.fallValue < -9f)
            {
                if (__instance.fallValue < -16f)
                {
                    __instance.movementAudio.PlayOneShot(StartOfRound.Instance.playerHitGroundHard, 1f);
                    WalkieTalkie.TransmitOneShotAudio(__instance.movementAudio, StartOfRound.Instance.playerHitGroundHard, 1f);
                }
                else if (__instance.fallValue < -2f)
                {
                    __instance.movementAudio.PlayOneShot(StartOfRound.Instance.playerHitGroundSoft, 1f);
                }
                __instance.LandFromJumpServerRpc(__instance.fallValue < -16f);
            }
            if (__instance.fallValueUncapped < -30f && FallDamagePatchBase.ModEnabled.Value) //if mod enabled allow fall damage at lower fall speeds
            {
                __instance.takingFallDamage = true;
            }
            if (__instance.takingFallDamage && !__instance.jetpackControls && !__instance.disablingJetpackControls && !__instance.isSpeedCheating)
            {
                if (__instance.fallValueUncapped < -50f)
                {
                    __instance.DamagePlayer(100, hasDamageSFX: true, callRPC: true, CauseOfDeath.Gravity);
                }
                else
                {
                    int damageNumber = 40; //default to v45 damage
                    if (FallDamagePatchBase.ModEnabled.Value)
                    {
                        damageNumber = 20; //Change damage to v40 if mod is enabled
                    }
                    __instance.DamagePlayer(damageNumber, hasDamageSFX: true, callRPC: true, CauseOfDeath.Gravity);

                }
            }
            if (__instance.fallValue < -16f)
            {
                RoundManager.Instance.PlayAudibleNoise(((Component)__instance).transform.position, 7f);
            }
            return;
        }
    }
}
