using HarmonyLib;
using Kitchen;

namespace LayoutBuilder.Patches
{
    [HarmonyPatch(typeof(LayoutSampleView), "UpdateBlueprint")]
    public class LayoutSampleViewPatch
    {
        public static SiteView view;
        static void Prefix(LayoutSampleView __instance)
        {
            view = __instance.DisplayPrefab;
        }
    }
}