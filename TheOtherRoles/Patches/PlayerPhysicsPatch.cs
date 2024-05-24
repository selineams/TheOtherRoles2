using HarmonyLib;
using UnityEngine;
using TheOtherRoles.Players;

namespace TheOtherRoles.Patches;

[HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.Awake))]
public static class PlayerPhysiscs_Awake_Patch
{
    [HarmonyPostfix]
    public static void Postfix(PlayerPhysics __instance)
    {
        if (!__instance.body) return;
        __instance.body.interpolation = RigidbodyInterpolation2D.Interpolate;
    }
}

[HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.FixedUpdate))]
    public static class PlayerPhysicsFixedUpdatePatch
    {
        public static void Postfix(PlayerPhysics __instance)
        {
            Kataomoi.FixedUpdate(__instance);
        }
    }
