using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kamgam.BikeAndCharacter25D
{
    public static class MaterialShaderFixer
    {
        public enum RenderPiplelineType
        {
            URP, HDRP, Standard
        }

        static Dictionary<string, Color> Materials = new Dictionary<string, Color> {
            { "Assets/BikeAndCharacter2.5D/Materials/Bike3DBody.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Materials/Bike3DParts.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Materials/Bike3DPlate.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Materials/Bike3DWheel.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Materials/Character3D.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Materials/DirtParticles.mat", new Color(0.2f, 0.8f, 0.2f) },
            { "Assets/BikeAndCharacter2.5D/Materials/ExhaustParticles.mat", new Color(0.6f, 0.6f, 0.6f) },
            { "Assets/BikeAndCharacter2.5D/Materials/Helmet3D.mat", Color.white },

            { "Assets/BikeAndCharacter2.5D/Examples/Materials/Sky.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Examples/Materials/TreeLeavesGreen.mat", new Color(0.15f, 0.5f, 0.15f) },
            { "Assets/BikeAndCharacter2.5D/Examples/Materials/TreeLeavesYellow.mat", new Color(0.9f, 0.8f, 0.0f) },
            { "Assets/BikeAndCharacter2.5D/Examples/Materials/TreeTrunk.mat", new Color(0.55f, 0.25f, 0.0f) },
            { "Assets/BikeAndCharacter2.5D/Examples/Materials/BridgePartTextured.mat", Color.white }
        };

        public static bool AreMaterialsLoaded()
        {
            int missingMaterials = 0;
            foreach (var path in Materials.Keys)
            {
                var material = AssetDatabase.LoadAssetAtPath<Material>(path);
                if (material != null)
                {
                    return true;
                }
                else
                {
                    missingMaterials++;
                    if (missingMaterials > 3)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public static void FixMaterials(RenderPiplelineType createdForRenderPipleline)
        {
            var currentRenderPipline = GetCurrentRenderPiplelineType();

            if (currentRenderPipline != createdForRenderPipleline)
            {
                Debug.Log("Fixing Materials");
                /*
                EditorUtility.DisplayDialog(
                    "Render pipeline mismatch detected.",
                    "The materials in this asset have been created with the Universal Render Pipeline (URP). You are using a different renderer, thus some of the materials may be broken (especially particle materials).\n\nThe tool will attempt to auto update materials now. In case some are still broken afterwards please fix those manually.",
                    "Understood"
                    );
                */

                Shader shader = GetDefaultShader();
                if (shader != null)
                {
                    Debug.Log("Using shader: " + shader.name);

                    foreach (var kv in Materials)
                    {
                        Material material = AssetDatabase.LoadAssetAtPath<Material>(kv.Key);
                        if (material != null)
                        {
                            Debug.Log("Updating material: " + material.name);
                            material.shader = shader;
                            material.color = kv.Value;
                            EditorUtility.SetDirty(material);
                        }
                        else
                        {
                            Debug.LogWarning("Huston, we have a problem! Material '" + kv.Key + "' not found!");
                        }
                    }
                }
            }
            else
            {
                Debug.Log("All good, nothing to fix.");
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        public static RenderPiplelineType GetCurrentRenderPiplelineType()
        {
            // Assume URP as default
            var renderPipeline = RenderPiplelineType.URP;

            // check if Standard or HDRP
            if (getUsedRenderPipeline() == null)
                renderPipeline = RenderPiplelineType.Standard; // Standard
            else if (!getUsedRenderPipeline().GetType().Name.Contains("Universal"))
                renderPipeline = RenderPiplelineType.HDRP; // HDRP

            return renderPipeline;
        }

        public static Shader GetDefaultShader()
        {
            if (getUsedRenderPipeline() == null)
                return Shader.Find("Standard");
            else
                return getUsedRenderPipeline().defaultShader;
        }

        /// <summary>
        /// Returns the current pipline. Returns NULL if it's the standard render pipeline.
        /// </summary>
        /// <returns></returns>
        static UnityEngine.Rendering.RenderPipelineAsset getUsedRenderPipeline()
        {
            if (UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline != null)
                return UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline;
            else
                return UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline;
        }

    }
}