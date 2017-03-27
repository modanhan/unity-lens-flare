using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GhostLensFlare : MonoBehaviour
{
    Material material;
    Material ghostMaterial;
    Material radialWarpMaterial;
    Material additiveMaterial;
    Material aberrationMaterial;

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
    [Range(0, 0.5f)]
    public float HaloWidth = 0.5f;
    public float HaloFalloff = 10;
    public float HaloSubtract = 1;

    public Color chromaticAberration = new Color(0, 16, 32);

    void OnEnable()
    {
        material = new Material(Shader.Find("Hidden/SubMul"));
        ghostMaterial = new Material(Shader.Find("Hidden/GhostFeature"));
        radialWarpMaterial = new Material(Shader.Find("Hidden/RadialWarp"));
        additiveMaterial = new Material(Shader.Find("Hidden/Additive"));
        aberrationMaterial = new Material(Shader.Find("Hidden/LensFlareAberration"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_Sub", Subtract);
        material.SetFloat("_Mul", Multiply);
        RenderTexture downsampled = RenderTexture.GetTemporary(Screen.width >> Downsample, Screen.height >> Downsample, 0, RenderTextureFormat.DefaultHDR);
        Graphics.Blit(source, downsampled, material);
        RenderTexture ghosts = RenderTexture.GetTemporary(Screen.width >> Downsample, Screen.height >> Downsample, 0, RenderTextureFormat.DefaultHDR);
        ghostMaterial.SetInt("_NumGhost", NumberOfGhosts);
        ghostMaterial.SetFloat("_Displace", Displacement);
        ghostMaterial.SetFloat("_Falloff", Falloff);
        Graphics.Blit(downsampled, ghosts, ghostMaterial);
        RenderTexture radialWarped = RenderTexture.GetTemporary(Screen.width >> Downsample, Screen.height >> Downsample, 0, RenderTextureFormat.DefaultHDR);

        radialWarpMaterial.SetFloat("_HaloFalloff", HaloFalloff);
        radialWarpMaterial.SetFloat("_HaloWidth", HaloWidth);
        radialWarpMaterial.SetFloat("_HaloSub", HaloSubtract);
        Graphics.Blit(source, radialWarped, radialWarpMaterial);

        additiveMaterial.SetTexture("_MainTex1", radialWarped);

        RenderTexture added = RenderTexture.GetTemporary(Screen.width, Screen.height, 0, RenderTextureFormat.DefaultHDR);
        Graphics.Blit(ghosts, added, additiveMaterial);

        RenderTexture aberration = RenderTexture.GetTemporary(Screen.width, Screen.height, 0, RenderTextureFormat.DefaultHDR); ;

        aberrationMaterial.SetColor("_DisplaceColor", chromaticAberration);
        Graphics.Blit(added, aberration, aberrationMaterial);

        additiveMaterial.SetTexture("_MainTex1", aberration);
        Graphics.Blit(source, destination, additiveMaterial);

        RenderTexture.ReleaseTemporary(downsampled);
        RenderTexture.ReleaseTemporary(ghosts);
        RenderTexture.ReleaseTemporary(radialWarped);
        RenderTexture.ReleaseTemporary(added);
        RenderTexture.ReleaseTemporary(aberration);
    }
}
