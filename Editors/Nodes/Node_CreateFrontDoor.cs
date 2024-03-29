using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using LayoutBuilder.Utility;
using UnityEngine;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_CreateFrontDoor : Node, IXNodeConvertable, ISerializableNode
    {

        public override void Init(Vector2 position)
        {
            Init(position, new Node_CreateFrontDoorWindow(), title: "CreateFrontDoor");

            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is CreateFrontDoor roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                Type = roomGrid.Type;
                ForceFirstHalf = roomGrid.ForceFirstHalf;
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is CreateFrontDoor roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.Type = Type;
                roomGrid.ForceFirstHalf = ForceFirstHalf;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public RoomType Type;
        public bool ForceFirstHalf;
    }

    public class Node_CreateFrontDoorWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        public override void OnGUI()
        {

            Node_CreateFrontDoor n = (Node_CreateFrontDoor)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];
            
            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateEnum<RoomType>("Type", ref n.Type);
            CreateBool("ForceFirstHalf", ref n.ForceFirstHalf);
            
        }
    }
}