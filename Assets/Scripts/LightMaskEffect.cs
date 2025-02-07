using UnityEngine;

[ExecuteInEditMode]
public class LightMaskEffect : MonoBehaviour
{
    public Material lightMaskMaterial;
    public Texture2D lightMaskTexture;
    [Range(0, 1)] public float darknessLevel = 0.8f;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (lightMaskMaterial != null)
        {
            lightMaskMaterial.SetTexture("_LightMask", lightMaskTexture);
            lightMaskMaterial.SetFloat("_Darkness", darknessLevel);
            Graphics.Blit(src, dest, lightMaskMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}