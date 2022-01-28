namespace Olcay.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Won()
        {
            LevelManager.Instance.PlayNextLevel();
        }

        private void Failed()
        {
            LevelManager.Instance.PlayCurrentLevel();
        }
    }
}

