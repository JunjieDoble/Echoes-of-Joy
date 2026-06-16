using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RoomDoorController : MonoBehaviour
{
    private Animator _doorAnimator;
    
    private void Awake()
    {
        _doorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _doorAnimator.SetBool("IsActive", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _doorAnimator.SetBool("IsActive", false);
        }
    }
}
