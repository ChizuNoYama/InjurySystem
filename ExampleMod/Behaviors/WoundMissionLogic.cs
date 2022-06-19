using ExampleMod.Components;
using ExampleMod.Models;
using ExampleMod.Utils;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace ExampleMod.Behaviors
{
    internal class WoundMissionLogic : MissionLogic
    {
        public override void OnBehaviorInitialize()
        {
            base.OnBehaviorInitialize();

            WoundLogger.DebugLog("=====> WoundMissionLogic Initialized");
        }

        //public override void OnRegisterBlow(Agent attacker, Agent victim, GameEntity realHitEntity, Blow b, ref AttackCollisionData collisionData, in MissionWeapon attackerWeapon)
        //{
        //    base.OnRegisterBlow(attacker, victim, realHitEntity, b, ref collisionData, attackerWeapon);

        //    if (victim.IsMainAgent && victim.IsHero)
        //    {
        //        MainAgentComponent agentComp = victim.GetComponent<MainAgentComponent>();
        //        agentComp?.ApplyLimbDamage(collisionData.CollisionBoneIndex, collisionData.InflictedDamage);
        //    }
        //}

        public override void OnAgentHit(Agent affectedAgent, Agent affectorAgent, in MissionWeapon affectorWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
        {
            base.OnAgentHit(affectedAgent, affectorAgent, affectorWeapon, blow, attackCollisionData);
            if (affectedAgent.IsMainAgent && affectedAgent.IsHero)
            {
                WoundedAgentComponent agentComp = affectedAgent.GetComponent<WoundedAgentComponent>();
                WoundLogger.DebugLog($"=====> Inflicted damage: {attackCollisionData.InflictedDamage}");
                if (!attackCollisionData.AttackBlockedWithShield)
                {
                    agentComp?.ApplyLimbDamage(attackCollisionData.CollisionBoneIndex, attackCollisionData.InflictedDamage);
                }
            }
        }

        //public override void OnMissileCollisionReaction(Mission.MissileCollisionReaction collisionReaction, Agent attackerAgent, Agent attachedAgent, sbyte attachedBoneIndex)
        //{
        //    base.OnMissileCollisionReaction(collisionReaction, attackerAgent, attachedAgent, attachedBoneIndex);

        //    Agent victim = attackerAgent.GetTargetAgent();
        //    collisionReaction.
        //    if (victim.IsMainAgent && victim.IsHero)
        //    {
        //        Utilities.DisplayMessage("=====> Missile Collision");
        //        MainAgentComponent agentComp = victim.GetComponent<MainAgentComponent>();
        //        agentComp?.ApplyLimbDamage(attachedBoneIndex);
        //    }
        //}
    }
}
