using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EventoLightsOut : MonoBehaviour
{
    public Light directionalLight;
    public bool carpaEventActivated = false;
    public Camera mainCam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !carpaEventActivated)
        {
            StartCoroutine(DesactivarLuces());
            carpaEventActivated = true;
        }
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
        directionalLight.color = Color.white;
        directionalLight.intensity = 1f;
        RenderSettings.ambientLight = Color.white;
        RenderSettings.fog = true;
        carpaEventActivated = false;

    }


}