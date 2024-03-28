using HarmonyLib;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace InjuryMod.Patches;

[HarmonyPatch(typeof(SkillHelper), nameof(SkillHelper.AddSkillBonusForCharacter))]
public class InjuryPenaltyToSkillCheckPatch
{
    [HarmonyPostfix]
    public static void ApplyPenaltyToSkillChecks(
        SkillObject skill,
        SkillEffect skillEffect,
        CharacterObject character,
        ref ExplainedNumber stat,
        int baseSkillOverride = -1,
        bool isBonusPositive = true,
        int extraSkillValue = 0)
    {
    //     if (isBonusPositive && character.IsPlayerCharacter)
    //     {
    //         List<BoneBodyPartType> parts = BodyPartToSkillConverter.GetBodyPartsFromSkills(skill);
    //         if (parts.Count > 0)
    //         {
    //             float penaltyPercentage = 0f;
    //             foreach (BoneBodyPartType part in parts)
    //             {
    //                 if(LimbDamageManager.Instance!.DamagedLimbs.TryGetValue(part, out BodyPartStatus status) && status.IsInjured)
    //                 {
    //                     InjurySeverity severity = LimbDamageManager.Instance.DamagedLimbs[part].Severity;
    //                     penaltyPercentage = InjurySeverityUtilities.GetPenaltyMultipler(severity);
    //                 }
    //             }
    //
    //             if (penaltyPercentage > 0)
    //             {
    //                 if (skillEffect.IncrementType == SkillEffect.EffectIncrementType.Add)
    //                 {
    //                     stat.Add(-(stat.ResultNumber * penaltyPercentage), description: new TextObject("This is a penalty by add"));
    //                     WoundLogger.DebugLog($"SkillEffect {skillEffect.Name} after by Add: {stat.ResultNumber}\n");
    //                 }
    //
    //                 else if (skillEffect.IncrementType == SkillEffect.EffectIncrementType.AddFactor)
    //                 {
    //                     stat.AddFactor(-penaltyPercentage, description: new TextObject("This is a penalty by factor"));
    //                     WoundLogger.DebugLog(
    //                         $"SkillEffect {skillEffect.Name} after by Factor: {stat.ResultNumber}\n");
    //                 }
    //             }
    //         }
    //     }
    }
}

// [Harrmony]
// public class InjuryPenalty
// {
//     [HarmonyPatch]
//     public static void
// }