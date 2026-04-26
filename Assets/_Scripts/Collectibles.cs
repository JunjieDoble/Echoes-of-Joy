using System.Collections;
using TMPro;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public TextMeshProUGUI Congratulations;
    public TextMeshProUGUI count;

    public int id;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryController.Instance.AddCollectible(id);
            //
            Congratulations.gameObject.SetActive(true);
            count.text = InventoryController.Instance.GetCollectibleCount() + " / 4";
            count.gameObject.SetActive(true);

            StartCoroutine(HideText());
            //Destroy(gameObject);
        }
    }

    IEnumerator HideText()
    {
        this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(3.0f);
        Congratulations.gameObject.SetActive(false);
        count.gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
