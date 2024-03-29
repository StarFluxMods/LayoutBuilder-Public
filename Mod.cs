using System.IO;
using KitchenLib;
using KitchenLib.Logging;
using KitchenLib.Logging.Exceptions;
using KitchenMods;
using System.Linq;
using System.Reflection;
using GUINodeEditor;
using KitchenLib.Preferences;
using LayoutBuilder.Editors;
using UnityEngine;

namespace LayoutBuilder
{
    public class Mod : BaseMod, IModSystem
    {
        public const string MOD_GUID = "com.starfluxgames.layoutbuilder";
        public const string MOD_NAME = "Layout Builder";
        public const string MOD_VERSION = "0.1.0";
        public const string MOD_AUTHOR = "StarFluxGames";
        public const string MOD_GAMEVERSION = ">=1.1.9";

        public static AssetBundle Bundle;
        public static KitchenLogger Logger;

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {
            Logger.LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        public static GameObject container;
        public static NodeEditorConfig config;
        public static PreferenceManager Manager;
        
        protected override void OnUpdate()
        {
            if (Input.GetKeyDown((KeyCode.G)))
            {
                if (container == null)
                {
                    container = new GameObject(MOD_NAME);
                    NodeEditor nodeEditor = container.AddComponent<NodeEditor>();
                    nodeEditor.saveLoadResourcesFolderName = Path.Combine(Application.persistentDataPath, "UserData", MOD_NAME, "Saved");
                    nodeEditor.shouldPrepareInitialLoad = true;
                    
                    if (!Directory.Exists(nodeEditor.saveLoadResourcesFolderName))
                    {
                        Directory.CreateDirectory(nodeEditor.saveLoadResourcesFolderName);
                    }
                    
                    nodeEditor.menuTypeHolder.serializedType = typeof(Layout_Editor).FullName;
                    nodeEditor.config.drawTextureOnScreen.backgroundTexture = Bundle.LoadAsset<Texture2D>("UccNodeEditorBackground");
                    nodeEditor.config.drawGridOnScreen.gridTexture = Bundle.LoadAsset<Texture2D>("UccNodeEditorGrid_times20");
                    
                    config = nodeEditor.config;
                    
                    container.AddComponent<RuntimeNodeEditor>();
                }
                else
                {
                    RuntimeNodeEditor runtimeNodeEditor = container.GetComponent<RuntimeNodeEditor>();
                    runtimeNodeEditor.enabled = !runtimeNodeEditor.enabled;
                }
            }
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            Bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).FirstOrDefault() ?? throw new MissingAssetBundleException(MOD_GUID);
            Logger = InitLogger();
            Manager = new PreferenceManager(MOD_GUID);
            
            Manager.RegisterPreference(new PreferenceInt("MENU_COLOR", 0));
            Manager.RegisterPreference(new PreferenceFloat("BACKGROUND_OPACITY", 0.25f));
            Manager.RegisterPreference(new PreferenceFloat("GRID_OPACITY", 0.25f));
            Manager.RegisterPreference(new PreferenceFloat("GRID_UNIT", 25));
            Manager.RegisterPreference(new PreferenceFloat("GRID_MULTIPLY_FACTOR", 20));
            Manager.RegisterPreference(new PreferenceBool("DRAW_MINIMAP", true));
            
            Manager.Load();
            Manager.Save();
        }
    }
}

