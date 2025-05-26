using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

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
    [Tooltip("Leave button")]
    public GameObject LeaveButton;
    [Header("Treasure GameObject")]
    public GameObject treasureRoot;
    [Header("ColletableHint GameObject")]
    public GameObject hintRoot;
    [Header("Treasure view button")]  
    public GameObject treasureView;
    [Tooltip("Bootprints")]
    public GameObject bootPrints;
    [Header("Phone Background Swap")]
    [Tooltip("Phone Background")]
    public Image phoneBackground;
    [Tooltip("Default texture")]
    public Sprite lockedSprite;
    [Tooltip("Unlocked texture")]
    public Sprite unlockedSprite;

    private void Awake()
    {
        phoneScreen.SetActive(false);
        menuBackground.SetActive(false);
        closeButton.SetActive(false);
        openButton.SetActive(true);
        treasureRoot.SetActive(false);
        hintRoot.SetActive(false);
        treasureView.SetActive(false);
        LeaveButton.SetActive(false);

        bool unlocked = GameManager.Instance.PhoneBgUnlocked;
        phoneBackground.sprite = unlocked ? unlockedSprite : lockedSprite;
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
        DoToggle();
    }

    public void DoToggle()
    {
        bool isOn = !phoneScreen.activeSelf;
        phoneScreen.SetActive(isOn);
        treasureRoot.SetActive(isOn);
        hintRoot.SetActive(isOn);
        menuBackground.SetActive(isOn);
        closeButton.SetActive(isOn);
        openButton.SetActive(!isOn);
        treasureView.SetActive(isOn);
        LeaveButton.SetActive(isOn);
    }

    public void CloseMenu()
    {
        menuBackground.SetActive(false);
        closeButton.SetActive(false);
        openButton.SetActive(true);
        treasureView.SetActive(false);
        LeaveButton.SetActive(false);
    }
    public void HintCloseMenu()
    {
        menuBackground.SetActive(false);
        closeButton.SetActive(false);
        openButton.SetActive(true);
        treasureView.SetActive(false);
        LeaveButton.SetActive(false);
        bootPrints.SetActive(true);
    }

    public void OpenMenu()
    {
        menuBackground.SetActive(true);
        closeButton.SetActive(true);
        openButton.SetActive(false);
        treasureView.SetActive(true);
        LeaveButton.SetActive(true);
        bootPrints.SetActive(false);
    }
    public void UnlockPhoneBackground()
    {
        GameManager.Instance.UnlockPhoneBackground();  
        phoneBackground.sprite = unlockedSprite;
    }
}