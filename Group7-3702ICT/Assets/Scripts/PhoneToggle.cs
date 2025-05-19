using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PhoneScreenManager : MonoBehaviour
{
    [Header("Phone-Screen Toggle")]
    [Tooltip("Quad")]
    public GameObject phoneScreen;
    [Tooltip("InputActionReference")]
    public InputActionReference toggleAction;

    [Header("Menu UI Elements")]
    [Tooltip("Background")]
    public GameObject menuBackground;
    [Tooltip("Button inside the menu that closes it")]
    public GameObject closeButton;
    [Tooltip("Button that re-opens the menu")]
    public GameObject openButton;
    [Header("Treasure GameObject")]
    public GameObject treasureRoot;
    

    private void Awake()
    {
        phoneScreen.SetActive(false);
        menuBackground.SetActive(false);
        closeButton.SetActive(false);
        openButton.SetActive(true);
        treasureRoot.SetActive(false);
    }

    private void OnEnable()
    {
        toggleAction.action.performed += OnTogglePhoneScreen;
        toggleAction.action.Enable();
    }

    private void OnDisable()
    {
        toggleAction.action.performed -= OnTogglePhoneScreen;
        toggleAction.action.Disable();
    }

    private void OnTogglePhoneScreen(InputAction.CallbackContext ctx)
    {
        bool isOn = !phoneScreen.activeSelf;
        phoneScreen.SetActive(isOn);
        treasureRoot.SetActive(isOn);

        if (isOn)
        {
            menuBackground.SetActive(true);
            closeButton.SetActive(true);
            openButton.SetActive(false);
        }
    }

    public void CloseMenu()
    {
        menuBackground.SetActive(false);
        closeButton.SetActive(false);
        openButton.SetActive(true);
    }

    public void OpenMenu()
    {
        menuBackground.SetActive(true);
        closeButton.SetActive(true);
        openButton.SetActive(false);
    }
}