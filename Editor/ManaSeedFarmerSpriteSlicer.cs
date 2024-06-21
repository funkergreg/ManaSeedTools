#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using ManaSeedTools.Scripts;

namespace ManaSeedTools.Editor {

    /// <summary>
    ///     This class is intended to parse sprite sheets from the Mana Seed Farmer Sprite system, here:
    ///     https://seliel-the-shaper.itch.io/farmer-base
    ///     This script was made after considering other resources like Mana Seed Character Animator:
    ///     https://feendrache.itch.io/mana-seed-character-animator-for-unity
    ///     For usage, see notes in the project's Readme.md
    /// </summary>
    public static class ManaSeedFarmerSpriteSlicer {

        [MenuItem("Tools/Mana Seed Farmer Sprite Slicer")]
        static void UpdateSettings() {
            int sliceHeight = 64;
            int sliceWidth = 64;
            foreach (Texture2D obj in Selection.objects) {
                SheetSlicers.SliceInPlace(obj, sliceHeight, sliceWidth);
            }
        }
    }
}

#endif