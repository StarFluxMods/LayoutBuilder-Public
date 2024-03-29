using LayoutBuilder.Editors.Nodes;
using UnityEngine;
using Node = GUINodeEditor.Node;

namespace LayoutBuilder.Editors
{
    public class ExistingFeatureFilterNodes_Menu : Node
    {

        public override void Init(Vector2 position)
        {
            Init (position, nodeWindow: new ExistingFeatureFilterNodes_Menu_Window(), title: "Filters");
        }

        public int FixSeed;
    }

    public class ExistingFeatureFilterNodes_Menu_Window : WindowModule
    {
        public override float GetWindowWidth()
        {
            return 200;
        }
        public override void OnGUI()
        {
            CreateNodeButton<Node_FilterAdjacentPair>("FilterAdjacentPair");
            CreateNodeButton<Node_FilterByFreeSpace>("FilterByFreeSpace");
            CreateNodeButton<Node_FilterByRoom>("FilterByRoom");
            CreateNodeButton<Node_FilterOnePerPair>("FilterOnePerPair");
            CreateNodeButton<Node_FilterOppositePair>("FilterOppositePair");
            CreateNodeButton<Node_FilterSelectCount>("FilterSelectCount");
        }
    }
}