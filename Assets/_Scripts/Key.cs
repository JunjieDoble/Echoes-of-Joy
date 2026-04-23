using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryController.Instance.AddItemToInventory(gameObject.name);
            Destroy(gameObject);
        }
    }
}
