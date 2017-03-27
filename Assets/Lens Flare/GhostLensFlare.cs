using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GhostLensFlare : MonoBehaviour
{
    Material material;
    Material ghostMaterial;

    public float Subtract = 0.5f;
    [Range(0, 1)]
    public float Multiply = 1;
    [Range(0, 6)]
    public int Downsample = 0;
    [Range(0, 8)]
    public int NumberOfGhosts = 2;
    [Range(0, 2)]
    public float Displacement = 0.1f;
    public float Falloff = 10;
    void OnEnable()
    {
        material = new Material(Shader.Find("Hidden/SubMul"));
        ghostMaterial = new Material(Shader.Find("Hidden/GhostFeature"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_Sub", Subtract);
        material.SetFloat("_Mul", Multiply);
        RenderTexture downsampled = RenderTexture.GetTemporary(Screen.width >> Downsample, Screen.height >> Downsample, 0, RenderTextureFormat.Default);
        Graphics.Blit(source, downsampled, material);
        RenderTexture ghosts = RenderTexture.GetTemporary(Screen.width >> Downsample, Screen.height >> Downsample, 0, RenderTextureFormat.Default);
        ghostMaterial.SetInt("_NumGhost", NumberOfGhosts);
        ghostMaterial.SetFloat("_Displace", Displacement);
        ghostMaterial.SetFloat("_Falloff", Falloff);
        Graphics.Blit(downsampled, ghosts, ghostMaterial);
        Graphics.Blit(ghosts, destination);
        RenderTexture.ReleaseTemporary(downsampled);
        RenderTexture.ReleaseTemporary(ghosts);
    }
}
