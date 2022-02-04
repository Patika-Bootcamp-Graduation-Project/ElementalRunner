using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UıManager : MonoSingleton<UıManager>
{
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject failUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private Text scoreTxt;
    [SerializeField] private Text bestScoreTxt;

    public void StartGame()
    {
        startUI.SetActive(false);
        inGameUI.SetActive(true);
    }

    public void Fail()
    {
        inGameUI.SetActive(false);
        failUI.SetActive(true);
    }

    public void Win()
    {
        inGameUI.SetActive(false);
        winUI.SetActive(true);
    }

    public void Score(int score)
    {
        scoreTxt.text = score.ToString();
    }

    public void BestScore()
    {
        bestScoreTxt.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
