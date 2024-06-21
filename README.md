# ManaSeedTools

This repository contains work-in-progress hobbyist code and tools to work with *Mana Seed* assets in Unity.  *Mana Seed* is a trademark owned by [Seliel the Shaper](https://www.patreon.com/selieltheshaper).  This reporitory is intended as a community resource to share code snippets and Unity Editor tools that may help developers (especially beginners like me) get stated with *Mana Seed* assets.

## Mana Seed

- The full collection of *Mana Seed* assets is available at <https://seliel-the-shaper.itch.io/>
- This project is intended specifically to work with sprite sheets from the *Mana Seed Farmer Sprite System*: <https://seliel-the-shaper.itch.io/farmer-base>

## Unity

- The Unity engine: <https://unity.com/products/unity-engine>
- *Mana Seed* assets in the Unity store: <https://assetstore.unity.com/publishers/46913>

## Mana Seed Tools

This repository is designed as a separate project directory (i.e. clone the *ManaSeedTools* git repo under your project's `Assets` folder).  The tools work with *Mana Seed* assets in place (assumed a separate folder), with as little movement as possible of the original *Mana Seed* assets and as few custom directories as possible.  The tools are designed as small-ish modular steps to support chaining utilities together to do more complex (or repetitive) work.  Ideally, users can re-run this repo's tools as necessary to interact with future *Mana Seed* releases and update pre-existing game prefabs made assets from older versions.

This repository uses `namespace ManaSeedTools`.  At least the [Mana Seed Character Animator](https://feendrache.itch.io/mana-seed-character-animator-for-unity) used this namespace base as `namespace ManaSeedTools.CharacterAnimator`.  Since [MSCA](https://schattenhandel.de/msca/msca-tutorial-version-2-x/) is not being maintained as of JUN 2024, hopefully there will be no future conflicts with the namespace and plain naming techniques in the `ManaSeedTools` namespace.

### Mana Seed Farmer Sprite Importer

The *Mana Seed Farmer Sprite Importer* is a script that adds a menu item to the Unity editor under `Tools` to slice the *Mana Seed Farmer Sprite System* base sheets.  Clicking the menu item while one or more farmer base sheet assets are selected will slice and name the individual sprites from the sheet.  The script leaves the original sprite sheet assets in place while updating Unity's data about how to use the assets.

## Licensing

All code in this repository is provided as open source resources as-is, with no guarantee of functionality, interoperability, or future updates.
