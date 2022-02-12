using Olcay.Player;
using UnityEngine;

namespace Olcay.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private int score = 0;
        private int tempScore = 0;
        [SerializeField] private int level = 1;
        private string levelValue;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            PlayerPrefs.SetInt("HighScore", score);
            if (PlayerPrefs.GetInt("Level") < level)
            {
                PlayerPrefs.SetInt("Level", level);
            }
            else
            {
                level = PlayerPrefs.GetInt("Level");
            }
        }

        public void ChangeScore(int index)
        {
            score += index;
            UIManager.Instance.InGameScore(score);
        }

        public void CurrentScoreAtFinish(int index)
        {
            tempScore = score;
            score *= index;

            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", score);
            } //UI Manager next level veya retry ile ilgili bir action

            Won();
        }

        public void Won()
        {
            //timescale=0 and use this function with UI Manager next level button
            UIManager.Instance.Win();
            UIManager.Instance.BestScore();
            UIManager.Instance.FinishScore(tempScore);
            tempScore = 0;
            score = 0;
            //LevelManager.Instance.PlayNextLevel();
        }

        public void Failed()
        {
            //timescale=0 and use this function with UI Manager retry button
            UIManager.Instance.Fail();
            tempScore = 0;
            score = 0;
            //LevelManager.Instance.PlayCurrentLevel();
        }

        public void StartThisLevel()
        {
            //Time.timeScale = 1f;
            UIManager.Instance.StartGame();
            tempScore = 0;
            score = 0;
        }

        public void ChangeLevelTextValue(int level)
        {
            PlayerPrefs.SetInt("Level", level);
            this.level=level;
            UIManager.Instance.TextCurrentLevel();
            //levelValue = ;
        }
    }
}