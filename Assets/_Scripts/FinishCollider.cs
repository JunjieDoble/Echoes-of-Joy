using System.Collections;
using System.Threading;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class FinishCollider : MonoBehaviour
{
    public TextMeshProUGUI victoryText;
    public InventoryController inventoryController;

    private int count = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (inventoryController != null)
            {
                count = inventoryController.GetCollectibleCount();
            }
            victoryText.text = "Congratulations! You escaped the circus and collected " + count + " out of 4 collectibles!";
            victoryText.gameObject.SetActive(true);
            StartCoroutine(HideHint());

        }
    }

    IEnumerator HideHint()
    {
        yield return new WaitForSeconds(4.0f);
        victoryText.gameObject.SetActive(false);
        Application.Quit();
    }
}
