using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Score { get; private set; }
    public bool PhoneBgUnlocked { get; private set; } = false;
    void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        
        Score = 0;
    }

    public void AddScore(int delta)
    {
        Score += delta;
    }
    public void UnlockPhoneBackground()
    {
        PhoneBgUnlocked = true;
    }
}