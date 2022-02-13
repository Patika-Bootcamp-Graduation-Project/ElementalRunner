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

                //UIManager.Instance.FinishScore(score);
                //Won();
            } //UI Manager next level veya retry ile ilgili bir action
            Won();
        }

        public void Won()
        {
            //UIManager.Instance.FinishScore(score);
            //timescale=0 and use this function with UI Manager next level button
            UIManager.Instance.Win();
            UIManager.Instance.FinishScore(score);
            UIManager.Instance.BestScore();
            score = 0;
            //LevelManager.Instance.PlayNextLevel();
        }

        public void Failed()
        {
            score = 0;
            //timescale=0 and use this function with UI Manager retry button
            UIManager.Instance.Fail();
            //LevelManager.Instance.PlayCurrentLevel();
        }

        public void StartThisLevel()
        {
            //Time.timeScale = 1f;
            UIManager.Instance.StartGame();
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