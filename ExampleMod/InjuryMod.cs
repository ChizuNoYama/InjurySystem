using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using System;
using System.Linq;
using ExampleMod.Components;
using TaleWorlds.Library;
using ExampleMod.Utils;
using ExampleMod.Behaviors;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using ExampleMod.Models;
using HarmonyLib;
using TaleWorlds.ObjectSystem;

namespace ExampleMod
{
    public class InjuryMod : MBSubModuleBase
    {
        public const string HarmonyId = "com.chiz.injury";
        
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            Module.CurrentModule.AddInitialStateOption(new InitialStateOption(id: "Message",
                                                                              name: new TextObject("Message", null),
                                                                              orderIndex: 9990,
                                                                              action: () => { InformationManager.DisplayMessage(new InformationMessage("Hello World!")); },
                                                                              isDisabledAndReason: () => (false, null)));
            
            InitHarmony();
            LimbDamageManager.Initialize();
        }

        public static void InitHarmony()
        {
            Harmony harmony = new Harmony(HarmonyId);
            harmony.PatchAll();
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);
            if(game.GameType is Campaign)
            {
                CampaignGameStarter starter = (CampaignGameStarter)gameStarterObject;
                InjuryPenaltyManager.Initialize();
                starter.AddBehavior(new SaveBehavior());
                starter.AddBehavior(new InjuryHealingBehavior());
                starter.AddBehavior(new InjuryInfoBehavior());
            }
        }

        public override void OnBeforeMissionBehaviorInitialize(Mission mission)
        {
            base.OnBeforeMissionBehaviorInitialize(mission);
            mission.AddMissionBehaviorIfNotExisting(new WoundMissionLogic());
        }

        public override void OnMissionBehaviorInitialize(Mission mission)
        {
            base.OnMissionBehaviorInitialize(mission);

            mission.OnMainAgentChanged -= OnMissionMainAgentChanged;
            mission.OnMainAgentChanged += OnMissionMainAgentChanged;
        }

        private void OnMissionMainAgentChanged(object sender, EventArgs args)
        {
            // I hate this nullable stuff, but I also hate handling NRE's
            Mission? mission = sender as Mission;
            if (mission?.MainAgent != null && mission.MainAgent.IsHero)
            {
                Hero? hero = (mission.MainAgent.Character as CharacterObject)?.HeroObject;
                if(hero != null && hero.IsHumanPlayerCharacter)
                {
                    WoundedAgentComponent component = new WoundedAgentComponent(mission.MainAgent);
                    mission.MainAgent.AddComponentIfNotExisting(component);
                    
                }
            }
        }
    }
}
