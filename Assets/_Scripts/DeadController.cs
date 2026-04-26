using System.Collections;
using System.Linq;
using UnityEngine;

public class DeadController : MonoBehaviour
{
    public Checkpoint checkpoint;
    public DeadUI deadUI;
    public TipUI tipUI;

    public GameObject[] clowns; 
    EventoLightsOut eventoLightsOut;
    EventoFocos eventoFocos;

    private bool isDead = false;
    private float deathTime = 4f;
    private GameObject player;
    private FPSController fpsController;
    private CharacterController characterController;


    private void Start()
    {
        player = this.gameObject;
        fpsController = player.GetComponent<FPSController>();
        characterController = player.GetComponent<CharacterController>();
    }

    public void PlayerDie()
    {
        if (!isDead)
        {
            isDead = true;
            deadUI.ShowDeadScreen();
            fpsController.SetAliveState(false);
            tipUI.ShowRandomTip();

            StartCoroutine(WaitAndRespawn());
        }
    }

    private IEnumerator WaitAndRespawn()
    {
        yield return new WaitForSeconds(deathTime);
        RespawnPlayer();
    }

    private void RespawnPlayer()
    {
        isDead = false;
        deadUI.HideDeadScreen();
        fpsController.SetAliveState(true);

        // Disable controllers before changing position to avoid unwanted physics interactions
        characterController.enabled = false;
        fpsController.enabled = false;
        player.transform.position = checkpoint.GetCheckpointPosition();
        characterController.enabled = true;
        fpsController.enabled = true;
        foreach (GameObject clown in clowns)
        {
            clown.SetActive(false);
        }
        eventoLightsOut.carpaEventActivated = false;
        eventoFocos.focosEventActivated = false;
    }
}
