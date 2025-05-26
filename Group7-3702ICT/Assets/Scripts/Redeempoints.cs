using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    const int wallpaperCost = 2;
    public TMP_Text feedbackText;

    public void TryBuyWallpaper()
    {
        if (GameManager.Instance.Score >= wallpaperCost)
        {
            GameManager.Instance.AddScore(-wallpaperCost);
            GameManager.Instance.UnlockPhoneBackground();
            feedbackText.text = "Wallpaper Unlocked!";
        }
        else
        {
            feedbackText.text = "Not enough points!";
        }
    }

}