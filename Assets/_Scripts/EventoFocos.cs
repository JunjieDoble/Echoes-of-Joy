using System.Collections;
using UnityEngine;

public class EventoFocos : MonoBehaviour
{
    public GameObject[] focos;
    public GameObject FinalFoco;
    public GameObject[] clownsToSpawn;
    public EventoLightsOut eventoLightsOut;

    public bool focosEventActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !focosEventActivated)
        {
            StartCoroutine(EncenderFocos());
            focosEventActivated = true;
        }
    }

    IEnumerator EncenderFocos()
    {
        foreach (GameObject foco in focos)
        {
            foco.SetActive(true);
            yield return new WaitForSeconds(0.9f);
        }
        clownsToSpawn[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        FinalFoco.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        eventoLightsOut.ReactivateLights();

        foreach (GameObject clown in clownsToSpawn)
        {
            clown.SetActive(true);
            EnemyBehaviour enemy = clown.GetComponent<EnemyBehaviour>();
            enemy.ActivateAlwaysChase();
        }
    }
}