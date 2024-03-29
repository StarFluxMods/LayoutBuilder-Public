using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using UnityEngine;
using LayoutBuilder.Utility;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_RequireFeatureCountEven : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_RequireFeatureCountEvenWindow(), title: "RequireFeatureCountEven");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is RequireFeatureCountEven roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                ResultStatus = roomGrid.ResultStatus;
                Type = roomGrid.Type;
                RequireEven = roomGrid.RequireEven;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is RequireFeatureCountEven roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.ResultStatus = ResultStatus;
                roomGrid.Type = Type;
                roomGrid.RequireEven = RequireEven;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public bool ResultStatus;
        public FeatureType Type;
        public bool RequireEven;
    }

    public class Node_RequireFeatureCountEvenWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {

            Node_RequireFeatureCountEven n = (Node_RequireFeatureCountEven)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];
            
            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateBool("ResultStatus", ref n.ResultStatus);
            CreateEnum<FeatureType>("Type", ref n.Type);
            CreateBool("RequireEven", ref n.RequireEven);
        }
    }
}