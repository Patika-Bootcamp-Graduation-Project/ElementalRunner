using UnityEngine;
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
    [SerializeField] private TextMeshProUGUI levelTxt;

    private int score;
    private void Awake()
    {
        startUI.SetActive(true);
        inGameUI.SetActive(true);
        TextCurrentLevel();
    }


    public void StartGame()
    {
        startUI.SetActive(false);
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
        this.score = score;
        inGameScoreTxt.text = this.score.ToString();
        this.score = 0;
    }

    public void FinishScore(int score)
    {
        finishScoreTxt.text = score.ToString();
    }

    public void BestScore()
    {
        bestScoreTxt.text = $"{PlayerPrefs.GetInt("HighScore",0)}";
    }

    public void TextCurrentLevel()
    {
        levelTxt.text = $"Level {PlayerPrefs.GetInt("Level",1)}";
    }
}
