using System.Collections;
using System.Collections.Generic;
using Simla.Managers;
using UnityEngine;

namespace Olcay.Balls
{
    public class BallsMovement : MonoBehaviour
    {
        private float ballsSpeed =>SettingsManager.GameSettings.ballSpeed;

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