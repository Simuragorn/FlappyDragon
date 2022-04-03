using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private bool isSoundOn = true;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(PrefsConstants.isSoundOnKey))
        {
            isSoundOn = PlayerPrefs.GetInt(PrefsConstants.isSoundOnKey) == 1;
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
