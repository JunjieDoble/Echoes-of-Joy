using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public StunHint itemHintToEnable;
    public StunHint itemHintToDisable;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryController.Instance.AddItemToInventory(gameObject.name);
            enableItemHint();
            disableItemHint();
            Destroy(gameObject);
        }
    }

    private void enableItemHint()
    {
        if (itemHintToEnable != null)
        {
            itemHintToEnable.EnableHintTrigger();
        }
    }

        private void disableItemHint()
        {
            if (itemHintToDisable != null)
            {
                itemHintToDisable.DisableHintTrigger();
            }
    }
}
