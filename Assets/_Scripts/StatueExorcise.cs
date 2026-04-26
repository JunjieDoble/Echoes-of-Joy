using System.Collections;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class StatueExorcise : MonoBehaviour
{
    public Collider statueCollider;
    public TextMeshProUGUI exorciseText;
    public GameObject door;

    private bool isOnRange = false;
    public void OnExorcise(InputAction.CallbackContext context)
    {
        if (context.performed && isOnRange)
        {
            StartCoroutine(Exorcise());
            Debug.Log("Exorcising the statue...");
            
            exorciseText.gameObject.SetActive(true);
            statueCollider.enabled = false;
            StartCoroutine(HideText());
        }
    }

    public void rangeSetTrue()
    {
        isOnRange = true;
    }
    public void rangeSetFalse()
    {
        isOnRange = false;
    }

    IEnumerator HideText()
    {
        exorciseText.text = "The statue has been exorcised, now run!";
        door.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        exorciseText.gameObject.SetActive(false);
    }

    IEnumerator Exorcise()
    {
        exorciseText.text = "Exorcising...!";
        yield return new WaitForSeconds(3.0f);
    }

}