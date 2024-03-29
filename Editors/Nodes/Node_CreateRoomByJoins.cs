using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using LayoutBuilder.Utility;
using UnityEngine;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_CreateRoomByJoins : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_CreateRoomByJoinsWindow(), title: "CreateRoomByJoins");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is CreateRoomByJoins roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                StartX = roomGrid.StartX;
                StartY = roomGrid.StartY;
                Joins = roomGrid.Joins;
                Type = roomGrid.Type;
                Required = roomGrid.Required;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is CreateRoomByJoins roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.StartX = StartX;
                roomGrid.StartY = StartY;
                roomGrid.Joins = Joins;
                roomGrid.Type = Type;
                roomGrid.Required = Required;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public int StartX;
        public int StartY;
        public int Joins;
        public RoomType Type;
        public bool Required;
    }

    public class Node_CreateRoomByJoinsWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {
            Node_CreateRoomByJoins n = (Node_CreateRoomByJoins)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];

            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateInt("StartX", ref n.StartX);
            CreateInt("StartY", ref n.StartY);
            CreateInt("Joins", ref n.Joins);
            CreateEnum<RoomType>("Type", ref n.Type);
            CreateBool("Required", ref n.Required);
            
        }
    }
}