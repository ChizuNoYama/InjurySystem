using NetworkMessages.FromServer;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace ExampleMod.Models;

public class InjuryPenaltyManager
{
    public InjuryPenaltyManager()
    {
    }
    
    public static InjuryPenaltyManager Instance { get; private set; }

    public static void Initialize()
    {
        Instance = new InjuryPenaltyManager();
        Instance.InitializeAllSkillEffects();
    }
    
    public SkillEffect SwingSpeedPenalty { get; private set; }

    public void InitializeAllSkillEffects()
    {
        this.SwingSpeedPenalty = MBObjectManager.Instance.RegisterPresumedObject(new SkillEffect("SwingSpeedDebuff"));
        this.SwingSpeedPenalty.Initialize(description:new TextObject("One handed swing speed penalty: {penalty}"), 
                                           effectedSkills: new SkillObject[]{DefaultSkills.OneHanded, DefaultSkills.TwoHanded},
                                           primaryRole: SkillEffect.PerkRole.Personal, 
                                           incrementType: SkillEffect.EffectIncrementType.AddFactor,
                                           primaryBonus:0.5f,
                                           primaryBaseValue:1f);


        // SkillEffect.All.Add(this.SwingSpeedPenalty);
    }
}