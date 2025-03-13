using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Kamgam.BikeAndCharacter25D
{
    public class BikeAndCharacter25DSettings : ScriptableObject
    {
        [InitializeOnLoadMethod]
        private static void OnInitialize()
        {
            BikeAndCharacter25DSettings.GetOrCreateSettings();
        }

        public const string Version = "1.1.2";
        public const string SettingsFilePath = "Assets/BikeAndCharacter25DSettings.asset";

        protected static BikeAndCharacter25DSettings cachedSettings;

        public static BikeAndCharacter25DSettings GetOrCreateSettings()
        {
            if (cachedSettings == null)
            {
                cachedSettings = AssetDatabase.LoadAssetAtPath<BikeAndCharacter25DSettings>(SettingsFilePath);

                if (cachedSettings == null)
                {
                    var guids = AssetDatabase.FindAssets("t:BikeAndCharacter25DSettings");
                    if (guids.Length > 0)
                    {
                        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                        cachedSettings = AssetDatabase.LoadAssetAtPath<BikeAndCharacter25DSettings>(path);
                    }
                }

                if (cachedSettings == null)
                {
                    cachedSettings = ScriptableObject.CreateInstance<BikeAndCharacter25DSettings>();


                    AssetDatabase.CreateAsset(cachedSettings, SettingsFilePath);
                    AssetDatabase.SaveAssets();

                    AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);

                    waitForMaterialsToLoadAndFixIfNeeded();
                }
            }
            return cachedSettings;
        }

        [MenuItem("Tools/Bike And Character 2.5D/Fix Materials", priority = 201)]
        public static void FixMaterialsIfNeeded()
        {
            // Fix normal materials
            MaterialShaderFixer.FixMaterials(MaterialShaderFixer.RenderPiplelineType.URP);

            // Fix tri planar materials
            CheckPackages.CheckForPackage("com.unity.shadergraph", (found) =>
            {
                if (found == false)
                {
                    string msg = "BikeAndCharacter2.5D Demo Scene: Shader Graph Package is not installed." +
                    "\n\nTo use the provided Tri-Planar shader you'll have to install shader graph: https://docs.unity3d.com/Packages/com.unity.shadergraph@latest/ " +
                    "\n\nIf you do not want to use Shader Graph then the 'Standard' shader will be assigned to all Tri-Planar materials and you will have to make your own Tri-Planar standard shader." +
                    "\n\nDo you want to fix the shader now?";
                    bool updateShaders = EditorUtility.DisplayDialog("BikeAndCharacter2.5D Demo: Shader Graph Package is not installed!",
                        msg,
                        "Yes (fix by assigning 'Standard' shader)",
                        "No (keep shaders, I'll install Shader Graph)"
                        );

                    // Fix shaders in all Materials
                    var shader = MaterialShaderFixer.GetDefaultShader();
                    if (shader != null)
                    {
                        if (updateShaders)
                        {
                            Debug.Log("BikeAndCharacter2.5D Demo Scene: Shader Graph Package is not installed. Using standard shader instead.");

                            Material material;
                            material = AssetDatabase.LoadAssetAtPath<Material>("Assets/BikeAndCharacter2.5D/Examples/Materials/2.5D Terrain TriPlanar.mat");
                            if (material != null)
                            {
                                material.shader = shader;
                                material.color = new Color(0.1f, 0.600f, 0.1f);

                                AssetDatabase.SaveAssets();
                            }
                        }
                        else
                        {
                            Debug.LogWarning("BikeAndCharacter2.5D Demo Scene: Shader Graph Package is not installed. Keeping broken shaders. They will appear PINK! Time to download and install Shader Graph: https://docs.unity3d.com/Packages/com.unity.shadergraph@latest/. \nAfter installation you may have to open the Graph (Shaders/TriPlanarProjection and Shaders/Terrain) and save it once.");
                        }
                    }
                    else
                    {
                        Debug.LogError("No default shader found!");
                    }
                }
            });
        }

        static async void waitForMaterialsToLoadAndFixIfNeeded()
        {
            // Wait for timeout or 
            double timeOutInSec = 10;
            double startTime = EditorApplication.timeSinceStartup;
            while (EditorApplication.timeSinceStartup - startTime < timeOutInSec && !MaterialShaderFixer.AreMaterialsLoaded())
            {
                Debug.Log("Waiting for materials to load.");
                await System.Threading.Tasks.Task.Yield();
            }

            FixMaterialsIfNeeded();
        }

        internal static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }

        // settings
        public static void SelectSettings()
        {
            var settings = BikeAndCharacter25DSettings.GetOrCreateSettings();
            if (settings != null)
            {
                Selection.activeObject = settings;
                EditorGUIUtility.PingObject(settings);
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "BikeAndCharacter25D settings could not be found or created.", "Ok");
            }
        }

        /*
        [MenuItem("Tools/Bike And Character 2.5D/Settings", priority = 100)]
        public static void OpenSettings()
        {
            var settings = GetOrCreateSettings();
            if (settings != null)
            {
                Selection.activeObject = settings;
                EditorGUIUtility.PingObject(settings);
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Settings could not be found or created.", "Ok");
            }
        }
        */

        [MenuItem("Tools/Bike And Character 2.5D/Manual", priority = 101)]
        public static void OpenManual()
        {
            Application.OpenURL("https://kamgam.com/unity/BikeAndCharacter2.5DManual.pdf");
        }

        [MenuItem("Tools/Bike And Character 2.5D/Open Example Scene", priority = 103)]
        public static void OpenExample()
        {
            string path = "Assets/BikeAndCharacter2.5D/Examples/DemoWithTerrain.unity";
            var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
            EditorGUIUtility.PingObject(scene);
            EditorSceneManager.OpenScene(path);
        }

        [MenuItem("Tools/Bike And Character 2.5D/Please leave a review ♥❤♥", priority = 301)]
        public static void Review()
        {
            Application.OpenURL("https://assetstore.unity.com/packages/slug/227498?aid=1100lqC54&pubref=asset");
        }

        [MenuItem("Tools/Bike And Character 2.5D/Check out 2.5D Bridge Builder", priority = 302)]
        public static void BridgeBuilder()
        {
            Application.OpenURL("https://assetstore.unity.com/packages/slug/225692?aid=1100lqC54&pubref=asset");
        }

        [MenuItem("Tools/Bike And Character 2.5D/Check out 2.5D Terrain", priority = 302)]
        public static void Terrain()
        {
            Application.OpenURL("https://assetstore.unity.com/packages/slug/220783?aid=1100lqC54&pubref=asset");
        }
    }
}