using ExampleMod.Models;
using ExampleMod.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.MountAndBlade;

namespace ExampleMod.Behaviors
{
    internal class SaveBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
        }

        private const string _saveDataKey = "wound_system_data";

        public override void SyncData(IDataStore dataStore)
        {
            if (dataStore.IsSaving)
            {
                string jsonString = JsonConvert.SerializeObject(LimbDamageManager.Instance?.DamagedLimbs);

                dataStore.SyncData(_saveDataKey, ref jsonString);
                WoundLogger.DebugLog($"=====> Saving to the store: {jsonString}");
            }
            if (dataStore.IsLoading)
            {
                string jsonString = "";
                dataStore.SyncData(_saveDataKey, ref jsonString);
                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    Dictionary<BoneBodyPartType, LimbDamage> data = JsonConvert.DeserializeObject<Dictionary<BoneBodyPartType, LimbDamage>>(jsonString)!;
                    LimbDamageManager.Initialize(data);
                }
                else
                {
                    LimbDamageManager.Initialize();
                }

                WoundLogger.DebugLog($"=====> Load data from the store: {LimbDamageManager.Instance != null}");
            }
        }
    }
}
