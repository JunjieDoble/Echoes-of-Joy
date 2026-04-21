using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;

    private Coroutine openCoroutine;
    private Coroutine closeCoroutine;

    private bool isOpen = false;

    private Quaternion leftClosedRotation;
    private Quaternion rightClosedRotation;
    private void Start()
    {
        leftClosedRotation = leftDoor.transform.localRotation;
        rightClosedRotation = rightDoor.transform.localRotation;
    }

    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        if (openCoroutine != null) return;

        openCoroutine = StartCoroutine(OpenDoorSlerp());
    }

    IEnumerator OpenDoorSlerp()
    {
        Quaternion leftTargetRot = leftClosedRotation * Quaternion.Euler(0, -90, 0);
        Quaternion rightTargetRot = rightClosedRotation * Quaternion.Euler(0, 90, 0);

        float speed = 12f;

        while (Quaternion.Angle(leftDoor.transform.localRotation, leftTargetRot) > 0.1f)
        {
            leftDoor.transform.localRotation = Quaternion.Slerp(leftDoor.transform.localRotation, leftTargetRot, speed * Time.deltaTime);
            rightDoor.transform.localRotation = Quaternion.Slerp(rightDoor.transform.localRotation, rightTargetRot, speed * Time.deltaTime);
            yield return null;
        }

        openCoroutine = null;
        return new WaitForSeconds(3.5f);

    }

    private void CloseDoor()
    {
        if (openCoroutine != null)
        {
            StopCoroutine(openCoroutine);
            openCoroutine = null;
        }

        if (closeCoroutine != null) return;

        closeCoroutine = StartCoroutine(CloseDoorSlerp());

    }
    
    private IEnumerator CloseDoorSlerp()
    {
        float speed = 12f;
        while (Quaternion.Angle(leftDoor.transform.localRotation, leftClosedRotation) > 0.1f)
        {
            leftDoor.transform.localRotation = Quaternion.Slerp(leftDoor.transform.localRotation, leftClosedRotation, speed * Time.deltaTime);
            rightDoor.transform.localRotation = Quaternion.Slerp(rightDoor.transform.localRotation, rightClosedRotation, speed * Time.deltaTime);
            yield return null;
        }
        closeCoroutine = null;
    }

}
