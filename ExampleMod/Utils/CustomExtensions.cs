using TaleWorlds.MountAndBlade;

namespace ExampleMod.Utils
{
    internal static class CustomExtensions
    {
        internal static void AddComponentIfNotExisting<T>(this Agent agent, T agentComponent) where T : AgentComponent
        {
            AgentComponent mainAgentComponent = agent.GetComponent<T>();
            if (mainAgentComponent == null)
            {
                agent.AddComponent(agentComponent);
            }
        }

        internal static void AddMissionBehaviorIfNotExisting<T>(this Mission mission, T missionBehavior) where T : MissionBehavior
        {
            T behavior = mission.GetMissionBehavior<T>();
            if (behavior == null)
            {
                mission.AddMissionBehavior(missionBehavior);
            }
        }
    }
}
