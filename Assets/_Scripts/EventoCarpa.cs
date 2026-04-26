using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class EventoCarpa : MonoBehaviour
{
    public LightingSettings lightingSettings;
    public Light directionalLight;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ActivateCarpaEvent());
        }
    }

    private IEnumerator ActivateCarpaEvent()
    {
        float elapsedTime = 0f;
        float duration = 2f;
        Color initialColor = directionalLight.color;
        Color targetColor = new Color(0f, 0f, 0f);
        while (elapsedTime < duration)
        {
            directionalLight.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration);
            UnityEngine.RenderSettings.ambientLight = Color.Lerp(Color.white, Color.black, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        directionalLight.color = targetColor; 
        UnityEngine.RenderSettings.ambientLight = Color.black;
        UnityEngine.RenderSettings.skybox = null;
        directionalLight.intensity = Mathf.Lerp(1f, 0f, Time.deltaTime);

    }
}

