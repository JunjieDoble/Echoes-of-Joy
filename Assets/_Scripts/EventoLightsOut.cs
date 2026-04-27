using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EventoLightsOut : MonoBehaviour
{
    public Light directionalLight;
    public bool carpaEventActivated = false;
    public Camera mainCam;
    public TextMeshProUGUI exorciseHintText;

    private Color originalLightColor;
    private float originalLightIntensity;
    private Color originalAmbientLight;
    private Material originalSkybox;
    private bool originalFog;
    private Color originalFogColor;
    private float originalFogDensity;
    private AmbientMode originalAmbientMode;
    private DefaultReflectionMode originalReflectionMode;
    private Cubemap originalReflectionTexture;
    private Color originalCamBackground;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !carpaEventActivated)
        {
            SaveOriginalState();
            StartCoroutine(DesactivarLuces());
            exorciseHint();
            carpaEventActivated = true;
        }
    }

    void SaveOriginalState()
    {
        originalLightColor = directionalLight.color;
        originalLightIntensity = directionalLight.intensity;

        originalAmbientLight = RenderSettings.ambientLight;
        originalAmbientMode = RenderSettings.ambientMode;

        originalSkybox = RenderSettings.skybox;

        originalFog = RenderSettings.fog;
        originalFogColor = RenderSettings.fogColor;
        originalFogDensity = RenderSettings.fogDensity;

        originalReflectionMode = RenderSettings.defaultReflectionMode;
        originalCamBackground = mainCam.backgroundColor;
    }

    public IEnumerator DesactivarLuces()
    {
        RenderSettings.skybox = null;

        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = Color.black;

        RenderSettings.defaultReflectionMode = UnityEngine.Rendering.DefaultReflectionMode.Custom;
        RenderSettings.customReflectionTexture = null;

        RenderSettings.skybox = null;
        DynamicGI.UpdateEnvironment();

        RenderSettings.fogColor = Color.black;
        RenderSettings.fogDensity = 0.05f;
        RenderSettings.fog = false;

        float elapsedTime = 0f;
        float duration = 2f;

        Color initialColor = directionalLight.color;
        float initialIntensity = directionalLight.intensity;
        Color initialAmbient = RenderSettings.ambientLight;

        RenderSettings.ambientMode = AmbientMode.Flat;

        mainCam.backgroundColor = Color.black;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            directionalLight.color = Color.Lerp(initialColor, Color.black, t);
            directionalLight.intensity = Mathf.Lerp(initialIntensity, 0f, t);
            RenderSettings.ambientLight = Color.Lerp(initialAmbient, Color.black, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        directionalLight.color = Color.black;
        directionalLight.intensity = 0f;
        RenderSettings.ambientLight = Color.black;
    }

    public void ReactivateLights()
    {
        directionalLight.color = originalLightColor;
        directionalLight.intensity = originalLightIntensity;

        RenderSettings.ambientMode = originalAmbientMode;
        RenderSettings.ambientLight = originalAmbientLight;

        RenderSettings.skybox = originalSkybox;

        RenderSettings.fog = originalFog;
        RenderSettings.fogColor = originalFogColor;
        RenderSettings.fogDensity = originalFogDensity;

        RenderSettings.defaultReflectionMode = originalReflectionMode;
        RenderSettings.customReflectionTexture = originalReflectionTexture;

        mainCam.backgroundColor = originalCamBackground;

        DynamicGI.UpdateEnvironment();
    }

    private void exorciseHint()
    {
        exorciseHintText.gameObject.SetActive(true);
        exorciseHintText.text = "That's the statue! \n I must exorcize it as soon as possible.";
        StartCoroutine(HideText());
    }

    IEnumerator HideText()
    {
        yield return new WaitForSeconds(3f);
        exorciseHintText.gameObject.SetActive(false);

    }


}