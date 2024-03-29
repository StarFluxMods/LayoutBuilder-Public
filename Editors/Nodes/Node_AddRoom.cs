using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using LayoutBuilder.Utility;
using UnityEngine;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_AddRoom : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_AddRoomWindow(), title: "AddRoom");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is AddRoom roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                X = roomGrid.X;
                Y = roomGrid.Y;
                Height = roomGrid.Height;
                Width = roomGrid.Width;
                Type = roomGrid.Type;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is AddRoom roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.X = X;
                roomGrid.Y = Y;
                roomGrid.Height = Height;
                roomGrid.Width = Width;
                roomGrid.Type = Type;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public int X;
        public int Y;
        public int Height;
        public int Width;
        public RoomType Type;
    }

    public class Node_AddRoomWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {
            Node_AddRoom n = (Node_AddRoom)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];
            
            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateInt("X", ref n.X);
            CreateInt("Y", ref n.Y);
            CreateInt("Height", ref n.Height);
            CreateInt("Width", ref n.Width);
            CreateEnum<RoomType>("Type", ref n.Type);
        }
    }
}