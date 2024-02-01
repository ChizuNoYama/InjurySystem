using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InjuryMod.Utils;
using TaleWorlds.MountAndBlade;
using TaleWorlds.TwoDimension;

namespace InjuryMod.Models;

internal class LimbDamageManager
{
    private int _injuredCapDamage = 100;
        
    private LimbDamageManager()
    {
        DamagedLimbs = new Dictionary<BoneBodyPartType, BodyPartStatus>();
    }
        
    public Action<BoneBodyPartType>? OnInjuryApplied { get; set; }
    public Action? OnAllInjuriesHealed { get; set; }
    public Dictionary<BoneBodyPartType, BodyPartStatus> DamagedLimbs { get; private set; }
    public static LimbDamageManager? Instance { get; private set; }

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
            limb.Severity = InjurySeverityUtilities.GetSeverity(limb.TotalDamage, _injuredCapDamage);
        }
    }

    // TODO: Input healing amount and calculate how much based on amount healed from party Surgeon
    public void ApplyHealingToInjuries(int healingAmount)
    {
        foreach (KeyValuePair<BoneBodyPartType,BodyPartStatus>  damage in DamagedLimbs)
        {
            BodyPartStatus ld = damage.Value;
            ld.TotalDamage -= healingAmount;

            // Clamp TotalDamage to 0 if it is negative
            if (ld.TotalDamage < 0)
            {
                ld.TotalDamage = 0;
            }

            ld.Severity = InjurySeverityUtilities.GetSeverity(ld.TotalDamage, _injuredCapDamage);
        }

        int woundedLimbsCount = this.DamagedLimbs.Count(x => x.Value.IsInjured);
        if (woundedLimbsCount == 0)
        {
            OnAllInjuriesHealed?.Invoke();
        }
    }

    public void ApplyLimbDamage(BoneBodyPartType bodyPartType, int limbDamage)
    {
        if (!DamagedLimbs.ContainsKey(bodyPartType))
        {
            DamagedLimbs[bodyPartType] = new BodyPartStatus
            {
                TotalDamage = limbDamage
            };
        }
        else
        {
            if (DamagedLimbs[bodyPartType].Severity !=  InjurySeverity.Mangled)
            {
                DamagedLimbs[bodyPartType].TotalDamage += limbDamage;
            }
        }
            
        if (this.DamagedLimbs[bodyPartType].TotalDamage > _injuredCapDamage)
        {
            this.DamagedLimbs[bodyPartType].TotalDamage = _injuredCapDamage;
        }
            
        //Clamp the value if it is over the cap
        this.DamagedLimbs[bodyPartType].Severity = InjurySeverityUtilities.GetSeverity(this.DamagedLimbs[bodyPartType].TotalDamage, _injuredCapDamage);
    }

    public int CalculateLimbDamage(int agentDamageTaken, int enduranceLevel, int maxLevelEnduranceLevel)
    {
        if (enduranceLevel != maxLevelEnduranceLevel)
        {
            int limbDamage = (int)(agentDamageTaken * ((maxLevelEnduranceLevel - enduranceLevel) / (float)maxLevelEnduranceLevel));
            WoundLogger.DebugLog($"Agent damage taken: {agentDamageTaken}\nLimb damage taken: {limbDamage}\n");
            return limbDamage;
        }
        return 2;
    }
        
    public string GetInjuryDescriptions()
    {
        StringBuilder infoBuilder = new();
        var keys = this.DamagedLimbs.Keys;
        
        foreach (BoneBodyPartType bodyPart in keys)
        {
            BodyPartStatus ld = this.DamagedLimbs[bodyPart];
            string injuredText = ld.IsInjured ? ld.Severity.ToString() : "No";
            infoBuilder.AppendLine($"{bodyPart.ToString()}: {ld.TotalDamage}\nSeverity: {injuredText}\n");
        }
        return infoBuilder.ToString();
    } 
}