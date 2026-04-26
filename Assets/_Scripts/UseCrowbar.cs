using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class UseCrowbar : MonoBehaviour
{
    public GameObject keyLock;
    public GameObject door;

    private Coroutine breakCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasCrowbar())
            {
                return;
            }
            breakCoroutine = StartCoroutine(BreakLock());
            InventoryController.Instance.RemoveItemFromInventory("Crowbar");
            
        }
    }

    private bool hasCrowbar()
    {
        return InventoryController.Instance.CheckForItem("Crowbar");
    }

    IEnumerator BreakLock()
    {
        keyLock.SetActive(false);
        Quaternion targetRot = door.transform.localRotation * Quaternion.Euler(0, -90, 0);

        float speed = 5f;

        while (Quaternion.Angle(door.transform.localRotation, targetRot) > 0.1f)
        {
            door.transform.localRotation = Quaternion.Slerp(door.transform.localRotation, targetRot, speed * Time.deltaTime);
            yield return null;
        }

        breakCoroutine = null;
        yield return new WaitForSeconds(1f);
    }
}