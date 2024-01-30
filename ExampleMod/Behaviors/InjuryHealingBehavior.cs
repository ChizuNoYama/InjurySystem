using System;
using ExampleMod.Models;
using ExampleMod.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace ExampleMod.Behaviors;

internal class InjuryHealingBehavior : CampaignBehaviorBase
{
    public override void RegisterEvents()
    {
        CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(this, this.OnDailyTickPartyEvent);
    }
    

    private void OnDailyTickPartyEvent(MobileParty party)
    {
        if (party.Owner != null && party.Owner.IsHumanPlayerCharacter)
        {
           this.HealInjuries(party);
        }
    }

    private void HealInjuries(MobileParty party)
    {
        Hero surgeon = party.GetEffectiveRoleHolder(SkillEffect.PerkRole.Surgeon);
        if (surgeon != null)
        {
            int healingSkillValue = surgeon.GetSkillValue(DefaultSkills.Medicine);
            
            //Logging
            WoundLogger.DisplayMessage($"Surgeon ({surgeon.Name}) current Medicine skill value: {healingSkillValue}");
            
            // TODO: Calculate healing here based off surgeon
            int limbHealingAmount = 2;
            
            LimbDamageManager.Instance?.ApplyHealingToInjuries(limbHealingAmount);
        }
        
    }

    public override void SyncData(IDataStore dataStore)
    {
    }
}

