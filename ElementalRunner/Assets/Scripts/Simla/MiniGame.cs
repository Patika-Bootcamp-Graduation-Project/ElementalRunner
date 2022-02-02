using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simla
{
    public class MiniGame : MonoBehaviour
    {
        private int HP = 3;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag.Equals("WaterBall") || collision.gameObject.tag.Equals("FireBall"))
            {
                HP -= 1;
                if (HP == 0)
                {
                  gameObject.SetActive(false);
                }
            }
        }
    }
}
