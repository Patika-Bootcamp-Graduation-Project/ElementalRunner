using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Olcay.Balls
{
    public class BallsMovement : MonoBehaviour
    {
        private float ballsSpeed = 10f;

        private void Update()
        {
            BallsForwardMovement();
        }

        private void BallsForwardMovement()
        {
            transform.position += Vector3.forward * ballsSpeed * Time.deltaTime;
        }
    }
}