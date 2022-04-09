using Assets.Scripts.Constants;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { private set; get; }
    private bool isSoundOn = true;

    private void Awake()
    {
        Instance = this;
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
