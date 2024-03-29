using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using UnityEngine;
using LayoutBuilder.Utility;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_ReplaceFeaturesByRoomType : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_ReplaceFeaturesByRoomTypeWindow(), title: "ReplaceFeaturesByRoomType");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is ReplaceFeaturesByRoomType roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                SetToFeature = roomGrid.SetToFeature;
                MatchingType1 = roomGrid.MatchingType1;
                MatchingType2 = roomGrid.MatchingType2;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is ReplaceFeaturesByRoomType roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.SetToFeature = SetToFeature;
                roomGrid.MatchingType1 = MatchingType1;
                roomGrid.MatchingType2 = MatchingType2;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public FeatureType SetToFeature;
        public RoomType MatchingType1;
        public RoomType MatchingType2;
    }

    public class Node_ReplaceFeaturesByRoomTypeWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {

            Node_ReplaceFeaturesByRoomType n = (Node_ReplaceFeaturesByRoomType)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];
            
            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateEnum<FeatureType>("SetToFeature", ref n.SetToFeature);
            CreateEnum<RoomType>("MatchingType1", ref n.MatchingType1);
            CreateEnum<RoomType>("MatchingType2", ref n.MatchingType2);
        }
    }
}