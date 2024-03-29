using System;
using System.Collections.Generic;
using System.IO;
using GUINodeEditor;
using Kitchen;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenData;
using KitchenLib.Preferences;
using LayoutBuilder.Editors.Nodes;
using UnityEngine;

namespace LayoutBuilder.Utility
{
    public enum MenuColor
    {
        Black,
        White,
        Yellow,
        Blue,
        Clear,
        Cyan,
        Gray,
        Green,
        Magenta,
        Red
    }

    public static class Utils
    {
        public static Dictionary<int, Color> MenuColorToColor = new Dictionary<int, Color>
        {
            { 0, Color.black },
            { 1, Color.white },
            { 2, Color.yellow },
            { 3, Color.blue },
            { 4, Color.clear },
            { 5, Color.cyan },
            { 6, Color.gray },
            { 7, Color.green },
            { 8, Color.magenta },
            { 9, Color.red }
        };

        public static RestaurantSetting selectedSetting;
        public static LayoutProfile selectedProfile;

        
        public static MenuColor MenuColor
        {
            get => (MenuColor)Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value;
            set => Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Set((int)value);
        }

        public static MenuColor temp;

        public static bool IsSeeded;
        public static string Seed = "";
        public static bool ExtendedScreenshotCount = false;
        public static int ScreenshotCount = 1;
        public static string GraphName = "";
        
        public static Dictionary<Type, Type> keyPairValues = new Dictionary<Type, Type>()
        {
            {typeof(AddRoom), typeof(Node_AddRoom)},
            {typeof(AppendFeatures), typeof(Node_AppendFeatures)},
            {typeof(CreateFrontDoor), typeof(Node_CreateFrontDoor)},
            {typeof(CreateRoomByJoins), typeof(Node_CreateRoomByJoins)},
            {typeof(FilterAdjacentPair), typeof(Node_FilterAdjacentPair)},
            {typeof(FilterByFreeSpace), typeof(Node_FilterByFreeSpace)},
            {typeof(FilterByRoom), typeof(Node_FilterByRoom)},
            {typeof(FilterOnePerPair), typeof(Node_FilterOnePerPair)},
            {typeof(FilterOppositePair), typeof(Node_FilterOppositePair)},
            {typeof(FilterSelectCount), typeof(Node_FilterSelectCount)},
            {typeof(FindAllFeatures), typeof(Node_FindAllFeatures)},
            {typeof(FullyConnect), typeof(Node_FullyConnect)},
            {typeof(InsertRandomRoom), typeof(Node_InsertRandomRoom)},
            {typeof(MergeRoomsByType), typeof(Node_MergeRoomsByType)},
            {typeof(Mirror), typeof(Node_Mirror)},
            {typeof(MoveFeatureInDirection), typeof(Node_MoveFeatureInDirection)},
            {typeof(Output), typeof(Node_Output)},
            {typeof(PadWithRoom), typeof(Node_PadWithRoom)},
            {typeof(RecentreLayout), typeof(Node_RecentreLayout)},
            {typeof(ReplaceFeaturesByRoomType), typeof(Node_ReplaceFeaturesByRoomType)},
            {typeof(RoomGrid), typeof(Node_RoomGrid)},
            {typeof(SetLine), typeof(Node_SetLine)},
            {typeof(SetRoom), typeof(Node_SetRoom)},
            {typeof(SplitLine), typeof(Node_SplitLine)},
            {typeof(SplitRooms), typeof(Node_SplitRooms)},
            {typeof(SwapRoomType), typeof(Node_SwapRoomType)},
            {typeof(SwitchFeatures), typeof(Node_SwitchFeatures)},
            {typeof(RequireAccessible), typeof(Node_RequireAccessible)},
            {typeof(RequireDoorPair), typeof(Node_RequireDoorPair)},
            {typeof(RequireFeatureCountEven), typeof(Node_RequireFeatureCountEven)},
            {typeof(RequireFeatures), typeof(Node_RequireFeatures)},
            {typeof(RequireNoOverlaps), typeof(Node_RequireNoOverlaps)},
        };

        public static Texture2D rotateTexture(Texture2D originalTexture, bool clockwise)
        {
            Color32[] original = originalTexture.GetPixels32();
            Color32[] rotated = new Color32[original.Length];
            int w = originalTexture.width;
            int h = originalTexture.height;

            int iRotated, iOriginal;

            for (int j = 0; j < h; ++j)
            {
                for (int i = 0; i < w; ++i)
                {
                    iRotated = (i + 1) * h - j - 1;
                    iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                    rotated[iRotated] = original[iOriginal];
                }
            }

            Texture2D rotatedTexture = new Texture2D(h, w);
            rotatedTexture.SetPixels32(rotated);
            rotatedTexture.Apply();
            return rotatedTexture;
        }
        
        public static void GenerateScreenshots(NodeEditor nodeEditor, int count, string saveDirectory = $"%persistentdatapath%\\UserData\\{Mod.MOD_NAME}\\Screenshots", string saveFile = "%graph% - %seed%")
        {
            for (int i = 0; i < count; i++)
            {
                if (Utils.IsSeeded)
                {
                    GenerationUtils.FixedSeed = new Seed(Utils.Seed);
                }
                else
                {
                    int random = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
                    GenerationUtils.FixedSeed = Kitchen.Seed.Generate(random);
                }
                
                using (FixedSeedContext fixedSeedContext = new FixedSeedContext(GenerationUtils.FixedSeed, 98234234))
                {
                    using (fixedSeedContext.UseSubcontext(0))
                    {
                        int seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);

                        if (!ConversionUtils.ConvertRuntimeToXNode(nodeEditor, out LayoutGraph graph)) continue;
                        if (!GenerationUtils.GenerateLayoutBlueprint(Utils.selectedProfile, Utils.selectedSetting, graph, seed, out LayoutDecorator layoutDecorator, out LayoutBlueprint layoutBlueprint)) continue;
                        if (!GenerationUtils.ConvertLayoutToScreenshot(layoutBlueprint, out Texture2D texture)) continue;
                        
                        string _saveDirectory = saveDirectory;
                        
                        _saveDirectory = _saveDirectory.Replace("%profile%", selectedProfile.name);
                        _saveDirectory = _saveDirectory.Replace("%setting%", selectedSetting.name);
                        _saveDirectory = _saveDirectory.Replace("%graph%", string.IsNullOrEmpty(graph.name) ? "Graph" : graph.name);
                        _saveDirectory = _saveDirectory.Replace("%seed%", seed.ToString());
                        _saveDirectory = _saveDirectory.Replace("%datapath%", Application.dataPath);
                        _saveDirectory = _saveDirectory.Replace("%persistentdatapath%", Application.persistentDataPath);
                        _saveDirectory = _saveDirectory.Replace("%desktop%", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                        _saveDirectory = _saveDirectory.Replace("%documents%", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

                        texture = Utils.rotateTexture(texture, false);
                        if (!Directory.Exists(_saveDirectory))
                        {
                            Directory.CreateDirectory(_saveDirectory);
                        }

                        string _saveFile = saveFile;
                        
                        _saveFile = _saveFile.Replace("%profile%", selectedProfile.name);
                        _saveFile = _saveFile.Replace("%setting%", selectedSetting.name);
                        _saveFile = _saveFile.Replace("%graph%", string.IsNullOrEmpty(graph.name) ? "Graph" : graph.name);
                        _saveFile = _saveFile.Replace("%seed%", seed.ToString());
                        
                        if (!_saveFile.EndsWith(".png"))
                            _saveFile += ".png";
                        
                        File.WriteAllBytes(Path.Combine(_saveDirectory, _saveFile), texture.EncodeToPNG());
                    }
                }
            } 
        }
    }
}