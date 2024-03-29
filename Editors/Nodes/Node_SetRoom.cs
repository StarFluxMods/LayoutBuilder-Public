using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using UnityEngine;
using LayoutBuilder.Utility;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_SetRoom : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_SetRoomWindow(), title: "SetRoom");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is SetRoom roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                X = roomGrid.X;
                Y = roomGrid.Y;
                Type = roomGrid.Type;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is SetRoom roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.X = X;
                roomGrid.Y = Y;
                roomGrid.Type = Type;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public int X;
        public int Y;
        public RoomType Type;
    }

    public class Node_SetRoomWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {

            Node_SetRoom n = (Node_SetRoom)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];
            
            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateInt("X", ref n.X);
            CreateInt("Y", ref n.Y);
            CreateEnum<RoomType>("Type", ref n.Type);
        }
    }
}