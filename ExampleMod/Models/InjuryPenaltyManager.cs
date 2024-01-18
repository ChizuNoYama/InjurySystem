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
        Instance.RegisterSkillEffects();
    }
    
    public SkillEffect SwingSpeedPenalty { get; private set; }

    public void RegisterSkillEffects()
    {
        
    }
}