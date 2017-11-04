# Note

Hey! Actually check out this version instead: https://github.com/modanhan/unity-lens-flare-1

# Unity Lens Flare

Lens Flare using Ghost features implemented in Unity.

![Sample Image](https://github.com/spoonsplz/unity-lens-flare/blob/master/Lens%20Flare.PNG)

# Video

Check out this video!

https://www.youtube.com/watch?v=xzwo0muslE8

# Concept

Original blog post by John Chapman: http://john-chapman-graphics.blogspot.ca/2013/02/pseudo-lens-flare.html

My implementation is a simplified version of the lens flare described in this blog optimized for Unity.

# Usage

Attach script GhostLensFlare to the camera.

HDR rendering is highly recommended, altho not required, lens flare will not look correct without HDR and tonemapping.

Linear color space is recommended.

A test scene Main.unity is included. Notice the main camera also has bloom and tonemapping.

## Notes

* If you want to try exporting a package, everything you need to use the lens flare is under the directory Assets/Lens Flare.
* GhostLensFlare should go after motion vector based motion blur; before bloom and tonemapping.
* Dirty lens effect is NOT included with this implementation; I believe it is correct to apply dirty lens with bloom.

# Known Issues
* I am unsure how this lens flare will work with Unity's new Post Processing Stack (https://github.com/Unity-Technologies/PostProcessing).
