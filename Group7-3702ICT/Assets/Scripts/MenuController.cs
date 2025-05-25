using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject levelSelectPanel;
    public GameObject tutorialPanel;
    public GameObject rewardsPanel;

    public void ShowLevelSelect()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    public void ShowTutorial()
    {
        mainMenuPanel.SetActive(false);
        tutorialPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        rewardsPanel.SetActive(false);
    }
    public void ShowRewards()
    {
        mainMenuPanel.SetActive(false);
        rewardsPanel.SetActive(true);
    }
}
