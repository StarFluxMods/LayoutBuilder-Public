using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using UnityEngine;
using LayoutBuilder.Utility;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_RequireDoorPair : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_RequireDoorPairWindow(), title: "RequireDoorPair");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is RequireDoorPair roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                ResultStatus = roomGrid.ResultStatus;
                Room1 = roomGrid.Room1;
                Room2 = roomGrid.Room2;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is RequireDoorPair roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.ResultStatus = ResultStatus;
                roomGrid.Room1 = Room1;
                roomGrid.Room2 = Room2;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public bool ResultStatus;
        public RoomType Room1;
        public RoomType Room2;
    }

    public class Node_RequireDoorPairWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {

            Node_RequireDoorPair n = (Node_RequireDoorPair)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];
            
            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateBool("ResultStatus", ref n.ResultStatus);
            CreateEnum<RoomType>("Room1", ref n.Room1);
            CreateEnum<RoomType>("Room2", ref n.Room2);
        }
    }
}