using UnityEngine;
using UnityEngine.InputSystem;

public class ClueClickPopup : MonoBehaviour
{
    public GameObject popupCanvas;
    public Transform playerCamera;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            int groundLayer = LayerMask.NameToLayer("Ground");
            int layerMask = ~(1 << groundLayer);  // All layers except ground

            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 2f);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                Debug.Log("Hit: " + hit.transform.name);

                if (hit.transform == transform)
                {
                    bool isActive = popupCanvas.activeSelf;
                    popupCanvas.SetActive(!isActive);

                    if (!isActive)
                    {
                        Vector3 popupPosition = playerCamera.position + playerCamera.forward * 2f;
                        popupPosition.y = playerCamera.position.y;  // keep popup at player eye level, no dipping down

                        popupCanvas.transform.position = popupPosition;
                        popupCanvas.transform.rotation = Quaternion.LookRotation(playerCamera.forward);
                    }

                }
            }
            else
            {
                Debug.Log("No hit on allowed layers.");
            }
        }
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
        popupCanvas.SetActive(false);
    }

}
