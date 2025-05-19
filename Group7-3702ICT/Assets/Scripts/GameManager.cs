using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Score { get; private set; }
    public event Action<int> OnScoreChanged;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void AddScore(int amount)
    {
        Score += amount;
        Debug.Log($"Score is now {Score}");
        OnScoreChanged?.Invoke(Score);
    }
}