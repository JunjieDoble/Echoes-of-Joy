using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;

    public string[] playerInventory;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        playerInventory = new string[2];
    }

    public void AddItemToInventory(string item)
    {
        for (int i = 0; i < playerInventory.Length; i++)
        {
            if (playerInventory[i] == null)
            {
                playerInventory[i] = item;
                Debug.Log("Item added to inventory: " + item);
                return;
            }
        }
        Debug.Log("Inventory is full. Cannot add item: " + item);
    }

    public bool CheckForItem(string itemName)
    {
        foreach (string item in playerInventory)
        {
            if (item != null && item.Equals(itemName))
            {
                return true;
            }
        }

        return false;
    }
}