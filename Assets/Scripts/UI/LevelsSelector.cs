using Assets.Scripts.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelsSelector : MonoBehaviour
{
    public Level ActiveLevel { private set; get; }

    [SerializeField] private Image menuBackground;
    [SerializeField] private LevelDB levelDB;
    [SerializeField] private List<ScenesEnum> levelIds;
    [SerializeField] private LevelComponent levelButtonPrefab;
    [SerializeField] private Transform root;
    public List<Level> Levels { get; private set; }

    public ScenesEnum SelectedScene { private set; get; }

    public void Init()
    {
        Levels = new List<Level>();
        levelIds.ForEach(levelId =>
        {
            var level = levelDB.GetLevel(levelId);
            Levels.Add(level);
            var levelComponent = Instantiate(levelButtonPrefab, root);
            bool isLocked = MenuManager.Instance.BestScore < level.RequiredScore;
            levelComponent.Init(level, this, isLocked);
        });

        var firstLevel = Levels.OrderBy(l => l.RequiredScore).First();
        SelectLevel(firstLevel);
    }

    public void SelectLevel(Level level)
    {
        if (MenuManager.Instance.BestScore < level.RequiredScore)
        {
            return;
        }

        ActiveLevel = level;
        menuBackground.sprite = level.Background;
    }
}
