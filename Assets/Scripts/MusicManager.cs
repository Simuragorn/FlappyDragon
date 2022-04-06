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
        if (PlayerPrefs.HasKey(PrefsConstants.IS_SOUND_ON_KEY))
        {
            isSoundOn = PlayerPrefs.GetInt(PrefsConstants.IS_SOUND_ON_KEY) == 1;
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
        SceneManager.LoadScene((int)ScenesEnum.Level1, LoadSceneMode.Single);
    }

    public void TurnSound()
    {
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt(PrefsConstants.IS_SOUND_ON_KEY, isSoundOn ? 1 : 0);
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
