using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace ExampleMod.Utils;

public static class BodyPartToSkillConverter
{
    public static List<SkillObject> GetSkillsFromBodyPart(BoneBodyPartType bodyPart)
    {
        List<SkillObject> skills = new List<SkillObject>();
        switch (bodyPart)
        {
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
}