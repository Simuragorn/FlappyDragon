using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;
    public static Player Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Death()
    {
        GameManager.Instance.End();
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
}
