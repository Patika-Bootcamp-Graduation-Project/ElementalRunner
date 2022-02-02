using System;
using System.Collections;
using System.Collections.Generic;
using Olcay.Player;
using UnityEngine;

namespace Simla
{
    public class MiniGame : MonoBehaviour
    {
        private int HP = 3;
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag.Equals("WaterBall") || other.gameObject.tag.Equals("FireBall"))
            {
                other.gameObject.SetActive(false);
                other.gameObject.transform.position = Vector3.zero;
                HP -= 1;
                if (HP == 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
