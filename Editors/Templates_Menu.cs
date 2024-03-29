using UnityEngine;
using GUINodeEditor;
using KitchenData;
using KitchenLib.References;
using LayoutBuilder.Utility;

namespace LayoutBuilder.Editors
{
    public class Templates_Menu : Node
    {

        public override void Init(Vector2 position)
        {
            Init (position, nodeWindow: new Templates_Menu_Window(), title: "Load Template");
        }
    }

    public class Templates_Menu_Window : WindowModule
    {
        private static int[] defaultSettingIDs = new[]
        {
            RestaurantSettingReferences.City,
            RestaurantSettingReferences.MarchSettingTurbo,
            RestaurantSettingReferences.Autumn,
            RestaurantSettingReferences.Country,
            RestaurantSettingReferences.Alpine,
        };
        public override float GetWindowWidth()
        {
            return 200;
        }
        public override void OnGUI()
        {
            CreateNodeButton("Diner Layout", () =>
            {
                LoadTemplate(LayoutProfileReferences.DinerLayout, defaultSettingIDs[Random.Range(0, defaultSettingIDs.Length)]);
            });
            
            CreateNodeButton("Medium Layout", () =>
            {
                LoadTemplate(LayoutProfileReferences.MediumLayout, defaultSettingIDs[Random.Range(0, defaultSettingIDs.Length)]);
            });
            
            CreateNodeButton("Basic Layout", () =>
            {
                LoadTemplate(LayoutProfileReferences.BasicLayout, defaultSettingIDs[Random.Range(0, defaultSettingIDs.Length)]);
            });
            
            CreateNodeButton("Huge Layout", () =>
            {
                LoadTemplate(LayoutProfileReferences.HugeLayout, defaultSettingIDs[Random.Range(0, defaultSettingIDs.Length)]);
            });
            
            CreateNodeButton("Extended Layout", () =>
            {
                LoadTemplate(LayoutProfileReferences.ExtendedLayout, defaultSettingIDs[Random.Range(0, defaultSettingIDs.Length)]);
            });
            
            CreateNodeButton("Lake Layout", () =>
            {
                LoadTemplate(LayoutProfileReferences.JanuaryLayoutProfile, RestaurantSettingReferences.JanuarySetting);
            });
            
            CreateNodeButton("Romantic Layout", () =>
            {
                LoadTemplate(LayoutProfileReferences.FebruaryLayout, RestaurantSettingReferences.FebruarySetting);
            });
            
            CreateNodeButton("Coffee Shop Layout", () =>
            {
                LoadTemplate(LayoutProfileReferences.CoffeeshopLayout, RestaurantSettingReferences.JuneSettingCoffeeshop);
            });
            
            CreateNodeButton("Bakery Layout", () =>
            {
                LoadTemplate(LayoutProfileReferences.BakeryLayout, RestaurantSettingReferences.BakerySetting);
            });
            
            CreateNodeButton("Witch Layout", () =>
            {
                LoadTemplate(LayoutProfileReferences.WitchLayout, RestaurantSettingReferences.WitchHut2310);
            });
        }

        private void LoadTemplate(LayoutProfile profile, RestaurantSetting setting)
        {
            nodeEditor.nodeLogic.panningOffset = new Vector2(0, 0);
            nodeEditor.nodeLogic.nodes.Clear();
            int result = ConversionUtils.ConvertXNodeToRuntime(profile.Graph, nodeEditor);
            Utils.selectedProfile = profile;
            Utils.selectedSetting = setting;
            Debug.Log("Graph loaded with " + result + " errors.");
        }
        
        private void LoadTemplate(int profile, int setting)
        {
            if (GameData.Main.TryGet(profile, out LayoutProfile _profile) && GameData.Main.TryGet(setting, out RestaurantSetting _setting))
            {
                LoadTemplate(_profile, _setting);
            }
        }
    }
}