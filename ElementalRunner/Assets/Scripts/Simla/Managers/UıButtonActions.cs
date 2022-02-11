using System;
using Olcay.Managers;
using UnityEngine;

public class UıButtonActions : MonoBehaviour
{
    private int level;

    private void Awake()
    {
        level = PlayerPrefs.GetInt("Level");
    }

    public void PlayCurrentLevelAgain()
    {
        LevelManager.Instance.PlayCurrentLevel();
    }

    public void PlayNextLevel()
    {
        LevelManager.Instance.PlayNextLevel();
        level++;
        GameManager.Instance.ChangeLevelTextValue(level);
        //UIManager.Instance.TextCurrentLevel();
    }

    
}
