using System;
using GUINodeEditor;
using UnityEngine;

namespace LayoutBuilder.Editors
{
    public abstract class NodeModule : NodeWindow
    {
        public void CreateInt(string name, ref int value)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(name);
            value = int.Parse(GUILayout.TextField(value.ToString()));
            GUILayout.EndHorizontal();
        }

        public void CreateEnum<T>(string name, ref T value) where T : Enum
        {
            GUILayout.Label(name);
            value = (T)popup.EnumPopup(value);
        }

        public void CreateBool(string name, ref bool value)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(name);
            value = GUILayout.Toggle(value, "");
            GUILayout.EndHorizontal();
        }

        public void CreateTopDocks(Dock output, Dock input)
        {
            GUILayout.BeginHorizontal();
            DrawDock(output);
            DrawDock(input);
            GUILayout.EndHorizontal();
        }

        public void CreateOptionalInput(Dock input)
        {
            GUILayout.BeginHorizontal();
            DrawDock(input);
            GUILayout.EndHorizontal();
        }
    }
}