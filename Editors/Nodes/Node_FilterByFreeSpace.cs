using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using LayoutBuilder.Utility;
using UnityEngine;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_FilterByFreeSpace : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_FilterByFreeSpaceWindow(), title: "FilterByFreeSpace");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is FilterByFreeSpace roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is FilterByFreeSpace roomGrid)
            {
                roomGrid.FixSeed = FixSeed;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
    }

    public class Node_FilterByFreeSpaceWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {

            Node_FilterByFreeSpace n = (Node_FilterByFreeSpace)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];
            
            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            
        }
    }
}