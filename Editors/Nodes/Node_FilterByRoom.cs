using System.Collections.Generic;
using System.Linq;
using GUINodeEditor;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenLib.Preferences;
using UnityEngine;
using LayoutBuilder.Utility;

namespace LayoutBuilder.Editors.Nodes
{
    public class Node_FilterByRoom : Node, IXNodeConvertable, ISerializableNode
    {
        public override void Init(Vector2 position)
        {
            Init(position, new Node_FilterByRoomWindow(), title: "FilterByRoom");
            
            AddInput(typeof(LayoutGraphConnection), "Input");
            AddOutput(typeof(LayoutGraphConnection), "Output");
        }

        public void LoadValuesFromXNode(XNode.Node node)
        {
            if (node is FilterByRoom roomGrid)
            {
                FixSeed = roomGrid.FixSeed;
                RemoveMode = roomGrid.RemoveMode;
                Type1 = roomGrid.Type1;
                FilterSecond = roomGrid.FilterSecond;
                Type2 = RoomTypeToRoomTypeX[roomGrid.Type2];
            }
        }

        public T ConvertToXNode<T>() where T : XNode.Node, new()
        {
            T node = (T)ScriptableObject.CreateInstance(typeof(T));
            if (node is FilterByRoom roomGrid)
            {
                roomGrid.FixSeed = FixSeed;
                roomGrid.RemoveMode = RemoveMode;
                roomGrid.Type1 = Type1;
                roomGrid.FilterSecond = FilterSecond;
                roomGrid.Type2 = RoomTypeToRoomTypeX.FirstOrDefault( x => x.Value == Type2).Key;

                roomGrid.position = nodeWindow.rect.position;
            }

            return node;
        }

        public int FixSeed;
        public bool RemoveMode;
        public RoomType Type1;
        public bool FilterSecond;
        public RoomTypeX Type2;
        
        /*
         * This is a dictionary that maps the RoomType enum to the RoomTypeX enum.
         * This only exists because the node editor 
         */

        private static readonly Dictionary<RoomType, RoomTypeX> RoomTypeToRoomTypeX = new Dictionary<RoomType, RoomTypeX>
        {
            {RoomType.NoRoom, RoomTypeX.NoRoom},
            {RoomType.Unassigned, RoomTypeX.Unassigned},
            {RoomType.Dining, RoomTypeX.Dining},
            {RoomType.Kitchen, RoomTypeX.Kitchen},
            {RoomType.Garden, RoomTypeX.Garden},
            {RoomType.FrontEntrance, RoomTypeX.FrontEntrance},
            {RoomType.SocialSpace, RoomTypeX.SocialSpace},
            {RoomType.Bedroom, RoomTypeX.Bedroom},
            {RoomType.Garage, RoomTypeX.Garage},
            {RoomType.Office, RoomTypeX.Office},
            {RoomType.Workshop, RoomTypeX.Workshop},
            {RoomType.Contracts, RoomTypeX.Contracts},
            {RoomType.Trophies, RoomTypeX.Trophies},
            {RoomType.Locations, RoomTypeX.Locations},
            {RoomType.Storage, RoomTypeX.Storage}
        };
    }

    public enum RoomTypeX
    {
        NoRoom,
        Unassigned,
        Dining,
        Kitchen,
        Garden,
        FrontEntrance,
        SocialSpace,
        Bedroom,
        Garage,
        Office,
        Workshop,
        Contracts,
        Trophies,
        Locations,
        Storage
    }

    public class Node_FilterByRoomWindow : NodeModule
    {
        public override float GetWindowWidth()
        {
            return 160;
        }

        
        public override void OnGUI()
        {

            Node_FilterByRoom n = (Node_FilterByRoom)node;
            backgroundColor = Utils.MenuColorToColor[Mod.Manager.GetPreference<PreferenceInt>("MENU_COLOR").Value];
            
            CreateTopDocks(n.outputs[0], n.inputs[0]);
            
            CreateInt("FixSeed", ref n.FixSeed);
            CreateBool("RemoveMode", ref n.RemoveMode);
            CreateEnum<RoomType>("Type1", ref n.Type1);
            CreateBool("FilterSecond", ref n.FilterSecond);
            CreateEnum<RoomTypeX>("Type2", ref n.Type2);
            
        }
    }
}