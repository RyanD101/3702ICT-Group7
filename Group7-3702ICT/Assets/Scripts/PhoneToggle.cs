using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneScreenManager : MonoBehaviour
{
    [Header("Phone-Screen Toggle")]
    [Tooltip("Your quad or world-space Canvas that shows the menu")]
    public GameObject phoneScreen;
    [Tooltip("Drag your existing InputActionReference (e.g. UI Press) here")]
    public InputActionReference toggleAction;

    [Header("Menu UI Elements")]
    [Tooltip("The full-screen background Image/Panel")]
    public GameObject menuBackground;
    [Tooltip("Button inside the menu that closes it")]
    public GameObject closeButton;
    [Tooltip("Button that re-opens the menu")]
    public GameObject openButton;

    private void Awake()
    {
        // start everything hidden
        phoneScreen.SetActive(false);
        menuBackground.SetActive(false);
        closeButton.SetActive(false);
        openButton.SetActive(true);
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
        // Show or hide the entire phone-screen menu
        bool isOn = !phoneScreen.activeSelf;
        phoneScreen.SetActive(isOn);

        // when you open the menu, also show background + close button
        if (isOn)
        {
            menuBackground.SetActive(true);
            closeButton.SetActive(true);
            openButton.SetActive(false);
        }
    }

    // Called by your Close Button’s OnClick()
    public void CloseMenu()
    {
        menuBackground.SetActive(false);
        closeButton.SetActive(false);
        openButton.SetActive(true);
    }

    // Called by your Open Button’s OnClick()
    public void OpenMenu()
    {
        menuBackground.SetActive(true);
        closeButton.SetActive(true);
        openButton.SetActive(false);
    }
}