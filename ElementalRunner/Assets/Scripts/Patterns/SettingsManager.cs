using UnityEngine;

namespace Simla.Managers
{
    public class SettingsManager : MonoSingleton<SettingsManager>
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        [SerializeField] private GameSettings settings;
        public static GameSettings GameSettings => Instance.settings;
    }
}