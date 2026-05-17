# Editable Interactive VR Gaussian Splatting

Project website: https://editgaussianvr.github.io/

A Unity VR viewer for Meta Quest that loads 3D Gaussian Splatting scenes and lets the user switch in real time between the original scene and edited variants (style transfer, text-based editing, object removal). Built on [ninjamode/Unity-VR-Gaussian-Splatting](https://github.com/ninjamode/Unity-VR-Gaussian-Splatting).

This repository contains only the Unity VR viewer. The offline editing pipelines and the large Gaussian Splat data are not included here.

## Requirements

- Unity 2022.3.x (developed on 2022.3.51f1), URP
- Meta Quest headset with Developer Mode enabled
- The Gaussian Splat asset files (large `.ply`/`.bytes` data — not in this repo, must be obtained separately)

## Run

1. Clone the repository.
2. Place the Gaussian Splat assets into:
   - `VR-URP/Assets/GaussianAssets_styles/`
   - `VR-URP/Assets/GaussianAssets_editing/`
   - `VR-URP/Assets/GaussianAssets_removal/`
3. Open the `VR-URP/` folder as a project in Unity Hub (Unity 2022.3.x).
4. Open the scene named **Demo** under `Assets/`.
5. `File > Build Settings`, set platform to **Android**, connect the Quest, and click **Build And Run**.

## Controls

- **B button (right controller)** — cycle to the next splat variant. A floating label shows the current variant's name.
- **Left / Right arrow keys** — cycle variants in the Unity editor (desktop testing, no headset needed).
- Standard XRI locomotion (move/turn/teleport); custom vertical movement via `VRVerticalMove`.
