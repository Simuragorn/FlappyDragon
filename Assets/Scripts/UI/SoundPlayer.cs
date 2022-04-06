using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private bool isSoundOn = true;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(PrefsConstants.IS_SOUND_ON_KEY))
        {
            isSoundOn = PlayerPrefs.GetInt(PrefsConstants.IS_SOUND_ON_KEY) == 1;
        }
    }

    public void PlaySound(AudioClip soundClip)
    {
        if (isSoundOn)
        {
            AudioSource.PlayClipAtPoint(soundClip, Camera.main.transform.position);
        }
    }
}
