using ExampleMod.Models;
using ExampleMod.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.Localization;

namespace ExampleMod.Behaviors;

public class InjuryInfoBehavior : CampaignBehaviorBase
{
    private const string _townMenuOptionId = "town_injuries";
    private const string _townMenuOptionText = "Assess Injuries";
    private const string _injuryMenuId = "injury_info";
    private const string _injuryMenuOptionId = "injury_info_back";
    
    public override void RegisterEvents()
    {
        CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, this.AddMenuItems);
    }

    public override void SyncData(IDataStore dataStore)
    {
        // Do nothing
    }

    private void AddMenuItems(CampaignGameStarter gameStarter)
    {
        // TODO: Check if this is working correctly. MEnu option i snot showing up on the town screen
        gameStarter.AddGameMenuOption(menuId: "town", optionId: _townMenuOptionId, optionText: _townMenuOptionText, condition: this.Condition, consequence: this.AssessInjuriesOnConsequence);
        gameStarter.AddGameMenu(menuId:_injuryMenuId, menuText: "Assessing Injuries\n{INJURY_INFO}", initDelegate: InjuryInfoBehavior.InjuryMenuOnInit);
        gameStarter.AddGameMenuOption(menuId:_injuryMenuId, optionId: _injuryMenuOptionId, optionText: "Done", condition: null, consequence: InjuryMenuOptionBackOnConsequence);
    }

    private static void InjuryMenuOnInit(MenuCallbackArgs args)
    {
        WoundLogger.DebugLog($"{args.MenuContext.GameMenu.MenuTitle}");
        TextObject text = args.MenuContext.GameMenu.GetText();
        text.SetTextVariable("INJURY_INFO", LimbDamageManager.Instance!.GetInjuryDescriptions());

    }
    private void InjuryMenuOptionBackOnConsequence(MenuCallbackArgs args)
    {
        GameMenu.SwitchToMenu("town");
    }

    private void AssessInjuriesOnConsequence(MenuCallbackArgs args)
    {
        GameMenu.SwitchToMenu(_injuryMenuId);
    }

    private bool Condition(MenuCallbackArgs args)
    {
        // return LimbDamageManager.Instance.HasInjuries;
        
        // TODO: Always return true for now
        return true;
    }
}