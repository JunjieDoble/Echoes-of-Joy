using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;

    public string[] playerInventory;
    public int[] collectibles;

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
        playerInventory = new string[8];
        collectibles = new int[4];
        for (int i = 0; i < collectibles.Length; i++)
        {
            collectibles[i] = 0;
        }
    }

    public void AddItemToInventory(string item)
    {
        for (int i = 0; i < playerInventory.Length; i++)
        {
            if (playerInventory[i] == null)
            {
                if (item.Contains("Clone"))
                {
                    string baseItemName = item.Replace("(Clone)", "").Trim();
                    item = baseItemName;
                }
                playerInventory[i] = item;
                if (item.Equals("Crucefix"))
                {
                    UseCrucefix crucifixScript = this.GetComponent<UseCrucefix>();
                    if (crucifixScript != null)
                    {
                        crucifixScript.addCrucefix();
                    }
                }
                return;
            }
        }
    }

    public void AddCollectible(int id)
    {
        collectibles[id] = 1;
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

    public void RemoveItemFromInventory(string itemName)
    {
        for (int i = 0; i < playerInventory.Length; i++)
        {
            if (playerInventory[i] != null && playerInventory[i].Equals(itemName))
            {
                playerInventory[i] = null;
                return;
            }
        }
    }

    public int GetCollectibleCount()
    {
        int count = 0;
        foreach (int collectible in collectibles)
        {
            if (collectible == 1)
            {
                count++;
            }
        }
        return count;
    }
}