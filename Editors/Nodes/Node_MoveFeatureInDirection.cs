using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using UnityEngine;
using LayoutBuilder.Utility;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_MoveFeatureInDirection : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_MoveFeatureInDirectionWindow(), title: "MoveFeatureInDirection");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is MoveFeatureInDirection roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                OffsetX = roomGrid.OffsetX;
                OffsetY = roomGrid.OffsetY;
                MaxSteps = roomGrid.MaxSteps;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is MoveFeatureInDirection roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.OffsetX = OffsetX;
                roomGrid.OffsetY = OffsetY;
                roomGrid.MaxSteps = MaxSteps;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public int OffsetX;
        public int OffsetY;
        public int MaxSteps;
    }

    public class Node_MoveFeatureInDirectionWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {

            Node_MoveFeatureInDirection n = (Node_MoveFeatureInDirection)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];
            
            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateInt("OffsetX", ref n.OffsetX);
            CreateInt("OffsetY", ref n.OffsetY);
            CreateInt("MaxSteps", ref n.MaxSteps);
        }
    }
}