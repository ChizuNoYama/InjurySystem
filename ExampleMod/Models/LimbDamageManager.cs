using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.MountAndBlade;

namespace ExampleMod.Models
{
    internal class LimbDamageManager
    {
        private LimbDamageManager()
        {
            this.DamagedLimbs = new();
        }

        private const int WoundDamageCap = 15;
        
        public Action<BoneBodyPartType, float>? OnWoundApplied { get; set; }
        public Action? OnAllInjuriesHealed { get; set; }
        public Dictionary<BoneBodyPartType, LimbDamage> DamagedLimbs { get; private set; }
        public static LimbDamageManager? Instance { get; set; }

        public static void Initialize(Dictionary<BoneBodyPartType, LimbDamage>? damagedLimbs = null )
        {
            Instance = new LimbDamageManager()
            {
                DamagedLimbs = damagedLimbs != null ? damagedLimbs : new Dictionary<BoneBodyPartType, LimbDamage>()
            };
        }

        public void ResetLimbDamage(BoneBodyPartType bodyPartType)
        {
            if (this.DamagedLimbs.ContainsKey(bodyPartType))
            {
                this.DamagedLimbs[bodyPartType].TotalDamage = 0;
            }
        }

        public void ApplyHealingToInjuries()
        {
            foreach (KeyValuePair<BoneBodyPartType,LimbDamage>  injury in this.DamagedLimbs)
            {
                LimbDamage ld = injury.Value;
                if (ld.IsWounded)
                {
                    ld.TotalDamage -= 3;
                }
                else
                {
                    ld.TotalDamage -= 1;
                }
                
                int woundedLimbsCount = DamagedLimbs.Where(x => x.Value.IsWounded).Count();
                if(woundedLimbsCount == 0)
                {
                    this.OnAllInjuriesHealed?.Invoke();
                }
            }
        }

        public void ApplyLimbDamage(BoneBodyPartType bodyPartType, int damage)
        {
            if (!this.DamagedLimbs.ContainsKey(bodyPartType))
            {
                LimbDamage limb = new LimbDamage() 
                {
                    TotalDamage = damage,
                    IsWounded = damage >= WoundDamageCap
                };
                
                this.DamagedLimbs[bodyPartType] = limb;
            }
            else
            {
                if (this.DamagedLimbs[bodyPartType].TotalDamage < WoundDamageCap)
                {
                    this.DamagedLimbs[bodyPartType].TotalDamage += damage;
                    this.DamagedLimbs[bodyPartType].IsWounded = damage >= WoundDamageCap;
                }
            }

            if (this.DamagedLimbs[bodyPartType].IsWounded)
            {
                this.DamagedLimbs[bodyPartType].IsWounded = true;
                float skillPenalty = 100;
                
                this.OnWoundApplied?.Invoke(bodyPartType, skillPenalty);
            }
        }
    }
}
