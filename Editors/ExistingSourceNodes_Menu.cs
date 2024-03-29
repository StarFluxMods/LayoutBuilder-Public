using LayoutBuilder.Editors.Nodes;
using UnityEngine;
using Node = GUINodeEditor.Node;

namespace LayoutBuilder.Editors
{
    public class ExistingSourceNodes_Menu : Node
    {

        public override void Init(Vector2 position)
        {
            Init (position, nodeWindow: new ExistingSourceNodes_Menu_Window(), title: "Source");
        }

        public int FixSeed;
    }

    public class ExistingSourceNodes_Menu_Window : WindowModule
    {
        public override float GetWindowWidth()
        {
            return 200;
        }
        public override void OnGUI()
        {
            CreateNodeButton<Node_RoomGrid>("RoomGrid");
        }
    }
}