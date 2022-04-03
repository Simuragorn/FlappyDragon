using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int Score => score;
    [SerializeField] private Text scoreText;

    [SerializeField] private BackgroundScroller backgroundScroller;
    [SerializeField] private int speedIncreaserScoreModulus;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject deathPanel;
    private bool gameEnded;
    private int score;


    private void Awake()
    {
        Instance = this;
        scoreText.text = score.ToString();
    }

    public void End()
    {
        gameEnded = true;
        Pause();
        deathPanel.SetActive(true);
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

    public void Restart()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex, LoadSceneMode.Single);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene((int)ScenesEnum.Menu);
    }

    public void OpenCloseSettings()
    {
        if (gameEnded)
        {
            return;
        }
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        if (!settingsPanel.activeSelf)
        {
            Continue();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        Player.Instance.Pause();
        ObstaclesManager.Instance.Stop();
        backgroundScroller.Pause();
    }

    private void Continue()
    {
        Player.Instance.Continue();
        ObstaclesManager.Instance.Continue();
        backgroundScroller.Continue();
    }
}
