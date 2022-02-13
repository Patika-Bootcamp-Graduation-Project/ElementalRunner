using Olcay.Managers;
using Olcay.Player;
using UnityEngine;

namespace Simla
{
    public class MiniGame : MonoBehaviour
    {

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
        }
        
    }
}
