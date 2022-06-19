using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using System;
using ExampleMod.Components;
using TaleWorlds.Library;
using ExampleMod.Utils;
using ExampleMod.Behaviors;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using ExampleMod.Models;
using HarmonyLib;

namespace ExampleMod
{
    public class Test : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            Module.CurrentModule.AddInitialStateOption(new InitialStateOption(id: "Message",
                                                                              name: new TextObject("Message", null),
                                                                              orderIndex: 9990,
                                                                              action: () => { InformationManager.DisplayMessage(new InformationMessage("Hello World!")); },
                                                                              isDisabledAndReason: () => { return (false, null); }));

            //string harmonyID = this.GetType().Assembly.FullName;
            //var harmony = new Harmony(harmonyID);

            LimbDamageManager.Initialize();
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            if(game.GameType is Campaign)
            {
                (gameStarterObject as CampaignGameStarter)?.AddBehavior(new SaveBehavior());
                //(gameStarterObject as CampaignGameStarter)?.AddBehavior(new WoundHealingBehavior());
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
            // I hate this nullable stuff, but I also hate handling Exceptions
            Mission? mission = sender as Mission;
            if (mission?.MainAgent != null && mission.MainAgent.IsHero)
            {
                Hero? hero = (mission.MainAgent.Character as CharacterObject)?.HeroObject;
                if(hero != null)
                {
                    mission.MainAgent.AddComponentIfNotExisting(new WoundedAgentComponent(mission.MainAgent));
                    WoundedHeroDeveloper woundDeveloper = new WoundedHeroDeveloper(hero.HeroDeveloper);
                    hero.SetHeroDeveloper(woundDeveloper);
                }
            }
        }
    }
}
