using UnityEngine;

public class DeadUI : MonoBehaviour
{
    public GameObject deadScreen;

    private void Start()
    {
        deadScreen.SetActive(false);
    }

    public void ShowDeadScreen()
    {
        deadScreen.SetActive(true);
    }

    public void HideDeadScreen()
    {
        deadScreen.SetActive(false);
    }
}