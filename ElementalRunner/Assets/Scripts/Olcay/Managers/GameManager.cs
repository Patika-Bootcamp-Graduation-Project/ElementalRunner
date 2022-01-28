using UnityEngine;

namespace Olcay.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private int score = 0;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            PlayerPrefs.SetInt("HighScore", score);
        }

        public void ChangeScore(int index)
        {
            score += index;
        }

        public void CurrentScoreAtFinish(int index)
        {
            score *= index;
            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            Won(); //UI Manager next level veya retry ile ilgili bir action
        }
        public void Won()
        {
            //timescale=0 and use this function with UI Manager next level button
            LevelManager.Instance.PlayNextLevel();
        }

        public void Failed()
        {
            //timescale=0 and use this function with UI Manager retry button
            LevelManager.Instance.PlayCurrentLevel();
        }
    }
}

