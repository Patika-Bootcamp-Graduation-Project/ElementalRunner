using System;
using Olcay.Managers;
using Olcay.Player;
using Olcay.Objectives;
using Simla.Managers;
using UnityEngine;

public class ScaleChanger : MonoBehaviour
{
    private float scaleChangeValue => SettingsManager.GameSettings.characterScaleChangeValueWithObjectives;
    private int scoreIncreaseValue => SettingsManager.GameSettings.increaseScoreValue;
    private int scoreDecreaseValue => SettingsManager.GameSettings.decreaseScoreValue;
    private bool isGirlActive;

    public static event Action levelFailed;

    private void Awake()
    {
        Players.playerChanged += ChangeCurrentPlayer;
        Objectives.collisionWithObjective += ChangePlayerScale;
    }

    private void OnDestroy()
    {
        Players.playerChanged -= ChangeCurrentPlayer;
        Objectives.collisionWithObjective -= ChangePlayerScale;
    }


    private void ChangeCurrentPlayer(bool whoActive)
    {
        isGirlActive = whoActive;
    }

    private void ChangePlayerScale(string tag)
    {
        switch (tag)
        {
            case "Water" when isGirlActive:
                transform.localScale += new Vector3(scaleChangeValue,scaleChangeValue,scaleChangeValue);
                GameManager.Instance.ChangeScore(scoreIncreaseValue);
                break;
            case "Fire" when isGirlActive:
                transform.localScale -= new Vector3(scaleChangeValue,scaleChangeValue,scaleChangeValue);
                GameManager.Instance.ChangeScore(scoreDecreaseValue);
                break;
            case "Fire" when !isGirlActive:
                transform.localScale += new Vector3(scaleChangeValue,scaleChangeValue,scaleChangeValue);
                GameManager.Instance.ChangeScore(scoreIncreaseValue);
                break;
            case "Water" when !isGirlActive:
                transform.localScale -= new Vector3(scaleChangeValue,scaleChangeValue,scaleChangeValue);
                GameManager.Instance.ChangeScore(scoreDecreaseValue);
                break;
        }

        if (transform.localScale.x < 1)
        {
            levelFailed?.Invoke();
            GameManager.Instance.Failed();
        }
    }
}