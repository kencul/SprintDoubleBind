# Sprint Double Bind

A Risk of Rain 2 mod for adding a secondary bind to sprinting.

As RoR2 does not support secondary keybinds, this mod allows provides a second keybind for sprinting.

Allows binding any key, mouse button and mouse scroll up and down to toggle sprint.

## Use Cases

- Add a secondary bind to a side mouse button, so sprinting is easier when your vanilla keybind is awkward to use. Ex. charging a loader punch then sprinting.

- Binding scroll wheel down to sprint toggle (RoR2 naitively only supports binding scroll wheel up). This is specifically useful for the Acrid sprint cancel tech.

![Acrid sprint cancel tech demo gif](https://github.com/kencul/SprintDoubleBind/blob/main/acridDemo.gif?raw=true)

## Features

- **Multi-Input Support**: Toggle sprint using your mouse wheel (up/down) or a custom keyboard/mouse button.

- **BepInEx Configuration**: All bindings and features are fully configurable to enable/disable features, and set any desired key.

- **Risk of Options Support**: Easily change all settings directly from the in-game Mod Settings menu.

## Installation

**Recommended**: Use [r2modman](https://thunderstore.io/package/ebkr/r2modman/)install. The manager 
**Manual**: Install [BepInEx](https://thunderstore.io/package/bbepis/BepInExPack/) and place the mod (and the dependencies) in the `Risk of Rain 2\BepInEx\plugins` folder	

## Configurable Options

- **SprintScrollUpEnabled**: If enabled, scrolling your mouse wheel up will toggle sprint.
- **SprintScrollDownEnabled**: If enabled, scrolling your mouse wheel down will toggle sprint.
- **SprintKeyEnabled**: If enabled, allows the use of the custom keyboard/mouse bind below to toggle sprint.
- **SprintBindKey**: The key or mouse button that toggles sprint (if Sprint Key Enabled is true). Supports all standard keys and mouse buttons.