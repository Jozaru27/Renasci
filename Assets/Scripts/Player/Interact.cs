using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    [SerializeField] LayerMask interactLayer;
    [SerializeField] float interactionDistance;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started && !GameManager.Instance.gameOver && !GameManager.Instance.gameWin)
        {
            if (GameManager.Instance.inInfo)
            {
                if (GameManager.Instance.infoShowed)
                    InfoPanel.Instance.CheckTextsList();
            }
            else
            {
                Ray cameraRay = new Ray(transform.position, transform.forward);

                if (Physics.Raycast(cameraRay, out RaycastHit hit, interactionDistance, interactLayer))
                {
                    if (hit.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                    {
                        GameManager.Instance.playerCannotMove = true;
                        GetComponent<PlayerAnimation>().Interact();
                        interactObj.Interact();
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * interactionDistance);
    }
}
