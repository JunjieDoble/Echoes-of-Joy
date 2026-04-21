using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;

    private Quaternion leftClosedRotation;
    private Quaternion rightClosedRotation;
    private void Start()
    {
        leftClosedRotation = leftDoor.transform.localRotation;
        rightClosedRotation = rightDoor.transform.localRotation;
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
        leftDoor.transform.localRotation = Quaternion.Euler(leftClosedRotation.eulerAngles + new Vector3(0, -90, 0));
        rightDoor.transform.localRotation = Quaternion.Euler(rightClosedRotation.eulerAngles + new Vector3(0, 90, 0));
    }

    private void CloseDoor()
    {
        leftDoor.transform.localRotation = leftClosedRotation;
        rightDoor.transform.localRotation = rightClosedRotation;
    }
}
