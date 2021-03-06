using FrontierDevelopments.General;
using HarmonyLib;
using Verse;
using Verse.AI;

namespace FrontierDevelopments.Shields.Harmony
{
    public class Harmony_AttackTargetFinder
    {
        [HarmonyPatch(typeof(AttackTargetFinder), "GetShootingTargetScore")]
        public static class Patch_GetShootingTargetScore
        {
            static float Postfix(float __result, IAttackTarget target, IAttackTargetSearcher searcher, Verb verb)
            {
                if (!Mod.Settings.EnableAIAttackTargetFinder) return __result;
                
                if (searcher.Thing.Map.GetComponent<ShieldManager>().Shielded(PositionUtility.ToVector3(searcher.Thing.Position),
                    PositionUtility.ToVector3(target.Thing.Position)))
                {
                    return __result / 2;
                }
                return __result;
            }
        }
    }
}