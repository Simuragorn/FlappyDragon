using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private SoundPlayer soundPlayer;
    [SerializeField] private AudioClip flapAudioClip;
    [SerializeField] private AudioClip clashAudioClip;
    [SerializeField] private AudioClip fallAudioClip;
    public static Player Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void StartDeath(DeathTypeEnum deathType)
    {
        if (GameManager.Instance.GameEnded)
        {
            return;
        }

        switch (deathType)
        {
            case DeathTypeEnum.Clash:
                soundPlayer.PlaySound(clashAudioClip);
                break;
            case DeathTypeEnum.OutOfBorder:
                soundPlayer.PlaySound(fallAudioClip);
                break;
        }

        GameManager.Instance.End();
        playerMove.enabled = false;
        playerMove.Rigidbody.constraints = RigidbodyConstraints2D.None;

        StartCoroutine(DeathInDelay(5));
    }

    public IEnumerator DeathInDelay(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    public void Pause()
    {
        playerMove.Pause();
    }

    public void Continue()
    {
        playerMove.Continue();
    }

    public void PlayFlapSound()
    {
        soundPlayer.PlaySound(flapAudioClip);
    }
}
