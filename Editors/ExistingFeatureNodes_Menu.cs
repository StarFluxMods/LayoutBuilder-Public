using LayoutBuilder.Editors.Nodes;
using UnityEngine;
using Node = GUINodeEditor.Node;

namespace LayoutBuilder.Editors
{
    public class ExistingFeatureNodes_Menu : Node
    {

        public override void Init(Vector2 position)
        {
            Init (position, nodeWindow: new ExistingFeatureNodes_Menu_Window(), title: "Features");
        }

        public int FixSeed;
    }

    public class ExistingFeatureNodes_Menu_Window : WindowModule
    {
        public override float GetWindowWidth()
        {
            return 200;
        }
        public override void OnGUI()
        {
            CreateNodeButton<Node_AppendFeatures>("AppendFeatures");
            CreateNodeButton<ExistingFeatureFilterNodes_Menu>("Filters >", true);
            CreateNodeButton<Node_FindAllFeatures>("FindAllFeatures");
            CreateNodeButton<Node_MoveFeatureInDirection>("MoveFeatureInDirection");
            CreateNodeButton<Node_ReplaceFeaturesByRoomType>("ReplaceFeaturesByRoomType");
            CreateNodeButton<Node_SwitchFeatures>("SwitchFeatures");
        }
    }
}