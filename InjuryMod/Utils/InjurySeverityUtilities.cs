using InjuryMod.Models;
using TaleWorlds.CampaignSystem.GameMenus.GameMenuInitializationHandlers;

namespace InjuryMod.Utils;

public static class InjurySeverityUtilities
{
    public static float GetPenalty(InjurySeverity severity)
    {
        switch (severity)
        {
            case InjurySeverity.Bruised:
                return 0.125f;
            case InjurySeverity.Sprained:
                return 0.25f;
            case InjurySeverity.Fractured:
                return 0.375f;
            case InjurySeverity.Broken:
                return 0.50f;
            case InjurySeverity.Mangled:
                return 0.75f;
            case InjurySeverity.None:
            default:
                return 0f;
                
        }
    }

    public static InjurySeverity GetSeverity(int totalLimbDamage, int damageCap)
    {
        float percentageOfLimbDamage = totalLimbDamage / (float)damageCap;

        if (totalLimbDamage == 0)
        {
            return InjurySeverity.None;
        }

        if (percentageOfLimbDamage > 0.01 &&percentageOfLimbDamage <= 0.25)
        {
            return InjurySeverity.Bruised;
        }

        if (percentageOfLimbDamage > 0.25 && percentageOfLimbDamage <= 0.5)
        {
            return InjurySeverity.Sprained;
        }

        if (percentageOfLimbDamage > 0.5 && percentageOfLimbDamage <= 0.75)
        {
            return InjurySeverity.Fractured;
        }

        if (percentageOfLimbDamage > 0.75 && percentageOfLimbDamage < 1)
        {
            return InjurySeverity.Broken;
        }

        return InjurySeverity.Mangled;

    }
}