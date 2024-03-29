using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using LayoutBuilder.Utility;
using UnityEngine;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_SwitchFeatures : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_SwitchFeaturesWindow(), title: "SwitchFeatures");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is SwitchFeatures roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                SetToFeature = roomGrid.SetToFeature;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is SwitchFeatures roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.SetToFeature = SetToFeature;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public FeatureType SetToFeature;
    }

    public class Node_SwitchFeaturesWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {

            Node_SwitchFeatures n = (Node_SwitchFeatures)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];
            
            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateEnum<FeatureType>("SetToFeature", ref n.SetToFeature);
        }
    }
}