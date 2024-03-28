using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TaleWorlds.Library;
using InjuryMod.Utils;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using HarmonyLib;
using InjuryMod.Behaviors;
using InjuryMod.Components;
using InjuryMod.Models;
using TaleWorlds.ObjectSystem;

namespace InjuryMod
{
    public class InjuryMod : MBSubModuleBase
    {
        public const string HarmonyId = "com.chiz.injury";
        
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            
            InitHarmony();
            LimbDamageManager.Initialize();
            WoundLogger.DebugLog("InjuryMod Loaded");
        }

        public static void InitHarmony()
        {
            Harmony.DEBUG = true;
            Harmony harmony = new Harmony(HarmonyId);
            // harmony.PatchCategory(Assembly.GetAssembly(typeof(InjuryMod)), "AgentDrivenPropertyPatch");
            harmony.PatchAll();
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);
            if(game.GameType is Campaign)
            {
                CampaignGameStarter starter = (CampaignGameStarter)gameStarterObject;
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
            Mission? mission = sender as Mission;
            if (mission?.MainAgent != null && mission.MainAgent.IsHero)
            {
                Hero? hero = (mission.MainAgent.Character as CharacterObject)?.HeroObject;
                if(hero != null && hero.IsHumanPlayerCharacter)
                {
                    WoundedAgentComponent component = new WoundedAgentComponent(mission.MainAgent);
                    mission.MainAgent.AddComponentIfNotExisting(component);
                    // ObjectUtilities.PlayerAgentProperties = mission.MainAgent.AgentDrivenProperties;

                }
            }
        }
    }
}
