using KitchenData;
using LayoutBuilder.Utility;
using UnityEngine;
using Node = GUINodeEditor.Node;

namespace LayoutBuilder.Editors
{
    public class ExistingGraphs_Menu : Node
    {

        public override void Init(Vector2 position)
        {
            Init (position, nodeWindow: new ExistingGraphs_Menu_Window(), title: "Existing Graphs");
        }
    }

    public class ExistingGraphs_Menu_Window : WindowModule
    {
        public override float GetWindowWidth()
        {
            return 200;
        }
        
        public override void OnGUI()
        {
            foreach (LayoutProfile profile in GameData.Main.Get<LayoutProfile>())
            {
                CreateNodeButton(profile.name, () =>
                {
                    nodeEditor.nodeLogic.panningOffset = new Vector2(0, 0);
                    nodeEditor.nodeLogic.nodes.Clear();

                    int result = ConversionUtils.ConvertXNodeToRuntime(profile.Graph, nodeEditor);
                    Debug.Log("Graph loaded with " + result + " errors.");
                }, true);
            }
        }
    }
}