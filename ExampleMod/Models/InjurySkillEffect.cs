using NetworkMessages.FromServer;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

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
        Instance.InitializeAllSkillEffects();
    }
    
    public SkillEffect OneHandedSwingSpeedPenalty { get; private set; }

    public void InitializeAllSkillEffects()
    {
        this.OneHandedSwingSpeedPenalty = MBObjectManager.Instance.RegisterPresumedObject(new SkillEffect("SwingSpeedDebuff"));
        // this.OneHandedSwingSpeedPenalty.Initialize(description:new TextObject("One handed swing speed penalty: {penalty}"), 
        //                                            effectedSkills: new SkillObject[]{DefaultSkills.OneHanded, DefaultSkills.TwoHanded},
        //                                            primaryRole: SkillEffect.PerkRole.Personal, 
        //                                            incrementType: SkillEffect.EffectIncrementType.AddFactor,
        //                                            primaryBonus:0.5f,
        //                                            primaryBaseValue:1f);
        
        
        // SkillEffect.All.Add(this.OneHandedSwingSpeedPenalty);
    }
}