using UnityEngine;
using UnityEngine.SceneManagement;

namespace Olcay.Managers
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        private int currentLevel;
        private int nextLevel;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (PlayerPrefs.GetInt("Level",1)+1< SceneManager.sceneCountInBuildSettings)
            {
                LoadLevel(PlayerPrefs.GetInt("Level",1)+1);
            }
            else
            {
                LoadLevel(SceneManager.sceneCountInBuildSettings-1);
            }
        }

        private void LoadLevel(int index)
        {
            currentLevel = index;
            //Camera camera=Camera.main;
            Camera camera = Extentions.Camera;
            if (camera!=null)
            {
                camera.cullingMask = 0;
            }
      
            Invoke(nameof(LoadScene),0f);
        }

        private void LoadScene()
        {
            
            //Time.timeScale = 1f;
            SceneManager.LoadScene(currentLevel);
        }

        public void PlayCurrentLevel()
        {
            LoadLevel(currentLevel);
        }

        public void PlayNextLevel()
        {
            nextLevel = currentLevel + 1;
            if (nextLevel< SceneManager.sceneCountInBuildSettings)
            {
                LoadLevel(nextLevel);
            }
            else
            {
                LoadLevel(SceneManager.sceneCountInBuildSettings-1);
            }
        }
    }

}
