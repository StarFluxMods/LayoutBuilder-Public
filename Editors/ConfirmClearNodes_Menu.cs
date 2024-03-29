using UnityEngine;
using Node = GUINodeEditor.Node;

namespace LayoutBuilder.Editors
{
    public class ConfirmClearNodes_Menu : Node
    {

        public override void Init(Vector2 position)
        {
            Init (position, nodeWindow: new ConfirmClearNodes_Menu_Window(), title: "Clear Nodes");
        }

        public int FixSeed;
    }

    public class ConfirmClearNodes_Menu_Window : WindowModule
    {
        public override float GetWindowWidth()
        {
            return 200;
        }
        public override void OnGUI()
        {
            GUILayout.Label("Are you sure?");
            GUILayout.Space(5);
            CreateNodeButton("Yes", () => { nodeEditor.nodeLogic.nodes.Clear(); }, true);
            GUILayout.Space(5);
            CreateNodeButton("No", () => { }, true);
        }
    }
}