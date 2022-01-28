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
            LoadLevel(1);
        }

        private void LoadLevel(int index)
        {
            currentLevel = index;
            Camera camera=Camera.main;
            if (camera!=null)
            {
                camera.cullingMask = 0;
            }
      
            Invoke(nameof(LoadScene),3f);
        }

        private void LoadScene()
        {
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
                LoadLevel(1);
            }
        }
    }

}
