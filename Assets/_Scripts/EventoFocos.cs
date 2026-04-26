using System.Collections;
using UnityEngine;

public class EventoFocos : MonoBehaviour
{
    public GameObject[] focos;
    public GameObject FinalFoco;
    public GameObject[] clownsToSpawn;
    public EventoLightsOut eventoLightsOut;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EncenderFocos());
        }
    }

    IEnumerator EncenderFocos()
    {
        foreach (GameObject foco in focos)
        {
            foco.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
        clownsToSpawn[0].SetActive(true);
        yield return new WaitForSeconds(2f);
        FinalFoco.SetActive(true);
        yield return new WaitForSeconds(2f);
        eventoLightsOut.ReactivateLights();

        foreach (GameObject clown in clownsToSpawn)
        {
            clown.SetActive(true);
        }
    }
}