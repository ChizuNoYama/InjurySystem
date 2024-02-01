using TaleWorlds.Library;
using Debug = System.Diagnostics.Debug;

namespace InjuryMod.Utils
{
    internal static class WoundLogger
    {
        internal static void DisplayMessage(string message, string colorHex = "#FFFFFFFF")
        {
            InformationManager.DisplayMessage(new InformationMessage(message, Color.ConvertStringToColor(colorHex)));
        }

        internal static void DebugLog(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
