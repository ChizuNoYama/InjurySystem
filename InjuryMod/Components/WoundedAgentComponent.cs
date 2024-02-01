using System;
using Helpers;
using InjuryMod.Models;
using InjuryMod.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace InjuryMod.Components
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
            }
        }

        private void OnInjuryAppliedToAgent(BoneBodyPartType boneBodyPartType)
        {
            BodyPartStatus status = LimbDamageManager.Instance!.DamagedLimbs[boneBodyPartType];
            WoundLogger.DisplayMessage($"{Enum.GetName(typeof(BoneBodyPartType), boneBodyPartType)} is {status.Severity.ToString()}");
            // this.ApplyPenaltyToSkillBonus(boneBodyPartType);
        }

        internal void ApplyPenaltyToSkillBonus(BoneBodyPartType boneBodyPartType)
        {
            // TODO: Body part to skill converter here. Display what skill has been penalized and save it in the LimbManager.
            // TODO: Calculate penalty amount based on player's Endurance/Vigor and total damage on the limb

            // Testing how to leverage the SkillHelper to change values from SkillEffects.
            // WoundLogger.DebugLog($"Current OneHandedDamage: {DefaultSkillEffects.OneHandedDamage.GetPrimaryValue(Hero.MainHero.GetSkillValue(DefaultSkills.OneHanded))}");
            // ExplainedNumber stat = new ExplainedNumber(0.5f, includeDescriptions: true, new TextObject("New OneHandedDamage"));
            // SkillHelper.AddSkillBonusForCharacter(DefaultSkills.OneHanded, DefaultSkillEffects.OneHandedDamage, Hero.MainHero.CharacterObject ,ref stat, isBonusPositive: false, );
            
            //Testing AgentDrivenProperties
            // this.Agent.AgentDrivenProperties.
            
            //This is temporary until I find a way to fine tune how debuffs will work (i.e. weapon swing speed. persuasion calculations) outside of just changing the skill level.
            // The idea is that a person's skill does not deteriorate as a result of an injury, just the stats of how things are calculated
             // List<SkillObject> affectedSkills = BodyPartToSkillConverter.GetSkillsFromBodyPart(boneBodyPartType);
             //
             // foreach (SkillObject skill in affectedSkills)
             // {
             //     int currentSkillValue = Hero.MainHero.GetSkillValue(skill);
             //     int changeAmount = currentSkillValue < 3 ? currentSkillValue : 3; // TODO: this looks a little fishy. Check if there is any way the skill can be a negative number=
             //     Hero.MainHero.SetSkillValue(skill, currentSkillValue - changeAmount);
             // }
        }

        internal void ApplyLimbDamage(BoneBodyPartType bodyPart, int damageInflicted)
        {
            if (LimbDamageManager.Instance != null)
            {
                int maxAttValue = Campaign.Current.Models.CharacterDevelopmentModel.MaxAttribute;
                int limbDamage = LimbDamageManager.Instance.CalculateLimbDamage(damageInflicted, 
                                                                    Hero.MainHero.GetAttributeValue(DefaultCharacterAttributes.Endurance), 
                                                                                maxAttValue);
                LimbDamageManager.Instance.ApplyLimbDamage(bodyPart, limbDamage);
            }
        }
    }
}
