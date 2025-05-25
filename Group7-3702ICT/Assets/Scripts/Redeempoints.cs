using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TMP_Text feedbackText;
    

    public void Purchase()
    {
        int cost = 1;
        if (GameManager.Instance.Score >= cost)
        {
            GameManager.Instance.AddScore(-cost);
            feedbackText.text = "Unlocked!";

        }
        else
        {
            feedbackText.text = "Not enough points!";
        }
        Invoke(nameof(Clear), 2f);
    }

    void Clear() => feedbackText.text = "";
}