using System;
using GUINodeEditor;
using UnityEngine;

namespace LayoutBuilder.Editors
{
    public abstract class WindowModule : NodeWindow_Menu
    {
        protected void CreateNodeButton<T>(string name, bool isMenu = false) where T : Node, new()
        {
            if (!GUILayout.Button(name)) return;
            T newNode = nodeEditor.CreateNewWindow<T>(nodeEditor.prePanningPosition, isMenu);
            if (isMenu)
            {
                nodeEditor.nodeMenu = newNode;
            }
        }

        protected void CreateNodeButton(string name, Action action, bool shouldCloseOnAction = true)
        {
            if (!GUILayout.Button(name)) return;
            action();
            if (shouldCloseOnAction)
            {
                nodeEditor.nodeMenu = null;
            }
        }
    }
}