using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private SoundPlayer soundPlayer;
    [SerializeField] private AudioClip flapAudioClip;
    [SerializeField] private AudioClip deathAudioClip;
    [SerializeField] private Animator animator;
    public static Player Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void StartDeath()
    {
        soundPlayer.PlaySound(deathAudioClip);
        GameManager.Instance.End();
        playerMove.enabled = false;

        animator.SetTrigger(TriggerConstants.StartDeathTrigger);
    }

    public void Death()
    {
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
