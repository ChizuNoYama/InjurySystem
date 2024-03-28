using System.Collections.Generic;
using HarmonyLib;
using TaleWorlds.MountAndBlade;
using InjuryMod.Models;
using InjuryMod.Utils;
using SandBox.GameComponents;

namespace InjuryMod.Patches;

// [HarmonyPatchCategory("AgentDrivenPropertyPatch")]
[HarmonyPatch(typeof(SandboxAgentStatCalculateModel), "UpdateHumanStats")]
public class InjuryPenaltyToAgentPropertiesPatch
{
    [HarmonyPostfix]
    static void ApplyPenaltyToAgentDrivenProperties(Agent agent, AgentDrivenProperties agentDrivenProperties)
    {
        BodyPartStatus status;

        foreach (KeyValuePair<BoneBodyPartType, BodyPartStatus> statusPair in LimbDamageManager.Instance!.DamagedLimbs)
        {
            if (statusPair.Value.IsInjured)
            {
                InjurySeverity severity = statusPair.Value.Severity;
                float penaltyMultiplier = InjurySeverityUtilities.GetPenaltyMultipler(severity);

                switch (statusPair.Key)
                {
                    case BoneBodyPartType.ArmLeft | BoneBodyPartType.ArmRight:
                        agentDrivenProperties.SwingSpeedMultiplier *= penaltyMultiplier;
                        agentDrivenProperties.HandlingMultiplier *= penaltyMultiplier;
                        agentDrivenProperties.ReloadSpeed *= penaltyMultiplier;
                        break;
                    case BoneBodyPartType.Legs:
                        agentDrivenProperties.MountManeuver *= penaltyMultiplier;
                        break;
            }
        // if(status == null)
        // {
        //     LimbDamageManager.Instance.DamagedLimbs.TryGetValue(BoneBodyPartType.Head, out status);
        // }
        //
        // if (status is { IsInjured: true })
        // {
        //     InjurySeverity severity = status.Severity;
        //     float penaltyMultiplier = InjurySeverityUtilities.GetPenaltyMultipler(severity);
        //     agentDrivenProperties.SwingSpeedMultiplier *= penaltyMultiplier;

            // MethodInfo? updateDrivenProperties = typeof(AgentDrivenProperties).GetMethod("UpdateDrivenProperties")
            //
            // MethodInfo? privMethod = __instance.GetType().GetMethod("UpdateDrivenProperties", 
            //     BindingFlags.NonPublic | BindingFlags.Instance);
            // privMethod?.Invoke(__instance, new object[__instance.AgentDrivenProperties.Values]);

        }
    }
}