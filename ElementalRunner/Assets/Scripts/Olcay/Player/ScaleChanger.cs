using System;
using Olcay.Managers;
using Olcay.Player;
using Olcay.Objectives;
using UnityEngine;

public class ScaleChanger : MonoBehaviour
{
    [SerializeField] private bool isGirlActive;

    
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
                transform.localScale += new Vector3(0.2f,0.2f,0.2f);
                //score += 10;
                GameManager.Instance.ChangeScore(+10);
                break;
            case "Fire" when isGirlActive:
                transform.localScale -= new Vector3(0.2f,0.2f,0.2f);
                //score -= 10;
                GameManager.Instance.ChangeScore(-10);
                break;
            case "Fire" when !isGirlActive:
                transform.localScale += new Vector3(0.2f,0.2f,0.2f);
                //score += 10;
                GameManager.Instance.ChangeScore(+10);
                break;
            case "Water" when !isGirlActive:
                transform.localScale -= new Vector3(0.2f,0.2f,0.2f);
                //score -= 10;
                GameManager.Instance.ChangeScore(-10);
                break;
        }
        //Debug.Log(score);
    }
}
