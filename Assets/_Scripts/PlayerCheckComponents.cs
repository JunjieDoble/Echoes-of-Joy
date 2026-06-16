using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerCheckComponents : MonoBehaviour
{
    private int componentCount = 0;
    public bool hasAllComponents = false;
    public TextMeshProUGUI componentCountText;

    public void AddComponent()
    {
        componentCount++;

        componentCountText.SetText("Components Collected: " + componentCount + "/3");
        componentCountText.gameObject.SetActive(true);

        StartCoroutine(HideText());
        CheckComponentsCount();
    }

    public void CheckComponentsCount()
    {
        if (componentCount >= 3)
        {
            hasAllComponents = true;
        }
    }

    IEnumerator HideText()
    {
        this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(3.0f);
        componentCountText.gameObject.SetActive(false);
    }
}
