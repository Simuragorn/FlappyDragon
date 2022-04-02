using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int Score => score;

    [SerializeField] private BackgroundScroller backgroundScroller;
    private int score;

    private void Awake()
    {
        Instance = this;
    }

    public void End()
    {
        ObstaclesManager.Instance.Stop();
        backgroundScroller.Pause();
    }

    public void AddScore(int bonusScore)
    {
        score += bonusScore;
        Debug.Log($"Score {score}");
    }
}
