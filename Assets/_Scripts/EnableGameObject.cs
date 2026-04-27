using UnityEngine;

public class EnableGameObject : MonoBehaviour
{
    public GameObject target;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target.gameObject.SetActive(true);
        }
    }
}
