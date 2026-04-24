using UnityEngine;

public class Crucefix : MonoBehaviour
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
