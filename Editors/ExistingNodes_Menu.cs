using LayoutBuilder.Editors.Nodes;
using UnityEngine;
using Node = GUINodeEditor.Node;

namespace LayoutBuilder.Editors
{
    public class ExistingNodes_Menu : Node
    {

        public override void Init(Vector2 position)
        {
            Init (position, nodeWindow: new ExistingNodes_Menu_Window(), title: "Nodes");
        }

        public int FixSeed;
    }

    public class ExistingNodes_Menu_Window : WindowModule
    {
        public override float GetWindowWidth()
        {
            return 200;
        }
        public override void OnGUI()
        {
            CreateNodeButton<ExistingConditionNodes_Menu>("Conditions >", true);
            CreateNodeButton<Node_AddRoom>("AddRoom");
            CreateNodeButton<Node_CreateRoomByJoins>("CreateRoomByJoins");
            CreateNodeButton<ExistingFeatureNodes_Menu>("Features >", true);
            CreateNodeButton<Node_CreateFrontDoor>("CreateFrontDoor");
            CreateNodeButton<Node_FullyConnect>("FullyConnect");
            CreateNodeButton<Node_InsertRandomRoom>("InsertRandomRoom");
            CreateNodeButton<Node_MergeRoomsByType>("MergeRoomsByType");
            CreateNodeButton<Node_Mirror>("Mirror");
            CreateNodeButton<Node_PadWithRoom>("PadWithRoom");
            CreateNodeButton<Node_RecentreLayout>("RecentreLayout");
            CreateNodeButton<Node_SetLine>("SetLine");
            CreateNodeButton<Node_SetRoom>("SetRoom");
            CreateNodeButton<ExistingSourceNodes_Menu>("Source >", true);
            CreateNodeButton<Node_SplitLine>("SplitLine");
            CreateNodeButton<Node_SplitRooms>("SplitRooms");
            CreateNodeButton<Node_SwapRoomType>("SwapRoomType");
            CreateNodeButton<Node_Output>("Output");
        }
    }
}