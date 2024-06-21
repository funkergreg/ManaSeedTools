using UnityEngine;
using System.IO;
using System.Linq;

#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.U2D.Sprites;

#endif

namespace ManaSeedTools.FarmerSpriteSystem{
    /// <summary>
    ///     This class is intended to parse sprite sheets from the Mana Seed Farmer Sprite system, here:
    ///     https://seliel-the-shaper.itch.io/farmer-base
    ///     The script is based partly on these references, modified for Unity 2022.3.31f1:
    ///     https://docs.unity3d.com/Manual/Sprite-data-provider-api.html
    ///     https://forum.unity.com/threads/sprite-editor-automatic-slicing-by-script.320776/#post-9756150
    ///     This script was made after considering other resources like Mana Seed Character Animator:
    ///     https://feendrache.itch.io/mana-seed-character-animator-for-unity
    ///     For usage, see notes in the project's Readme.md
    /// </summary>
    public static class ManaSeedFarmerSpriteImporter{

        [MenuItem("Tools/Mana Seed Farmer Sprite Importer")]
        static void UpdateSettings(){
            // Below could be made into input arguments to parse other sizes of imports
            // (i.e. for a gui-based slicer)
            int sliceHeight = 64;
            int sliceWidth = 64;
            foreach (Texture2D obj in Selection.objects){
                string assetPath = AssetDatabase.GetAssetPath(obj);
                string filename = Path.GetFileName(assetPath);
                // Mod the Asset (the full sprite sheet)
                TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                if (textureImporter == null){
                    Debug.LogError("Failed to get TextureImporter for texture");
                } else{
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
                if (obj is Texture2D){
                    var factory = new SpriteDataProviderFactories();
                    factory.Init();
                    ISpriteEditorDataProvider dataProvider = factory.GetSpriteEditorDataProviderFromObject(obj);
                    dataProvider.InitSpriteEditorDataProvider();
                    // Below only for Unity 2021.2 and newer
                    var spriteNameFileIdDataProvider = dataProvider.GetDataProvider<ISpriteNameFileIdDataProvider>();
                    // A temp container for names
                    var nameFileIdPairs = spriteNameFileIdDataProvider.GetNameFileIdPairs().ToList();
                    var spriteRects = dataProvider.GetSpriteRects().ToList();
                    // Clear lists (so Unity does not add elements to current list)
                    nameFileIdPairs.Clear();
                    spriteRects.Clear();
                    // Like an index, uniqueName is used to create a unique name for each entry in the sprite sheet...
                    int uniqueName = 0;
                    // ... with three digits, including any leading zeroes
                    string leadingZeroesFormat = "000";
                    // NOTE: Mana Seed Farmer Sprite System base sheets have cell ids numbered 0-255, so using three digits is sufficient
                    // Slice sprites as Row then Column
                    for (int y = obj.height; y > 0; y -= sliceHeight){ // Row (starts at top)
                        for (int x = 0; x < obj.width; x += sliceWidth){ // Column (starts at left)
                            SpriteRect spriteRect = new SpriteRect(){
                                rect = new Rect(x, y - sliceHeight, sliceWidth, sliceHeight),
                                // Place uniqueName as prefix and concat filename with extension removed
                                // NOTE: Since the names may be 4 or 5 ID elements long, placing the sprite's unique cell id
                                //   (place in the sheet) at the front allows identification by parsing the name
                                //   such as when creating prefabs with other scripts
                                name = $"{uniqueName.ToString(leadingZeroesFormat)}_{filename.Replace(".png","")}",
                                pivot = new Vector2(0.5f, 0f),
                                alignment = SpriteAlignment.BottomCenter,
                                border = new Vector4(0,0,0,0),
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
                    // If wanting to use this script to generate object, use below (and change return type from void)
                    // return spriteRects.ToArray(); // returns SpriteRect[]
                } else{
                    Debug.LogError("obj is not Texture2D type");
                }
            }
        }
    }
}