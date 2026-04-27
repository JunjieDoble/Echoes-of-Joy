using TMPro;
using UnityEngine;

public class StatueCollider : MonoBehaviour
{
    public StatueExorcise statueExorciseScript;
    public TextMeshProUGUI exorciseText;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            statueExorciseScript.rangeSetTrue();
            exorciseText.text = "Press Q to exorcise the statue!";
            exorciseText.gameObject.SetActive(true);
            Debug.Log("Player entered the statue range.");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            statueExorciseScript.rangeSetFalse();
            exorciseText.gameObject.SetActive(false);
            Debug.Log("Player exited the statue range.");
        }
    }

}
