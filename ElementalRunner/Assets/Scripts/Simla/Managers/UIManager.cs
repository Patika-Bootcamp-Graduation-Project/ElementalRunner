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

    private void Awake()
    {
        startUI.SetActive(true);
        inGameUI.SetActive(true);
        TextCurrentLevel();
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        startUI.SetActive(false);
        //inGameUI.SetActive(true);
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

    public void TextCurrentLevel()
    {
        levelTxt.text = $"Level {PlayerPrefs.GetInt("Level")}";
    }
}
