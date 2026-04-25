using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class UseCrucefix : MonoBehaviour
{
    public LayerMask layerMask;

    public void OnStun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!hasCrucefix())
            {
                return;
            }
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out RaycastHit hit, layerMask))
            {
                // Falta gestionar si no da al enemigo
                EnemyBehaviour enemy = hit.collider.gameObject.GetComponent<EnemyBehaviour>();
                enemy.StunEnemy();
            }
        }
    }

    private bool hasCrucefix() { 
        return InventoryController.Instance.CheckForItem("Crucefix");
    }
}
