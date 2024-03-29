using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using UnityEngine;
using LayoutBuilder.Utility;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_FullyConnect : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_FullyConnectWindow(), title: "FullyConnect");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is FullyConnect roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                BaseRoom = roomGrid.BaseRoom;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is FullyConnect roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.BaseRoom = BaseRoom;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public RoomType BaseRoom;
    }

    public class Node_FullyConnectWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {

            Node_FullyConnect n = (Node_FullyConnect)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];
            
            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateEnum<RoomType>("BaseRoom", ref n.BaseRoom);
        }
    }
}