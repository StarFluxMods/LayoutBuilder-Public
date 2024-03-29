using Kitchen.Layouts;
using LayoutBuilder.Systems;
using LayoutBuilder.Utility;
using UnityEngine;
using Node = GUINodeEditor.Node;

namespace LayoutBuilder.Editors
{
    public class Layout_Editor : Node {
        public override void Init(Vector2 position) {
            Init (position, nodeWindow: new Layout_Editor_Window());
        }
    }
    
    public class Layout_Editor_Window : WindowModule
    {
        public static Layout_Editor_Window instance;
        public override float GetWindowWidth()
        {
            return 200;
        }

        public override void OnGUI()
        {
            instance ??= this;
            if (clickedWindow != null)
            {
                title = clickedWindow.node.GetType().ToString();
                GUILayout.Box("Custom menu");
            }
            else
            {
                title = "Layout Builder";
                
                CreateNodeButton<ExistingGraphs_Menu>("Existing Graphs >", true);
                CreateNodeButton<ExistingRestaurantSettings_Menu>("Assign Setting >", true);
                CreateNodeButton<ExistingLayoutProfiles_Menu>("Assign Decorator >", true);
                CreateNodeButton<Templates_Menu>("Templates >", true);
                
                GUILayout.Space(5);
                
                CreateNodeButton<ExistingNodes_Menu>("Nodes >", true);
                CreateNodeButton<ConfirmClearNodes_Menu>("Clear Nodes", true);
                
                GUILayout.Space(5);
                
                GUILayout.Label("Graph Name:");
                Utils.GraphName = GUILayout.TextField(Utils.GraphName);
                
                GUILayout.Space(5);
                
                GUILayout.Label("Screenshots: " + Utils.ScreenshotCount);
                Utils.ExtendedScreenshotCount = GUILayout.Toggle(Utils.ExtendedScreenshotCount, "Extended");
                if (Utils.ExtendedScreenshotCount)
                {
                    Utils.ScreenshotCount = (int)GUILayout.HorizontalSlider(Utils.ScreenshotCount, 1, 1000);
                }
                else
                {
                    Utils.ScreenshotCount = (int)GUILayout.HorizontalSlider(Utils.ScreenshotCount, 1, 100);
                }
                CreateNodeButton("Save Screenshot(s)", () => { Utils.GenerateScreenshots(nodeEditor, Utils.ScreenshotCount); });
                
                GUILayout.Space(5);
                
                CreateNodeButton("Give Player Map", () =>
                {
                    if (ConversionUtils.ConvertRuntimeToXNode(nodeEditor, out LayoutGraph graph))
                        GivePlayerMap.instance.GivePlayerNewMap(graph);
                });
                
                GUILayout.Space(5);

                CreateNodeButton("Generate Script", () =>
                {
                    if (ConversionUtils.ConvertRuntimeToXNode(nodeEditor, out LayoutGraph graph))
                    {
                        if (ConversionUtils.ConvertXNodeToGenerativeScript(graph, out string[] scriptLines))
                        {
                            if (ConversionUtils.SaveScript(scriptLines)) {}
                        }
                    }
                });
                
                GUILayout.Space(5);
                
                CreateNodeButton<GraphOptions_Menu>("Options >");
            }
        }
    }
}