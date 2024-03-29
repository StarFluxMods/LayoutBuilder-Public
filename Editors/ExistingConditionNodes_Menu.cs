using LayoutBuilder.Editors.Nodes;
using UnityEngine;
using Node = GUINodeEditor.Node;

namespace LayoutBuilder.Editors
{
    public class ExistingConditionNodes_Menu : Node
    {

        public override void Init(Vector2 position)
        {
            Init (position, nodeWindow: new ExistingConditionNodes_Menu_Window(), title: "Conditions");
        }

        public int FixSeed;
    }

    public class ExistingConditionNodes_Menu_Window : WindowModule
    {
        public override float GetWindowWidth()
        {
            return 200;
        }
        public override void OnGUI()
        {
            CreateNodeButton<Node_RequireAccessible>("RequireAccessible");
            CreateNodeButton<Node_RequireDoorPair>("RequireDoorPair");
            CreateNodeButton<Node_RequireFeatureCountEven>("RequireFeatureCountEven");
            CreateNodeButton<Node_RequireFeatures>("RequireFeatures");
            CreateNodeButton<Node_RequireNoOverlaps>("RequireNoOverlaps");
        }
    }
}