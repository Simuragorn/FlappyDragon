using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private bool isSoundOn = true;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private SwitchButton turnMusicButton;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(PrefsConstants.isSoundOnKey))
        {
            isSoundOn = PlayerPrefs.GetInt(PrefsConstants.isSoundOnKey) == 1;
        }
        musicSource.playOnAwake = isSoundOn;
        if (!isSoundOn)
        {
            musicSource.Stop();
        }
        if (turnMusicButton != null)
        {
            turnMusicButton.SetState(isSoundOn);
        }
    }
    public void PlayMusic()
    {
        SceneManager.LoadScene((int)ScenesEnum.Level, LoadSceneMode.Single);
    }

    public void TurnSound()
    {
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt(PrefsConstants.isSoundOnKey, isSoundOn ? 1 : 0);
        musicSource.playOnAwake = isSoundOn;
        if (!isSoundOn)
        {
            musicSource.Stop();
        }
        else
        {
            musicSource.Play();
        }
    }
}
