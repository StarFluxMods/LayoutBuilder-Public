using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using UnityEngine;
using LayoutBuilder.Utility;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_PadWithRoom : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_PadWithRoomWindow(), title: "PadWithRoom");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is PadWithRoom roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                Type = roomGrid.Type;
                Above = roomGrid.Above;
                Left = roomGrid.Left;
                Right = roomGrid.Right;
                Below = roomGrid.Below;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is PadWithRoom roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.Type = Type;
                roomGrid.Above = Above;
                roomGrid.Left = Left;
                roomGrid.Right = Right;
                roomGrid.Below = Below;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public RoomType Type;
        public int Above;
        public int Left;
        public int Right;
        public int Below;
    }

    public class Node_PadWithRoomWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {
            Node_PadWithRoom n = (Node_PadWithRoom)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];

            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateEnum<RoomType>("Type", ref n.Type);
            CreateInt("Above", ref n.Above);
            CreateInt("Left", ref n.Left);
            CreateInt("Right", ref n.Right);
            CreateInt("Below", ref n.Below);
            
        }
    }
}