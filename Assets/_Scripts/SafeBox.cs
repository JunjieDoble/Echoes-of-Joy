using System.Collections;
using TMPro;
using UnityEngine;

public class SafeBox : MonoBehaviour
{
    private Animator safeboxAnimator;
    public TextMeshProUGUI componentCountText;

    private void Awake()
    {
        safeboxAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCheckComponents playerCheck = other.GetComponent<PlayerCheckComponents>();
            if (playerCheck != null && playerCheck.hasAllComponents)
            {
                safeboxAnimator.SetBool("IsActive", true);
            }
            else
            {
                componentCountText.text = "You need all 3 components to open";
                componentCountText.gameObject.SetActive(true);
                StartCoroutine(HideText());
            }
        }
    }

    IEnumerator HideText()
    {
        this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(3.0f);
        componentCountText.gameObject.SetActive(false);
    }
}
