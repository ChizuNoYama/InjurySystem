using InjuryMod.Components;
using InjuryMod.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace InjuryMod.Behaviors
{
    internal class WoundMissionLogic : MissionLogic
    {
        // NOTE: Entry point
        public override void OnBehaviorInitialize()
        {
            base.OnBehaviorInitialize();

            WoundLogger.DebugLog("=====> WoundMissionLogic Initialized");
        }

        public override void OnAgentHit(Agent affectedAgent, Agent affectorAgent, in MissionWeapon affectorWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
        {
            base.OnAgentHit(affectedAgent, affectorAgent, affectorWeapon, blow, attackCollisionData);
            if (affectedAgent.IsMainAgent && affectedAgent.IsHero)
            {
                WoundedAgentComponent agentComp = affectedAgent.GetComponent<WoundedAgentComponent>();
                if (!attackCollisionData.AttackBlockedWithShield)
                {
                    agentComp?.ApplyLimbDamage(attackCollisionData.VictimHitBodyPart, attackCollisionData.InflictedDamage - attackCollisionData.AbsorbedByArmor);
                }
            }
        }
    }
}
