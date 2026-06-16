using UnityEngine;

public class ComponentObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCheckComponents playerCheck = other.GetComponent<PlayerCheckComponents>();
            playerCheck.AddComponent();
            Destroy(gameObject);
        }
    }
}
