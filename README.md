# ManaSeedTools

This repository contains work-in-progress hobbyist code and tools to work with *Mana Seed* assets in Unity.  *Mana Seed* is a trademark wholly owned by [Seliel the Shaper](https://www.patreon.com/selieltheshaper).  This reporitory is intended as a community resource to share code that may help beginning developers get stated with *Mana Seed* assets.

## Mana Seed

- The full collection of *Mana Seed* assets is available at <https://seliel-the-shaper.itch.io/>
- This project is intended specifically to work with sprite sheets from the *Mana Seed Farmer Sprite System*: <https://seliel-the-shaper.itch.io/farmer-base>

## Unity

- The Unity engine: <https://unity.com/products/unity-engine>
- *Mana Seed* Assets in the Unity store: <https://assetstore.unity.com/publishers/46913>

## Mana Seed Tools

This repository is designed as a separate project directory (i.e. clone the *ManaSeedTools* git repo under your project's `Assets` folder).  The tools work with *Mana Seed* assets in place (assumed a separate folder), with as little movement of the original assets or and as few custom directories as possible.  The tools are designed as small-ish modular steps to support chaining these together to do more complex work.  Ideally, this repo's tools can be re-run as necessary to interact with any future *Mana Seed* asset releases.

This repository uses `namespace ManaSeedTools`.  At least the [Mana Seed Character Animator](https://feendrache.itch.io/mana-seed-character-animator-for-unity) used this namespace base as `namespace ManaSeedTools.CharacterAnimator`.  Since [MSCA](https://schattenhandel.de/msca/msca-tutorial-version-2-x/) is not being maintained as of JUN 2024, hopefully there will be no future conflicts with the namespace and plain naming techniques, such as this repo's use of `ManaSeedTools.FarmerSpriteSystem`.

### Mana Seed Farmer Sprite Importer

The *Mana Seed Farmer Sprite Importer* is a script that adds a menu item to the Unity editor under `Tools` to slice the *Mana Seed Farmer Sprite System* base sheets.  Clicking the menu item while one or more farmer base sheet assets are selected will slice and name the individual sprites from the sheet.  The script leaves the original sprite sheet assets in place while updating Unity's data about how to use the assets.

## Licensing

All code in this repository is provided as open source resources as-is, with no guarantee of functionality, interoperability, or future updates.
