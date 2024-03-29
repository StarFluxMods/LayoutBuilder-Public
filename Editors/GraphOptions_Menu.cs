using GUINodeEditor;
using KitchenLib.Preferences;
using LayoutBuilder.Utility;
using UnityEngine;

namespace LayoutBuilder.Editors
{
    public class GraphOptions_Menu : Node
    {

        public override void Init(Vector2 position)
        {
            Init (position, nodeWindow: new GraphOptions_Menu_Window(), title: "Options");
        }
    }

    public class GraphOptions_Menu_Window : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 200;
        }
        public override void OnGUI()
        {
            GUILayout.Label("Menu Color:");
            Utils.MenuColor = (MenuColor)popup.EnumPopup(Utils.MenuColor);
            
            GUILayout.Space(5);
            GUILayout.Label("Background Opacity:");
            Mod.Manager.GetPreference<PreferenceFloat>("BACKGROUND_OPACITY").Set(GUILayout.HorizontalSlider(Mod.config.drawTextureOnScreen.backgroundOpacity, 0, 1));
                
            GUILayout.Space(5);
            GUILayout.Label("Grid Opacity:");
            Mod.Manager.GetPreference<PreferenceFloat>("GRID_OPACITY").Set(GUILayout.HorizontalSlider(Mod.config.drawGridOnScreen.gridOpacity, 0, 1));
            
            GUILayout.Space(5);
            GUILayout.Label("Grid Unit:");
            Mod.Manager.GetPreference<PreferenceFloat>("GRID_UNIT").Set(GUILayout.HorizontalSlider(Mod.config.drawGridOnScreen.gridUnit, 25, 50));
            
            GUILayout.Space(5);
            GUILayout.Label("Grid Multiply Factor:");
            Mod.Manager.GetPreference<PreferenceFloat>("GRID_MULTIPLY_FACTOR").Set(GUILayout.HorizontalSlider(Mod.config.drawGridOnScreen.gridMultiplyFactor, 1, 50));
            
            GUILayout.Space(5);
            GUILayout.Label("Draw Minimap:");
            Mod.Manager.GetPreference<PreferenceBool>("DRAW_MINIMAP").Set(GUILayout.Toggle(Mod.config.drawMinimap, ""));
            
            GUILayout.Space(5);
            if (GUILayout.Button("Save"))
            {
                Mod.Manager.Save();
            }
        }
    }
}