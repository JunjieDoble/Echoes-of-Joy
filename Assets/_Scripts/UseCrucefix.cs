using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class UseCrucefix : MonoBehaviour
{
    public TextMeshProUGUI usesLeftUI;
    public LayerMask layerMask;
    public int usesLeft = 0;

    public void Awake()
    {
        usesLeftUI.enabled = false;
    }

    public void OnStun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!hasCrucefix() || usesLeft <= 0)
            {
                return;
            }
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out RaycastHit hit, layerMask))
            {
                EnemyBehaviour enemy = hit.collider.gameObject.GetComponent<EnemyBehaviour>();
                enemy.StunEnemy();
                useCrucefix();
            }
        }
    }

    private bool hasCrucefix() { 
        return InventoryController.Instance.CheckForItem("Crucefix");
    }

    public void useCrucefix()
    {
        if (usesLeft > 0)
        {
            usesLeft--;
            UpdateUsesUI();
            if (usesLeft == 0)
            {
                InventoryController.Instance.RemoveItemFromInventory("Crucefix");
                usesLeftUI.enabled = false;
            }
        }
    }

    public void addCrucefix()
    {
        usesLeft+=3;
        usesLeftUI.enabled = true;
        UpdateUsesUI();
    }

    private void UpdateUsesUI()
    {
        usesLeftUI.text = "Crucefix Uses Left: " + usesLeft;
    }

}
