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

    private Coroutine exorcizeCoroutine;
    private Coroutine hideTextCoroutine;

    private bool isOnRange = false;
    public void OnExorcise(InputAction.CallbackContext context)
    {
        if (context.performed && isOnRange)
        {
            exorciseText.text = "Exorcising, stay close to the statue...";

            if (exorcizeCoroutine != null) return; 
                exorcizeCoroutine = StartCoroutine(Exorcise());

            Debug.Log("Exorcising the statue...");
            exorciseText.gameObject.SetActive(true);

            if (hideTextCoroutine != null) return; 
            hideTextCoroutine = StartCoroutine(HideText());
        }
    }

    public void rangeSetTrue()
    {
        isOnRange = true;
    }
    public void rangeSetFalse()
    {
        isOnRange = false;
        exorciseText.gameObject.SetActive(false);
    }

    IEnumerator HideText()
    {
        yield return new WaitForSeconds(4.5f);
        exorciseText.text = "The statue has been exorcised, now run!";
        exorciseText.gameObject.SetActive(true);
        door.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        exorciseText.gameObject.SetActive(false);
        statueCollider.gameObject.SetActive(false);
        hideTextCoroutine = null;
    }

    IEnumerator Exorcise()
    {
        exorciseText.text = "Exorcising, stay close to the statue...";
        yield return new WaitForSeconds(3.0f);
        exorcizeCoroutine = null;
    }

}