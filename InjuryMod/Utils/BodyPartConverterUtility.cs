using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace InjuryMod.Utils;

public static class BodyPartConverterUtility
{
    public static List<SkillObject> GetSkillsFromBodyPart(BoneBodyPartType bodyPart)
    {
        List<SkillObject> skills = new List<SkillObject>();
        switch (bodyPart)
        {
            // TODO: Figure out what to do with chest
            case BoneBodyPartType.CriticalBodyPartsBegin: // Same as the head. have the same value
                skills.Add(DefaultSkills.Tactics);
                skills.Add(DefaultSkills.Trade);
                skills.Add(DefaultSkills.Charm);
                skills.Add(DefaultSkills.Steward);
                skills.Add(DefaultSkills.Engineering);
                skills.Add(DefaultSkills.Medicine);
                skills.Add(DefaultSkills.Leadership);
                break;
            case BoneBodyPartType.ShoulderLeft:
            case BoneBodyPartType.ShoulderRight:
            case BoneBodyPartType.ArmLeft:
            case BoneBodyPartType.ArmRight:
                skills.Add(DefaultSkills.TwoHanded);
                skills.Add(DefaultSkills.OneHanded);
                skills.Add(DefaultSkills.Crossbow);
                skills.Add(DefaultSkills.Crafting);
                skills.Add(DefaultSkills.Bow);
                skills.Add(DefaultSkills.Throwing);
                break;
            case BoneBodyPartType.Legs:
                skills.Add(DefaultSkills.Riding);
                skills.Add(DefaultSkills.Athletics);
                break;
        }
        return skills;
    }
    
    public static List<BoneBodyPartType> GetBodyPartsFromSkills(SkillObject skill)
    {
        List<BoneBodyPartType> parts = new();
        if (skill == DefaultSkills.Athletics || skill == DefaultSkills.Riding)
        {
            parts = new(){BoneBodyPartType.Legs};
        }

        if (skill == DefaultSkills.TwoHanded || skill == DefaultSkills.OneHanded || skill == DefaultSkills.Crossbow ||
            skill == DefaultSkills.Bow || skill == DefaultSkills.Throwing || skill == DefaultSkills.Crafting)
        {
            parts = new()
            {
                BoneBodyPartType.ArmLeft, BoneBodyPartType.ArmRight, BoneBodyPartType.ShoulderLeft,
                BoneBodyPartType.ShoulderRight
            };
        }

        if (skill == DefaultSkills.Tactics || skill == DefaultSkills.Engineering || skill == DefaultSkills.Charm ||
            skill == DefaultSkills.Leadership || skill == DefaultSkills.Scouting || skill == DefaultSkills.Steward)
        {
            parts = new (){BoneBodyPartType.CriticalBodyPartsBegin};
        }
        return parts;
    }
}