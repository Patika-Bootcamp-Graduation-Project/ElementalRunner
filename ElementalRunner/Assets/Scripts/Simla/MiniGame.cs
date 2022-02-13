using System;
using System.Collections;
using System.Collections.Generic;
using Olcay.Animations;
using Olcay.Managers;
using Olcay.Player;
using UnityEngine;

namespace Simla
{
    public class MiniGame : MonoBehaviour
    {
       // private int HP = 3;
        //private int ballCount = 1;
        public static event Action LevelFinished;

        private void Awake()
        {
            Players.calculateFinishScore += GameFinishScore;
        }

        private void OnDestroy()
        {
            Players.calculateFinishScore -= GameFinishScore;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("WaterBall") || other.gameObject.tag.Equals("FireBall"))
            {
                other.gameObject.transform.position = Vector3.zero;
                other.gameObject.SetActive(false);
            }
        }

        private void GameFinishScore(int ballCount)
        {
            GameManager.Instance.CurrentScoreAtFinish(ballCount);
            LevelFinished?.Invoke();
        }
        
    }
}
