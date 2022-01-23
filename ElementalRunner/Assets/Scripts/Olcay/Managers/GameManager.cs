using System;
using UnityEngine;

namespace Olcay
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

