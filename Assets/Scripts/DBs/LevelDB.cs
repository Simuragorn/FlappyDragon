using Assets.Scripts.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Databases/Levels")]
public class LevelDB : ScriptableObject
{
    [SerializeField, HideInInspector] List<Level> levels;
    [SerializeField] private Level currentLevel;
    private int currentIndex;

    public void CreateLevel()
    {
        if (levels == null)
        {
            levels = new List<Level>();
        }
        var level = new Level();
        levels.Add(level);
        currentLevel = level;
        currentIndex = levels.Count - 1;
    }

    public void RemoveLevel()
    {
        if (levels == null || currentLevel == null)
        {
            return;
        }
        levels.Remove(currentLevel);
        if (levels.Count == 0)
        {
            CreateLevel();
        }
        else
        {
            currentLevel = levels[0];
        }
    }

    public void NextLevel()
    {
        if (currentIndex + 1 < levels.Count)
        {
            currentIndex++;
            currentLevel = levels[currentIndex];
        }
    }

    public void PrevLevel()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            currentLevel = levels[currentIndex];
        }
    }

    public Level GetLevel(ScenesEnum sceneId)
    {
        return levels.Find(l => l.SceneId == sceneId);
    }
}

[Serializable]
public class Level
{
    [SerializeField] private string name;
    public string Name => name;

    [SerializeField] Sprite menuBackground;
    public Sprite Background => menuBackground;

    [SerializeField] int requiredScore;
    public int RequiredScore => requiredScore;
    [SerializeField] private ScenesEnum sceneId;
    public ScenesEnum SceneId => sceneId;
}
