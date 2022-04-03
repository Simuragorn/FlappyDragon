using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int Score => score;
    [SerializeField] private Text scoreText;

    [SerializeField] private BackgroundScroller backgroundScroller;
    [SerializeField] private int speedIncreaserScoreModulus;
    private int score;


    private void Awake()
    {
        Instance = this;
        scoreText.text = score.ToString();
    }

    public void End()
    {
        ObstaclesManager.Instance.Stop();
        backgroundScroller.Pause();
    }

    public void AddScore(int bonusScore)
     {
        score += bonusScore;
        scoreText.text = score.ToString();

        if (score % speedIncreaserScoreModulus == 0)
         {
            ObstaclesManager.Instance.IncreaseSpeed();
        }
    }
}
