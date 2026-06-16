using UnityEngine;

public class ThrowStone : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip stoneFalling;

    private void OnTriggerEnter(Collider other)
    {
        audioSource.PlayOneShot(stoneFalling, 0.8f);
    }


}
