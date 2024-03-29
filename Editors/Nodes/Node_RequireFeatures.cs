using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using UnityEngine;
using LayoutBuilder.Utility;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_RequireFeatures : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_RequireFeaturesWindow(), title: "RequireFeatures");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is RequireFeatures roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                ResultStatus = roomGrid.ResultStatus;
                Type = roomGrid.Type;
                Minimum = roomGrid.Minimum;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is RequireFeatures roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.ResultStatus = ResultStatus;
                roomGrid.Type = Type;
                roomGrid.Minimum = Minimum;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public bool ResultStatus;
        public FeatureType Type;
        public int Minimum;
    }

    public class Node_RequireFeaturesWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {

            Node_RequireFeatures n = (Node_RequireFeatures)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];
            
            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateBool("ResultStatus", ref n.ResultStatus);
            CreateEnum<FeatureType>("Type", ref n.Type);
            CreateInt("Minimum", ref n.Minimum);
        }
    }
}