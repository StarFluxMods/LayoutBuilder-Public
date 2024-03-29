using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using UnityEngine;
using LayoutBuilder.Utility;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_SplitLine : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_SplitLineWindow(), title: "SplitLine");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is SplitLine roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                Position = roomGrid.Position;
                Count = roomGrid.Count;
                IsRow = roomGrid.IsRow;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is SplitLine roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.Position = Position;
                roomGrid.Count = Count;
                roomGrid.IsRow = IsRow;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public int Position;
        public int Count;
        public bool IsRow;
    }

    public class Node_SplitLineWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {

            Node_SplitLine n = (Node_SplitLine)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];
            
            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateInt("Position", ref n.Position);
            CreateInt("Count", ref n.Count);
            CreateBool("IsRow", ref n.IsRow);
        }
    }
}