using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace ExampleMod.Models
{
    internal class LimbDamageManager
    {
        private LimbDamageManager()
        {
            DamagedLimbs = new Dictionary<BoneBodyPartType, BodyPartStatus>();
        }
        
        public Action<BoneBodyPartType>? OnInjuryApplied { get; set; }
        public Action? OnAllInjuriesHealed { get; set; }
        public Dictionary<BoneBodyPartType, BodyPartStatus> DamagedLimbs { get; private set; }
        public Dictionary<SkillEffect, Penalty> Penalties { get; private set; }
        public static LimbDamageManager? Instance { get; private set; }
        public bool HasInjuries { get; set; }

        public static void Initialize(Dictionary<BoneBodyPartType, BodyPartStatus>? damagedLimbs = null )
        {
            Instance = new LimbDamageManager
            {
                DamagedLimbs = damagedLimbs ?? new Dictionary<BoneBodyPartType, BodyPartStatus>()
            };
        }

        public void ResetLimbDamage(BoneBodyPartType bodyPartType)
        {
            if (this.DamagedLimbs.TryGetValue(bodyPartType, out BodyPartStatus limb))
            {
                limb.TotalDamage = 0;
            }
        }

        public void ApplyHealingToInjuries()
        {
            // TODO: Input healing amount and calculate how much based on amount healed from party Surgeon
            if (this.HasInjuries)
            {
                foreach (KeyValuePair<BoneBodyPartType,BodyPartStatus>  damage in DamagedLimbs)
                {
                    BodyPartStatus ld = damage.Value;
                    if (!ld.IsInjured && ld.TotalDamage > 0)
                    {
                        ld.TotalDamage -= 2;
                    }
                    else
                    {
                        ld.TotalDamage -= 1;
                    }

                    // Clamp TotalDamage to 0 if it is negative
                    if (ld.TotalDamage <= 0)
                    {
                        ld.IsInjured = false;
                        ld.TotalDamage = 0;
                    }
                }

                int woundedLimbsCount = this.DamagedLimbs.Count(x => x.Value.IsInjured);
                if (woundedLimbsCount == 0)
                {
                    this.HasInjuries = false;
                    OnAllInjuriesHealed?.Invoke();
                }
            }
        }

        public void ApplyLimbDamage(BoneBodyPartType bodyPartType, int damage)
        {
            BodyPartStatus bodyPartStatus = new BodyPartStatus();
            if (!DamagedLimbs.ContainsKey(bodyPartType))
            {
                bodyPartStatus.TotalDamage = damage;
                bodyPartStatus.IsInjured = damage >= _injuredCapDamage;
                this.HasInjuries = bodyPartStatus.IsInjured;

                DamagedLimbs[bodyPartType] = bodyPartStatus;
            }
            else
            {
                if (DamagedLimbs[bodyPartType].TotalDamage < _injuredCapDamage && !DamagedLimbs[bodyPartType].IsInjured)
                {
                    DamagedLimbs[bodyPartType].TotalDamage += damage;
                    DamagedLimbs[bodyPartType].IsInjured = DamagedLimbs[bodyPartType].TotalDamage >= _injuredCapDamage;

                    if (DamagedLimbs[bodyPartType].IsInjured)
                    {
                        this.HasInjuries = true;
                    }
                }
            }
            if(DamagedLimbs[bodyPartType].IsInjured && !DamagedLimbs[bodyPartType].PenaltyApplied)
            { 
                OnInjuryApplied?.Invoke(bodyPartType);
                this.DamagedLimbs[bodyPartType].PenaltyApplied = true;
            }
        }
        
        public string GetInjuryDescriptions()
        {
            StringBuilder infoBuilder = new();
            var keys = this.DamagedLimbs.Keys;
        
            foreach (BoneBodyPartType bodyPart in keys)
            {
                BodyPartStatus ld = this.DamagedLimbs[bodyPart];
                string injuredText = ld.IsInjured ? "Yes" : "No";
                infoBuilder.AppendLine($"{bodyPart.ToString()}: {ld.TotalDamage}\nInjured: {injuredText}\n");
            }
            return infoBuilder.ToString();
        } 
    }
}
