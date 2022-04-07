using Assets.Scripts.Constants;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { private set; get; }
    public int BestScore => bestScore;

    [SerializeField] private Text scoreText;
    [SerializeField] private LevelsSelector levelsSelector;
    [SerializeField] private Text scoreHintText;
    private int bestScore;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        LoadScore();
        levelsSelector.Init();
        SetScoreHint();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R))
        {
            ClearAndReload();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnlockAllAndReload();
        }
#endif
    }
    public void Play()
    {
        var activeLevel = levelsSelector.ActiveLevel;
        SceneManager.LoadScene((int)activeLevel.SceneId, LoadSceneMode.Single);
    }
    public void Exit()
    {
        Application.Quit();
    }

    private void LoadScore()
    {
        if (PlayerPrefs.HasKey(PrefsConstants.SCORE_KEY))
        {
            bestScore = PlayerPrefs.GetInt(PrefsConstants.SCORE_KEY);
        }
        scoreText.text = bestScore.ToString();
    }

    private void SetScoreHint()
    {
        Level lockedLevel = levelsSelector.Levels.FirstOrDefault(l => l.RequiredScore > bestScore);

        if (lockedLevel != null)
        {
            scoreHintText.gameObject.SetActive(true);
            scoreHintText.text = $"Get {lockedLevel.RequiredScore} score for unlock new level";
        }
        else
        {
            scoreHintText.gameObject.SetActive(false);
        }
    }
    #region Testing
    private void ClearAndReload()
    {
        PlayerPrefs.DeleteAll();
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
    }

    private void UnlockAllAndReload()
    {
        PlayerPrefs.SetInt(PrefsConstants.SCORE_KEY, 100);
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
    }
    #endregion
}
