using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelComponent : MonoBehaviour
{
    private Level level;
    [SerializeField] private Image levelImage;
    [SerializeField] private Sprite disabledImage;
    private LevelsSelector levelsSelector;

    public void Init(Level levelData, LevelsSelector levelsSelectorComponent, bool isLocked)
    {
        level = levelData;
        levelsSelector = levelsSelectorComponent;

        if (isLocked)
        {
            levelImage.sprite = disabledImage;
        }
        else
        {
            levelImage.sprite = level.Background;
        }
    }

    public void SelectLevel()
    {
        levelsSelector.SelectLevel(level);
    }
}
