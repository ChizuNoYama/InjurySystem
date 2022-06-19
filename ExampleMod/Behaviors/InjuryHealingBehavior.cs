using ExampleMod.Models;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;

namespace ExampleMod.Behaviors
{
    internal class InjuryHealingBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(this, OnDailyTickPartyEvent);
        }

        private void OnDailyTickPartyEvent(MobileParty party)
        {
            LimbDamageManager.Instance?.ApplyHealingToInjuries();
        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}
