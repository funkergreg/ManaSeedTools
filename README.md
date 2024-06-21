# ManaSeedTools

This repository contains code to work with *Mana Seed* assets in Unity.

## Mana Seed

This repository is designed to worh with *Mana Seed* assets by [Seliel the Shaper](https://www.patreon.com/selieltheshaper)

See the full collection of *Mana Seed* assets here:
<https://seliel-the-shaper.itch.io/>

This project is intended to specifically work with sprite sheets from the *Mana Seed Farmer Sprite System*, available here:
<https://seliel-the-shaper.itch.io/farmer-base>

## Unity

- The Unity engine: <https://unity.com/products/unity-engine>
- Seliel's Assets in the Unity store: <https://assetstore.unity.com/publishers/46913>

## Mana Seed Tools

This repository is designed as a separate repositry to work with *Mana Seed* assets in place, with as little moving or custom directories as possible so that the tools can be re-run as necessary to interact with any future *Mana Seed* asset releases.  The tools are being designed as small-ish modular steps to support chaining these together to do more complex work.

This repository uses `namespace ManaSeedTools`.  At least the [Mana Seed Character Animator](https://feendrache.itch.io/mana-seed-character-animator-for-unity) uses this namespace base as `namespace ManaSeedTools.CharacterAnimator`.  Since [MSCA](https://schattenhandel.de/msca/msca-tutorial-version-2-x/) is not being maintained as of JUN 2024, hopefully there will be no future conflicts with the namespace and plain naming techniques, such as this repo's use of `ManaSeedTools.FarmerSpriteSystem`.

### Mana Seed Farmer Sprite Importer

The `Mana Seed Farmer Sprite Importer` is a script that adds a menu item to the Unity editor under `Tools` to slice the *Mana Seed Farmer Sprite System*'s base sheets.  Clicking the menu item while one or more farmer base sheet assets are selected will slice and name the individual sprites from the sheet.  The script leaves the sprite sheet assets in place while updating Unity's data about how to use the assets.
