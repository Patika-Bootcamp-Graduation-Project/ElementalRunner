using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject failUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private TextMeshProUGUI inGameScoreTxt;
    [SerializeField] private TextMeshProUGUI bestScoreTxt;
    [SerializeField] private TextMeshProUGUI finishScoreTxt;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

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

    public void InGameScore(int score)
    {
        inGameScoreTxt.text = score.ToString();
    }

    public void FinishScore(int score)
    {
        finishScoreTxt.text = score.ToString();
    }

    public void BestScore()
    {
        bestScoreTxt.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
