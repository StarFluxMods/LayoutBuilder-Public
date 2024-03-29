using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using UnityEngine;
using LayoutBuilder.Utility;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_RoomGrid : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_RoomGridWindow(), title: "RoomGrid");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is RoomGrid roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                Width = roomGrid.Width;
                Height = roomGrid.Height;
                Type = roomGrid.Type;
                SetType = roomGrid.SetType;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is RoomGrid roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.Width = Width;
                roomGrid.Height = Height;
                roomGrid.Type = Type;
                roomGrid.SetType = SetType;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public int Width;
        public int Height;
        public RoomType Type;
        public bool SetType;
    }

    public class Node_RoomGridWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {

            Node_RoomGrid n = (Node_RoomGrid)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];
            
            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateInt("Width", ref n.Width);
            CreateInt("Height", ref n.Height);
            CreateEnum<RoomType>("Type", ref n.Type);
            CreateBool("SetType", ref n.SetType);
        }
    }
}