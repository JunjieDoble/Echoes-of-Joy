using System.Collections;
using TMPro;
using UnityEngine;

public class StunHint : MonoBehaviour
{
    public TextMeshProUGUI hint;
    public string hintMessage = "Press E to stun the clown";
    public bool hasHintTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasHintTrigger)
        {
            hasHintTrigger = true;
            hint.text = hintMessage;
            hint.gameObject.SetActive(true);

            StartCoroutine(HideHint());
        }
    }

    IEnumerator HideHint()
    {
        yield return new WaitForSeconds(4.0f);
        hint.gameObject.SetActive(false);
    }

    public void DisableHintTrigger()
    {
        hasHintTrigger = true;
    }

    public void EnableHintTrigger()
    {
        hasHintTrigger = false;
    }
}
