using System;
using System.Collections;
using System.Collections.Generic;
using Olcay.Managers;
using Olcay.Player;
using UnityEngine;

namespace Simla
{
    public class MiniGame : MonoBehaviour
    {
        private int HP = 3;
        private int ballCount = 0;

        private void Awake()
        {
            Players.CalculateFinishScore += GameFinishScore;
        }

        private void OnDestroy()
        {
            Players.CalculateFinishScore -= GameFinishScore;
        }
        

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag.Equals("WaterBall") || other.gameObject.tag.Equals("FireBall"))
            {
                other.gameObject.SetActive(false);
                other.gameObject.transform.position = Vector3.zero;
                HP -= 1;
                ballCount++;
                if (HP == 0)
                {
                    gameObject.SetActive(false);
                }
            }
            
        }

        private void GameFinishScore()
        {
            GameManager.Instance.CurrentScoreAtFinish(ballCount);
        }
        
    }
}
