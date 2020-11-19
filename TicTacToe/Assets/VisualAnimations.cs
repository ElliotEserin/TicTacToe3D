using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualAnimations : MonoBehaviour
{
    public Color cross, naught;
    public float defaultIntensity;

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
