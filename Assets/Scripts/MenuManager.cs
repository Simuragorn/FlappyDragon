using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene((int)ScenesEnum.Level, LoadSceneMode.Single);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
