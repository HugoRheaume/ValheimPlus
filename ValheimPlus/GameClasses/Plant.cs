using System.Collections.Generic;
using System;
using HarmonyLib;
using UnityEngine;
using System.Linq;
using ValheimPlus.Configurations;

namespace ValheimPlus.GameClasses
{
    [HarmonyPatch(typeof(Plant), nameof(Plant.Awake))]
    public static class Plant_Awake_Patch
    {
        private static Dictionary<string, float> t = new Dictionary<string, float>() {{
            "$item_beechseeds", Configuration.Current.Plant.beechSeeds
        }};

        private static void Postfix(ref Plant __instance)
        {
            if (!Configuration.Current.Plant.IsEnabled) return;



            // __instance.m_growTime = 100;
            /*if (__instance.m_name == "$prop_beech_sapling")
            {
                __instance.m_growTime = 100f;
                __instance.m_growTimeMax = __instance.m_growTime;

                 Debug.Log($"Name: {__instance.m_name} Grow Time: {__instance.m_growTime}");
            }*/
        }

        /*[HarmonyPostfix]
        [HarmonyPatch(typeof(Plant), nameof(Plant.GetHoverText))]
        public static string PlantGetHoverText_Patch(string __result, Plant __instance)
        {
            if (__instance == null) return __result;

            double percentage = Mathf.Floor((float)__instance.TimeSincePlanted() / (float)__instance.GetGrowTime() * 100);
           // string colour = GetColour(percentage);
            string growPercentage = $"<color=blue>{decimal.Round((decimal)percentage, 2)}%</color>";

            return __result.Replace(" )", $", {growPercentage} )");
        }*/
    }

    [HarmonyPatch(typeof(Plant), nameof(Plant.GetHoverText))]
    public static class Plant_GetHoverText_Patch {
        private static string Postfix(string __result, Plant __instance)
        {
            if (__instance == null || !Configuration.Current.Plant.IsEnabled || !Configuration.Current.Plant.showDuration)
                return __result;
            
            if (!PrivateArea.CheckAccess(__instance.transform.position, 0f, false))
                return Localization.instance.Localize(__instance.m_name + "\n$piece_noaccess");            

            float percentage = Mathf.Floor((float)__instance.TimeSincePlanted() / __instance.GetGrowTime() * 100);
            return __result.Replace(" )", $", {percentage}% )");
        }
    }

   /* [HarmonyPatch(typeof(Pickable), "Awake")]
            static class Pickable_Awake_Patch
            {
                static void Postfix(ref Pickable __instance)
                {
                    if (!dropRateEnabled.Value)
                        return;
                    string name = __instance.GetHoverName();
                    if (name == "$item_turnipseeds")
                    {
                        __instance.m_amount = SeedTurnipDrop.Value;
                        logger.LogInfo($"Set: {name} to drop: {SeedTurnipDrop.Value}");
                    }
                    if (name == "$item_turnip")
                    {
                        __instance.m_amount = TurnipDrop.Value;
                        logger.LogInfo($"Set: {name} to drop: {TurnipDrop.Value}");
                    }
                    if (name == "$item_carrotseeds")
                    {
                        __instance.m_amount = SeedCarrotDrop.Value;
                        logger.LogInfo($"Set: {name} to drop: {SeedCarrotDrop.Value}");
                    }
                    if (name == "$item_carrot")
                    {
                        __instance.m_amount = CarrotDrop.Value;
                        logger.LogInfo($"Set: {name} to drop: {CarrotDrop.Value}");
                    }

                    if (name == "$item_barley")
                    {
                        __instance.m_amount = BarleyDrop.Value;
                        logger.LogInfo($"Set: {name} to drop: {BarleyDrop.Value}");
                    }
                    if (name == "$item_flax")
                    {
                        __instance.m_amount = FlaxDrop.Value;
                        logger.LogInfo($"Set: {name} to drop: {FlaxDrop.Value}");
                    }

                }
            }*/

    public static class PlantType {
        // Seeds
        // https://valheim-modding.github.io/Jotunn/data/objects/item-list.html
        public static string Acorn = "$item_oakseeds";
        public static string AncientSeed = "$item_ancientseed";
        public static string BeechSeeds = "$item_beechseeds";
        public static string BirchSeeds = "$item_birchseeds";
        public static string CarrotSeeds = "$item_carrotseeds";
        public static string OnionSeeds = "$item_onionseeds";
        public static string TurnipSeeds = "$item_turnipseeds";
    }
}
