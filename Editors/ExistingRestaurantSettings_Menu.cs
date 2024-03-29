using KitchenData;
using LayoutBuilder.Utility;
using UnityEngine;
using Node = GUINodeEditor.Node;

namespace LayoutBuilder.Editors
{
    public class ExistingRestaurantSettings_Menu : Node
    {

        public override void Init(Vector2 position)
        {
            Init (position, nodeWindow: new ExistingRestaurantSettings_Menu_Window(), title: "Assign Setting");
        }

        public int FixSeed;
    }

    public class ExistingRestaurantSettings_Menu_Window : WindowModule
    {
        public override float GetWindowWidth()
        {
            return 200;
        }
        public override void OnGUI()
        {
            foreach (RestaurantSetting setting in GameData.Main.Get<RestaurantSetting>())
            {
                CreateNodeButton(setting.name, () =>
                {
                    Utils.selectedSetting = setting;
                    nodeEditor.nodeLogic.nodes.Remove(node);
                }, true);
            }
        }
    }
}