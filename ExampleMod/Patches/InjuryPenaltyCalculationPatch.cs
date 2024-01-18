using ExampleMod.Utils;
using HarmonyLib;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace ExampleMod.Patches;

[HarmonyPatch(typeof(SkillHelper), nameof(SkillHelper.AddSkillBonusForCharacter))]
public class InjuryPenaltyCalculationPatch
{
    [HarmonyPostfix]
    public static void TestPenaltyCalculation(
        SkillObject skill,
        SkillEffect skillEffect,
        CharacterObject character,
        ref ExplainedNumber stat,
        int baseSkillOverride = -1,
        bool isBonusPositive = true,
        int extraSkillValue = 0)
    {
        if (isBonusPositive && character.IsPlayerCharacter) //TODO: Check for injuries
        {
            WoundLogger.DebugLog($"SkillEffect before \"{skillEffect.Description}\": {stat.ResultNumber}");
            if (skillEffect.IncrementType == SkillEffect.EffectIncrementType.Add)
            {
                //TODO: for now we will chop the number by half. If they are injured
                
                WoundLogger.DebugLog($"SkillEffect {skillEffect.Name} after by Add: {stat.ResultNumber}\n");
            }
            
            else if (skillEffect.IncrementType == SkillEffect.EffectIncrementType.AddFactor)
            {
                stat.AddFactor(-0.5f, description: new TextObject("This is a penalty by factor"));
                WoundLogger.DebugLog($"SkillEffect {skillEffect.Name} after by Factor: {stat.ResultNumber}\n");
            }
        }
    }
}