using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class ClueClickPopup : UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable
{
    public GameObject popupCanvas;
    public FootprintPath2 footprintPath;  // Assign in inspector or dynamically
    public Transform playerCamera;
    public GameObject questionMark;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        bool isActive = popupCanvas.activeSelf;
        popupCanvas.SetActive(!isActive);

        if (!isActive)
        {
            Vector3 popupPosition = playerCamera.position + playerCamera.forward * 2f;
            popupPosition.y = playerCamera.position.y;

            popupCanvas.transform.position = popupPosition;
            popupCanvas.transform.rotation = Quaternion.LookRotation(playerCamera.forward);
        }
    }

    public void ClosePopup()
    {
        popupCanvas.SetActive(false);

        // Destroy footprints spawned by this footprint path
        if (footprintPath != null)
        {
            footprintPath.ClearExistingFootprints();
        }
        else
        {
            Debug.LogWarning("FootprintPath not assigned!");
        }

        if (questionMark != null)
        {
            Destroy(questionMark);
        }
    }
}
