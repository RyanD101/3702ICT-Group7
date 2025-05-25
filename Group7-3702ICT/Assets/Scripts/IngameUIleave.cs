using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LeaveGame()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}