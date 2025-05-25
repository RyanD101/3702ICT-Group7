using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [Tooltip("Drag your TextMeshProUGUI here")]
    public TMP_Text scoreText;

    int _lastScore = int.MinValue;

    void Update()
{
    int current = GameManager.Instance.Score;
    if (current != _lastScore)
    {
        _lastScore = current;


        string suffix = (current == 1) ? " point" : " points";
        scoreText.text = current + suffix;
    }
}
}