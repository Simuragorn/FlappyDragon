using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }
    public int Score => score;
    public bool GameEnded => gameEnded;
    [SerializeField] private Text scoreText;

    [SerializeField] private BackgroundScroller backgroundScroller;
    [SerializeField] private int speedIncreaserScoreModulus;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject deathPanel;

    [SerializeField] private AudioClip clashBonusScoreSound;
    [SerializeField] private AudioClip distanceBonusScoreSound;
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
        ObstaclesManager.Instance.Stop();
        backgroundScroller.Pause();
        deathPanel.SetActive(true);

        SaveScore();
    }

    public void AddScore(int bonusScore, BonusScoreTypeEnum bonusScoreType)
    {
        score += bonusScore;
        scoreText.text = score.ToString();

        if (score % speedIncreaserScoreModulus == 0)
        {
            ObstaclesManager.Instance.IncreaseSpeed();
            if (bonusScoreType == BonusScoreTypeEnum.Distance)
            {
                SoundManager.Instance.PlaySound(distanceBonusScoreSound);
            }
        }
        if (bonusScoreType == BonusScoreTypeEnum.Clash)
        {
            SoundManager.Instance.PlaySound(clashBonusScoreSound);
        }

        if (gameEnded)
        {
            SaveScore();
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

    private void SaveScore()
    {
        int bestScore = score;
        if (PlayerPrefs.HasKey(PrefsConstants.SCORE_KEY))
        {
            int previousScore = PlayerPrefs.GetInt(PrefsConstants.SCORE_KEY);
            bestScore = Mathf.Max(score, previousScore);
        }
        PlayerPrefs.SetInt(PrefsConstants.SCORE_KEY, bestScore);
        PlayerPrefs.Save();
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
