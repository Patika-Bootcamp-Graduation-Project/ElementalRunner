using System;
using UnityEngine;

namespace Olcay.Player
{
    
    public class PlayerMovement : MonoBehaviour
    {
        private float speed = 5f;
        [SerializeField] private Transform floorPos;
        [SerializeField] private float fallTimer = 0;
        private float fallCD = 0.2f;
        [SerializeField] private bool waitBeforeFall = false;
        private float floorPosY => floorPos.position.z;

        private bool isFinish;

        private void Awake()
        {
            Players.playerCollisionWithFinish += ChangeFinishState;
        }

        private void Update()
        {
            ForwardMovement();
            HandleInput();

            if (waitBeforeFall)
            {
                Fall();
            }
        }

        private void ForwardMovement()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        private void HandleInput()
        {
            if (Input.GetMouseButton(0) && !isFinish)
            {
                transform.position += Vector3.up * Time.deltaTime * 4f;
                waitBeforeFall = false;
            }
            else
            {
                if (!waitBeforeFall && transform.position.y > floorPosY)
                {
                    fallTimer += Time.deltaTime;
                    if (fallTimer >= fallCD)
                    {
                        waitBeforeFall = true;
                        fallTimer -= 0;
                    }
                }
            }
        }

        private void Fall()
        {
            if (transform.position.y > floorPosY )
            {
                transform.position += Vector3.down * Time.deltaTime * +9.8f;
            }
            else if (transform.position.y <= floorPosY )
            {
                waitBeforeFall = false;
            }
        }

        private void ChangeFinishState()
        {
            isFinish = true;
        }
    }
}