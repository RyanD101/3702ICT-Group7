using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [Tooltip("Drag text here")]
    public TMP_Text scoreText;

    void Start()
    {
        scoreText.text = GameManager.Instance.Score.ToString();
        GameManager.Instance.OnScoreChanged += UpdateScoreText;
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnScoreChanged -= UpdateScoreText;
    }

    void UpdateScoreText(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
}