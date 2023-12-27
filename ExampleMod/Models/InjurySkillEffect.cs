using NetworkMessages.FromServer;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace ExampleMod.Models;

public class InjurySkillEffects
{
    public InjurySkillEffects()
    {
    }
    
    public static InjurySkillEffects Instance { get; private set; }

    public static void Initialize()
    {
        Instance = new InjurySkillEffects();
    }
    
    public SkillEffect OneHandedSwingSpeedPenalty { get; private set; }

    public void InitializeAll()
    {
        Instance.OneHandedSwingSpeedPenalty.Initialize(description:new TextObject("One handed swing speed penalty: {penalty}"), 
                                                       effectedSkills: [DefaultSkills.OneHanded, DefaultSkills.TwoHanded],
                                                       primaryRole: SkillEffect.PerkRole.Personal, 
                                                       incrementType: SkillEffect.EffectIncrementType.Add);
    }
}