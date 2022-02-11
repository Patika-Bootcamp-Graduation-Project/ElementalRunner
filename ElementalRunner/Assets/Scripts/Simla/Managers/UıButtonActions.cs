using Olcay.Managers;
using UnityEngine;

public class UıButtonActions : MonoBehaviour
{
    public void PlayCurrentLevelAgain()
    {
        LevelManager.Instance.PlayCurrentLevel();
    }

    public void PlayNextLevel()
    {
        LevelManager.Instance.PlayNextLevel();
    }

    
}
