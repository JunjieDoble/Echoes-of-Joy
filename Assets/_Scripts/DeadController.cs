using System.Collections;
using System.Linq;
using UnityEngine;

public class DeadController : MonoBehaviour
{
    public Checkpoint checkpoint;
    public DeadUI deadUI;
    public TipUI tipUI;

    public GameObject[] clowns; 
    public Transform[] clownSpawnPoints;
    public GameObject[] focos;

    public EventoLightsOut eventoLightsOut;
    public EventoFocos eventoFocos;
    public StatueCollider statueCollider;
    public InventoryController inventoryController;

    public GameObject[] objectsToRespawn;
    public Transform[] objectsToRespawnLocations;

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

        clownSpawnPoints = new Transform[clowns.Length];
        objectsToRespawnLocations = new Transform[objectsToRespawn.Length]; 

        for (int i = 0; i < clowns.Length; i++)
        {
            clownSpawnPoints[i] = clowns[i].transform;
            Debug.Log("clown with name " + clowns[i].name + " has spawn point " + clownSpawnPoints[i].position);
        }

        for (int i = 0; i < objectsToRespawn.Length; i++)
        {
            objectsToRespawnLocations[i] = objectsToRespawn[i].transform;
        }
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
    private void teleportClowns()
    {
        for (int i = 0; i < clowns.Length; i++)
        {
            clowns[i].transform.position = clownSpawnPoints[i].position;
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
        statueCollider.gameObject.SetActive(false);
        teleportClowns();

        foreach (GameObject clown in clowns)
        {
            if(clown.name.StartsWith("Final"))
            {
                clown.gameObject.SetActive(false);
            }
        }

        foreach (GameObject foco in focos)
        {
            foco.SetActive(false);
        }

        eventoLightsOut.carpaEventActivated = false;
        eventoFocos.focosEventActivated = false;

        respawnItems();

    }

    private void respawnItems()
    {
        inventoryController.RemoveItemFromInventory("redKey");
        inventoryController.RemoveItemFromInventory("Crucefix");
        inventoryController.RemoveItemFromInventory("Crowbar");
        inventoryController.RemoveItemFromInventory("blueKey");

        for (int i = 0; i < objectsToRespawn.Length; i++)
        {
            objectsToRespawn[i].transform.position = objectsToRespawnLocations[i].position;
            objectsToRespawn[i].gameObject.SetActive(true);
        }

    }
}
