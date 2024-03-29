using KitchenData;
using LayoutBuilder.Utility;
using UnityEngine;
using Node = GUINodeEditor.Node;

namespace LayoutBuilder.Editors
{
    public class ExistingLayoutProfiles_Menu : Node
    {

        public override void Init(Vector2 position)
        {
            Init (position, nodeWindow: new ExistingLayoutProfiles_Menu_Window(), title: "Assign Decorator");
        }
    }

    public class ExistingLayoutProfiles_Menu_Window : WindowModule
    {
        public override float GetWindowWidth()
        {
            return 200;
        }
        public override void OnGUI()
        {
            foreach (LayoutProfile profile in GameData.Main.Get<LayoutProfile>())
            {
                CreateNodeButton(profile.name, () =>
                {
                    Utils.selectedProfile = profile;
                    nodeEditor.nodeLogic.nodes.Remove(node);
                }, true);
            }
        }
    }
}