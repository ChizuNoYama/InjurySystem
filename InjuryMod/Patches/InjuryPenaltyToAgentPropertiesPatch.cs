using System.Collections.Generic;
using HarmonyLib;
using TaleWorlds.MountAndBlade;
using InjuryMod.Models;
using InjuryMod.Utils;
using SandBox.GameComponents;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;

namespace InjuryMod.Patches;

[HarmonyPatch(typeof(SandboxAgentStatCalculateModel), "UpdateHumanStats")]
public class InjuryPenaltyToAgentPropertiesPatch
{
    [HarmonyPostfix]
    static void ApplyPenaltyToAgentDrivenProperties(Agent agent, AgentDrivenProperties agentDrivenProperties)
    {
        foreach (KeyValuePair<BoneBodyPartType, BodyPartStatus> statusPair in LimbDamageManager.Instance!.DamagedLimbs)
        {
            if (statusPair.Value.IsInjured)
            {
                InjurySeverity severity = statusPair.Value.Severity;
                float penaltyMultiplier = InjurySeverityUtilities.GetPenaltyMultipler(severity);

                switch (statusPair.Key)
                {
                    case BoneBodyPartType.ArmLeft:
                    case BoneBodyPartType.ArmRight:
                        agentDrivenProperties.SwingSpeedMultiplier *= penaltyMultiplier;
                        agentDrivenProperties.HandlingMultiplier *= penaltyMultiplier;
                        agentDrivenProperties.ReloadSpeed *= penaltyMultiplier;
                        agentDrivenProperties.WeaponBestAccuracyWaitTime *= penaltyMultiplier;
                        agentDrivenProperties.WeaponsEncumbrance *= penaltyMultiplier;
                        break;
                    case BoneBodyPartType.Legs:
                        agentDrivenProperties.MountManeuver *= penaltyMultiplier;
                        agentDrivenProperties.MountSpeed += penaltyMultiplier;
                        agentDrivenProperties.ArmorEncumbrance *= penaltyMultiplier;
                        agentDrivenProperties.TopSpeedReachDuration *= penaltyMultiplier;
                        agentDrivenProperties.MaxSpeedMultiplier *= penaltyMultiplier;
                        break;
                    case BoneBodyPartType.Chest:
                        agentDrivenProperties.ArmorEncumbrance *= penaltyMultiplier;
                        break;
                }
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