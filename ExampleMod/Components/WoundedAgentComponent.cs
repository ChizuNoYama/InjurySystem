using ExampleMod.Models;
using ExampleMod.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace ExampleMod.Components
{
    internal class WoundedAgentComponent : AgentComponent
    {
        public WoundedAgentComponent(Agent agent) : base(agent)
        {
        }       

        public override void Initialize()
        {
            base.Initialize();

            if(LimbDamageManager.Instance != null)
            {
                LimbDamageManager.Instance.OnWoundApplied = this.OnWoundAppliedToAgent;
                LimbDamageManager.Instance.OnAllInjuriesHealed = this.OnAllInjuriesHealed;

                this.Agent.OnAgentHealthChanged -= this.OnAgentHealthChanged;
                this.Agent.OnAgentHealthChanged += this.OnAgentHealthChanged;
            }
        }

        private void OnAgentHealthChanged(Agent agent, float oldHealth, float newHealth)
        {
            if(oldHealth < newHealth)
            {
                WoundLogger.DebugLog($"=====> This Agent is healing.\n");
            }
        }

        private void OnWoundAppliedToAgent(BoneBodyPartType boneBodyPartType, float skillPenalty)
        {
            //TODO: Body part to skill converter here. Display what skill has been fucked and save it in the LimbManager.

            //    WoundLogger.DebugLog($"=====> Before change {this.GetHero()?.GetAttributeValue(DefaultCharacterAttributes.Endurance)}");
            //    this.GetHero()?.HeroDeveloper.RemoveAttribute(DefaultCharacterAttributes.Endurance, 2);
            //    WoundLogger.DebugLog($"=====> After change {this.GetHero()?.GetAttributeValue(DefaultCharacterAttributes.Endurance)}");            
        }

        private void OnAllInjuriesHealed()
        {
            this.GetHero()?.HeroDeveloper.AddAttribute(DefaultCharacterAttributes.Endurance, 2);
        }

        internal void ApplyLimbDamage(sbyte boneIndex, int damageInflicted)
        {
            BoneBodyPartType bodyPartType = this.Agent.AgentVisuals.GetBoneTypeData(boneIndex).BodyPartType;
            LimbDamageManager.Instance?.ApplyLimbDamage(bodyPartType, damageInflicted);
        }

        private CharacterObject? GetCharacterObject()
        {
            return (this.Agent.Character as CharacterObject);
        }

        private Hero? GetHero()
        {
            return this.GetCharacterObject()?.HeroObject;
        }
    }
}
