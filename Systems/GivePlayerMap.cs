using Kitchen;
using Kitchen.Layouts;
using KitchenData;
using KitchenLib.References;
using KitchenMods;
using LayoutBuilder.Utility;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace LayoutBuilder.Systems
{
    public class GivePlayerMap : GameSystemBase, IModSystem
    {
        private EntityQuery _players;
        public static GivePlayerMap instance;
        protected override void Initialise()
        {
            base.Initialise();
            _players = GetEntityQuery(typeof(CPlayer));
            instance = this;
        }

        public void GivePlayerNewMap(LayoutGraph graph)
        {
            using NativeArray<Entity> players = _players.ToEntityArray(Allocator.Temp);
            if (players.Length < 0) return;
            Entity player = players[0];
                
            if (Require(player, out CItemHolder cItemHolder))
            {
                if (cItemHolder.HeldItem != Entity.Null)
                    EntityManager.DestroyEntity(cItemHolder.HeldItem);

                if (Utils.IsSeeded)
                    GenerationUtils.FixedSeed = new Seed(Utils.Seed);
                else
                    GenerationUtils.FixedSeed = Seed.Generate(Random.Range(int.MinValue, int.MaxValue));
                    
                using (FixedSeedContext fixedSeedContext = new FixedSeedContext(GenerationUtils.FixedSeed, 98234234))
                {
                    using (fixedSeedContext.UseSubcontext(0))
                    {
                        int seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
                        if (GenerationUtils.GenerateLayoutBlueprint(Utils.selectedProfile, Utils.selectedSetting, graph, seed, out LayoutDecorator layoutDecorator, out LayoutBlueprint layoutBlueprint))
                        {
                            if (GenerationUtils.ConvertLayoutToMapEntity(EntityManager, Utils.selectedSetting, layoutDecorator, layoutBlueprint, out Entity map))
                            {
                                EntityManager.AddComponentData(map, new CHeldBy
                                {
                                    Holder = player
                                });                
                                    
                                cItemHolder.HeldItem = map;
                                EntityManager.SetComponentData(player, cItemHolder);
                            }
                        }
                    }
                }
            }
        }

        protected override void OnUpdate()
        {
            if (!Input.GetKey(KeyCode.Delete)) return;
            
            if (Input.GetKeyDown(KeyCode.J))
            {
                using NativeArray<Entity> players = _players.ToEntityArray(Allocator.Temp);
                if (players.Length < 0) return;
                Entity player = players[0];
                
                if (Require(player, out CItemHolder cItemHolder))
                {
                    if (cItemHolder.HeldItem != Entity.Null)
                        EntityManager.DestroyEntity(cItemHolder.HeldItem);

                    if (Utils.IsSeeded)
                        GenerationUtils.FixedSeed = new Seed(Utils.Seed);
                    else
                        GenerationUtils.FixedSeed = Seed.Generate(Random.Range(int.MinValue, int.MaxValue));
                    
                    using (FixedSeedContext fixedSeedContext = new FixedSeedContext(GenerationUtils.FixedSeed, 98234234))
                    {
                        using (fixedSeedContext.UseSubcontext(0))
                        {
                            int seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
                            if (GenerationUtils.GenerateLayoutBlueprint(Utils.selectedProfile, Utils.selectedSetting, GameData.Main.Get<LayoutProfile>(LayoutProfileReferences.WitchLayout).Graph, seed, out LayoutDecorator layoutDecorator, out LayoutBlueprint layoutBlueprint))
                            {
                                if (GenerationUtils.ConvertLayoutToMapEntity(EntityManager, Utils.selectedSetting, layoutDecorator, layoutBlueprint, out Entity map))
                                {
                                    EntityManager.AddComponentData(map, new CHeldBy
                                    {
                                        Holder = player
                                    });                
                                    
                                    cItemHolder.HeldItem = map;
                                    EntityManager.SetComponentData(player, cItemHolder);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}