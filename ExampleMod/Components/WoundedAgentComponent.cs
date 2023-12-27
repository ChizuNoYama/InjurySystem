using System;
using System.Collections.Generic;
using ExampleMod.Models;
using ExampleMod.Utils;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;
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
                LimbDamageManager.Instance.OnInjuryApplied = null;
                LimbDamageManager.Instance.OnInjuryApplied = this.OnInjuryAppliedToAgent;

                // TODO: Calculate Damage cap here
                // TODO: Figure out why I wrote the above TODO... I have no idea why.
            }
        }
        
        private Hero _hero => this.GetCharacterObject()?.HeroObject!;

        private void OnInjuryAppliedToAgent(BoneBodyPartType boneBodyPartType)
        {
            WoundLogger.DisplayMessage($"{Enum.GetName(typeof(BoneBodyPartType), boneBodyPartType)} Is Injured");
            this.ApplySkillPenalty(boneBodyPartType);
        }

        private void ApplySkillPenalty(BoneBodyPartType boneBodyPartType)
        {
            // TODO: Body part to skill converter here. Display what skill has been penalized and save it in the LimbManager.
            // TODO: Calculate penalty amount based on player's Endurance/Vigor and total damage on the limb

            List<SkillObject> affectedSkills = BodyToSkillConverter.GetSkillsFromBodyPart(boneBodyPartType);
            
            
            foreach (SkillObject skill in affectedSkills)
            {
                int currentSkillValue = _hero!.GetSkillValue(skill);
                _hero.SetSkillValue(skill, currentSkillValue - 5);
            }
        }

        private void ApplySkillEffectPenalty(BoneBodyPartType bodyPart)
        {
            ExplainedNumber stat = new ExplainedNumber(baseNumber: 0f, includeDescriptions: true,
                baseText: new TextObject("This is a test skill effect"));
            SkillHelper.AddSkillBonusForCharacter(DefaultSkills.OneHanded, InjurySkillEffects.Instance.OneHandedSwingSpeedPenalty, this.GetCharacterObject(), ref stat, isBonusPositive:false);
        }

        internal void ApplyLimbDamage(BoneBodyPartType bodyPart, int damageInflicted)
        {
            LimbDamageManager.Instance?.ApplyLimbDamage(bodyPart, damageInflicted);
        }

        private CharacterObject? GetCharacterObject()
        {
            return this.Agent.Character as CharacterObject;
        }
    }
}
