# ManaSeedTools

This repository contains hobbyist code and tools to work with *Mana Seed* assets in Unity.  *Mana Seed* is a trademark whiolly owned by [Seliel the Shaper](https://www.patreon.com/selieltheshaper).  This reporitory is intended as a community resource to help beginning developers get stated with these assets.  All code in this repository is provided as-is, with no guarantee of future updates.

## Mana Seed

- The full collection of *Mana Seed* assets is available at <https://seliel-the-shaper.itch.io/>
- This project is intended specifically to work with sprite sheets from the *Mana Seed Farmer Sprite System*: <https://seliel-the-shaper.itch.io/farmer-base>

## Unity

- The Unity engine: <https://unity.com/products/unity-engine>
- Seliel's Assets in the Unity store: <https://assetstore.unity.com/publishers/46913>

## Mana Seed Tools

This repository is designed as a separate project directory that can work with *Mana Seed* assets in place, with as little moving or custom directories as possible so that the tools can be re-run as necessary to interact with any future *Mana Seed* asset releases.  The tools are designed as small-ish modular steps to support chaining these together to do more complex work.

This repository uses `namespace ManaSeedTools`.  At least the [Mana Seed Character Animator](https://feendrache.itch.io/mana-seed-character-animator-for-unity) uses this namespace base as `namespace ManaSeedTools.CharacterAnimator`.  Since [MSCA](https://schattenhandel.de/msca/msca-tutorial-version-2-x/) is not being maintained as of JUN 2024, hopefully there will be no future conflicts with the namespace and plain naming techniques, such as this repo's use of `ManaSeedTools.FarmerSpriteSystem`.

### Mana Seed Farmer Sprite Importer

The *Mana Seed Farmer Sprite Importer* is a script that adds a menu item to the Unity editor under `Tools` to slice the *Mana Seed Farmer Sprite System* base sheets.  Clicking the menu item while one or more farmer base sheet assets are selected will slice and name the individual sprites from the sheet.  The script leaves the original sprite sheet assets in place while updating Unity's data about how to use the assets.
