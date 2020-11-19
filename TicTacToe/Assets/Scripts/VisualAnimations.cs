using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VisualAnimations : MonoBehaviour
{
    public Color cross, naught;
    public Texture crossSprite, naughtSprite;
    public float defaultIntensity;
    public float defaultBloomIntensity;

    PostProcessVolume volume;
    Bloom bloom;

    private void Awake()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out bloom);
    }

    public void ChangeBloomTexture(bool value)
    {
        bloom.dirtTexture.value = (value) ? crossSprite : naughtSprite;
        StartCoroutine(DimDirtIntensity());
    }

    IEnumerator DimDirtIntensity()
    {
        bloom.dirtIntensity.value = 10f;
        while (bloom.dirtIntensity - defaultBloomIntensity > 0.01)
        {
            bloom.dirtIntensity.value = Mathf.Lerp(bloom.dirtIntensity, defaultBloomIntensity, 3 * Time.deltaTime);
            yield return null;
        }
    }

    public void AnimateSkybox(bool value)
    {
        RenderSettings.skybox.SetColor("_Tint", (value) ? cross : naught);
        RenderSettings.skybox.SetFloat("_Exposure", 2);
        StartCoroutine(DimExposure());
    }

    IEnumerator DimExposure()
    {
        while (RenderSettings.skybox.GetFloat("_Exposure") - defaultIntensity > 0.01)
        {
            RenderSettings.skybox.SetFloat("_Exposure", Mathf.Lerp(RenderSettings.skybox.GetFloat("_Exposure"), defaultIntensity, 3 * Time.deltaTime));
            yield return null;
        }
    }
}
