using Olcay.Player;
using UnityEngine;

namespace Olcay.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private int score = 0;
        [SerializeField] private int level = 1;
        private string levelValue;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
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
            score *= index;
            
            if (score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            Won();
        }

        public void Won()
        {
            UIManager.Instance.Win();
            UIManager.Instance.FinishScore(score);
            UIManager.Instance.BestScore();
            score = 0;
        }

        public void Failed()
        {
            score = 0;
            UIManager.Instance.Fail();
        }

        public void StartThisLevel()
        {
            UIManager.Instance.StartGame();
            score = 0;
        }

        public void ChangeLevelTextValue(int level)
        {
            PlayerPrefs.SetInt("Level", level);
            this.level=level;
            UIManager.Instance.TextCurrentLevel();
        }
    }
}