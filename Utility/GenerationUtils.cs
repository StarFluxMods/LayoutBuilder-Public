using Kitchen;
using Kitchen.Layouts;
using KitchenData;
using LayoutBuilder.Patches;
using Unity.Entities;
using UnityEngine;

namespace LayoutBuilder.Utility
{
    public static class GenerationUtils
    {

        public static Seed FixedSeed;

        public static int MaxGenerationAttempts = 100;
        
        public static bool GenerateLayoutBlueprint(LayoutProfile profile, RestaurantSetting setting, LayoutGraph graph, int seed, out LayoutDecorator layoutDecorator, out LayoutBlueprint layoutBlueprint)
        {
            layoutDecorator = null;
            layoutBlueprint = null;
            bool flag = false;
            for (int i = 0; i < MaxGenerationAttempts; i++)
            {
                try
                {
                    layoutBlueprint = graph.Build(seed + i);
                    layoutDecorator = new LayoutDecorator(layoutBlueprint, profile, setting);
                    layoutDecorator.AttemptDecoration();
                    flag = true;
                    break;
                }
                catch (LayoutFailureException)
                {
                }
            }
            if (!flag || ((layoutDecorator != null) ? layoutDecorator.Decorations : null) == null)
            {
                Debug.LogError(string.Format("Giving up after {0} attempts", MaxGenerationAttempts));
                return false;
            }
            return true;
        }

        public static bool ConvertLayoutToMapEntity(EntityManager entityManager, RestaurantSetting setting, LayoutDecorator layoutDecorator, LayoutBlueprint layoutBlueprint, out Entity map)
        {
            Entity entity = entityManager.CreateEntity(new ComponentType[]
            {
                typeof(CStartingItem),
                typeof(CLayoutRoomTile),
                typeof(CLayoutFeature),
                typeof(CLayoutAppliancePlacement)
            });
            layoutBlueprint.ToEntity(entityManager, entity);
            DynamicBuffer<CLayoutAppliancePlacement> buffer = entityManager.GetBuffer<CLayoutAppliancePlacement>(entity);
            foreach (CLayoutAppliancePlacement elem in layoutDecorator.Decorations)
            {
                buffer.Add(elem);
            }
            
            map = entityManager.CreateEntity(new ComponentType[]
            {
                typeof(CCreateItem),
                typeof(CHeldBy),
                typeof(CHome)
            });
            entityManager.SetComponentData<CCreateItem>(map, new CCreateItem
            {
                ID = AssetReference.MapItem
            });
            entityManager.AddComponentData<CItemLayoutMap>(map, new CItemLayoutMap
            {
                Layout = entity
            });
            entityManager.AddComponentData<CSetting>(map, new CSetting
            {
                RestaurantSetting = setting.ID,
                FixedSeed = FixedSeed
            });
            entityManager.AddComponent<CShowSeed>(map);

            return true;
        }

        public static bool ConvertLayoutToScreenshot(LayoutBlueprint layout, out Texture2D texture)
        {
            texture = PrefabSnapshot.GetLayoutSnapshot(LayoutSampleViewPatch.view, layout);
            return true;
        }
    }
}