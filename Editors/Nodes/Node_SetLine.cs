using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using UnityEngine;
using LayoutBuilder.Utility;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_SetLine : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_SetLineWindow(), title: "SetLine");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is SetLine roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                Position = roomGrid.Position;
                IsRow = roomGrid.IsRow;
                Type = roomGrid.Type;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is SetLine roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.Position = Position;
                roomGrid.IsRow = IsRow;
                roomGrid.Type = Type;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public int Position;
        public bool IsRow;
        public RoomType Type;
    }

    public class Node_SetLineWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {

            Node_SetLine n = (Node_SetLine)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];
            
            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateInt("Position", ref n.Position);
            CreateBool("IsRow", ref n.IsRow);
            CreateEnum<RoomType>("Type", ref n.Type);
        }
    }
}