#if UNITY_EDITOR

using UnityEngine;
using System.Linq;
using UnityEditor.U2D.Sprites;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace ManaSeedTools.Scripts{

    public static class SheetSlicers{

        /// <summary>
        /// Only use on single resources. 
        /// Uses same params as <see cref="MakeSlices"/>
        /// </summary>
        public static SpriteRect[] GetSlicedSpriteArray(Texture2D obj, int sliceHeight, int sliceWidth) {
            List<SpriteRect> spriteRects = MakeSlices(obj, sliceHeight, sliceWidth);
            return spriteRects.ToArray(); // returns SpriteRect[]
        }

        /// <summary>
        /// Use on single or multiple resources (does not return the resources to caller). 
        /// Uses same params as <see cref="MakeSlices"/>
        /// </summary>
        public static void SliceInPlace(Texture2D obj, int sliceHeight, int sliceWidth) {
            MakeSlices(obj, sliceHeight, sliceWidth);
        }

        /// <summary>
        ///   Can be used on any/multiple assets.
        ///   Script is based partly on these references, modified for Unity 2022.3.31f1:
        ///   https://docs.unity3d.com/Manual/Sprite-data-provider-api.html
        ///   https://forum.unity.com/threads/sprite-editor-automatic-slicing-by-script.320776/#post-9756150
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="sliceHeight"></param>
        /// <param name="sliceWidth"></param>
        /// <returns> a <c>List<SpriteRect></c> - may return an empty list </returns>
        static List<SpriteRect> MakeSlices(Texture2D obj, int sliceHeight, int sliceWidth){
            // Init the return object
            List<SpriteRect> spriteRects = new();
            
            string assetPath = AssetDatabase.GetAssetPath(obj);
            string filename = Path.GetFileName(assetPath);
            // Mod the Asset (the full sprite sheet)
            TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (textureImporter == null) {
                Debug.LogError("Failed to get TextureImporter for texture");
            } else {
                // Set type to sprite
                textureImporter.textureType = TextureImporterType.Sprite;
                // Set to 'multiple' mode
                textureImporter.spriteImportMode = SpriteImportMode.Multiple;
                // Ensure no complression
                textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
                // Reimport the texture with updated settings
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            }
            // Slice full sheet into individual sprites
            if (obj is Texture2D) {
                var factory = new SpriteDataProviderFactories();
                factory.Init();
                ISpriteEditorDataProvider dataProvider = factory.GetSpriteEditorDataProviderFromObject(obj);
                dataProvider.InitSpriteEditorDataProvider();
                // Below only for Unity 2021.2 and newer
                var spriteNameFileIdDataProvider = dataProvider.GetDataProvider<ISpriteNameFileIdDataProvider>();
                // Containers
                var nameFileIdPairs = spriteNameFileIdDataProvider.GetNameFileIdPairs().ToList();
                spriteRects = dataProvider.GetSpriteRects().ToList();
                // Clear containers (so Unity does not add elements to current)
                nameFileIdPairs.Clear();
                spriteRects.Clear();
                // Like an index, uniqueName is used to create a unique name for each entry in the sprite sheet...
                int uniqueName = 0;
                // ... with three digits, including any leading zeroes
                string leadingZeroesFormat = "000";
                // NOTE: Mana Seed Farmer Sprite System base sheets have cell ids numbered 0-255, so using three digits is sufficient
                // Slice sprites as Row then Column
                for (int y = obj.height; y > 0; y -= sliceHeight) { // Row (starts at top)
                    for (int x = 0; x < obj.width; x += sliceWidth) { // Column (starts at left)
                        SpriteRect spriteRect = new SpriteRect() {
                            rect = new Rect(x, y - sliceHeight, sliceWidth, sliceHeight),
                            // Place uniqueName as prefix and concat filename with extension removed
                            // NOTE: Since the names may be 4 or 5 ID elements long, placing the sprite's unique cell id
                            //   (place in the sheet) at the front allows identification by parsing the name
                            //   such as when creating prefabs with other scripts
                            name = $"{uniqueName.ToString(leadingZeroesFormat)}_{filename.Replace(".png", "")}",
                            pivot = new Vector2(0.5f, 0f),
                            alignment = SpriteAlignment.BottomCenter,
                            border = new Vector4(0, 0, 0, 0),
                            spriteID = GUID.Generate()
                        };
                        spriteRects.Add(spriteRect);
                        // Below only for Unity 2021.2 and newer
                        // Register the new Sprite Rect's name and GUID with the ISpriteNameFileIdDataProvider
                        nameFileIdPairs.Add(new SpriteNameFileIdPair(spriteRect.name, spriteRect.spriteID));
                        // Update index (for naming only)
                        uniqueName++;
                    }
                }
                // Set sprites (and ID provider details) to data provider
                dataProvider.SetSpriteRects(spriteRects.ToArray());
                spriteNameFileIdDataProvider.SetNameFileIdPairs(nameFileIdPairs);
                // Apply the changes made to the data provider
                dataProvider.Apply();
                // Reimport the asset to have the changes applied
                var assetImporter = dataProvider.targetObject as AssetImporter;
                assetImporter.SaveAndReimport();
            } else {
                Debug.LogError($"Asset obj at {assetPath} is not Texture2D type");
            }
            // Note no checks are being performed that there is anything in the list
            return spriteRects;
        }
    }
}

#endif
