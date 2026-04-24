using UnityEngine;

public class TriggerDestroysGO : MonoBehaviour
{
    public GameObject destroyThis;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(destroyThis);
        }
    }
}

