using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject[] playerInventory;

    private void Start()
    {
        playerInventory = new GameObject[5];
    }

    public void AddItemToInventory(GameObject item)
    {
        for (int i = 0; i < playerInventory.Length; i++)
        {
            if (playerInventory[i] == null)
            {
                playerInventory[i] = item;
                Debug.Log("Item added to inventory: " + item.name);
                return;
            }
        }
        Debug.Log("Inventory is full. Cannot add item: " + item.name);
    }

    public bool checkForItem(string itemName)
    {
        foreach (GameObject item in playerInventory)
        {
            if (item != null && item.name == itemName)
            {
                return true;
            }
        }
        return false;
    }
}
