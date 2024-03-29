using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using UnityEngine;
using LayoutBuilder.Utility;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_RequireAccessible : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_RequireAccessibleWindow(), title: "RequireAccessible");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is RequireAccessible roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                ResultStatus = roomGrid.ResultStatus;
                AllowGardens = roomGrid.AllowGardens;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is RequireAccessible roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.ResultStatus = ResultStatus;
                roomGrid.AllowGardens = AllowGardens;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public bool ResultStatus;
        public bool AllowGardens;
    }

    public class Node_RequireAccessibleWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {

            Node_RequireAccessible n = (Node_RequireAccessible)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];
            
            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateBool("ResultStatus", ref n.ResultStatus);
            CreateBool("AllowGardens", ref n.AllowGardens);
        }
    }
}