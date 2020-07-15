# SnapTurn
This mod allows room rotation to be adjusted within Beat Saber's main menu. By default, rotations are made using the left controller's joystick or thumbpad.

## Configuration:
### Main settings
* **Enabled:** Rather self-explanatory.
* **Allow Snap Turning in songs:** Allows the player to turn within GameCore scenes. Disables score submission.
### Rotation settings
* **Smooth Turn:** Continues rotating the player until they release the turn button. Caps the maximum turn step at 5.
* **Rotation step:** Amount in degrees to rotate the player each turn.
### Input settings
* **Selected Controller:** Whether to use the left or right controller for rotations.
* **Input Type:** Determines how inputs are registered. Defaults to touch mode if the selected controller has a joystick.

## Known bugs:
* Mods which use tracking data rather than GameObjects (e.g CustomAvatars) may not rotate correctly.
