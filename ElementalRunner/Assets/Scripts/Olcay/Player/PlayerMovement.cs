using System;
using Olcay.Animations;
using Olcay.Managers;
using UnityEngine;

namespace Olcay.Player
{
    
    public class PlayerMovement : MonoBehaviour
    {
        private float speed = 5f;
        [SerializeField] private Transform floorPos;
        private float fallCD = 0.2f;
        [SerializeField] private bool isGrounded;
        private float floorPosY => floorPos.position.z;

        private bool isFinish;
        [SerializeField]private bool isLevelFinish;
        
        private bool isGameStart;
        

        private void Awake()
        {
            Players.playerCollisionWithFinish += ChangeFinishState;
            Players.playerCollisionWithLevelFinish += ChangeLevelFinishState;
        }

        private void OnDestroy()
        {
            Players.playerCollisionWithFinish -= ChangeFinishState;
            Players.playerCollisionWithLevelFinish -= ChangeLevelFinishState;
        }

        private void Update()
        {
            if (!isFinish)
            {
                AnimationHandler();
            }

            if (isLevelFinish)
            {
                return;
            }
            ForwardMovement();
            HandleInput();
            Fall();
        }

        private void ForwardMovement()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        private void HandleInput()
        {
            if (Input.GetMouseButton(0) && !isFinish)
            {
                if (!isGameStart)
                {
                    GameManager.Instance.StartThisLevel();
                    AnimationController.Instance.ChangeAnimationState(State.Running);
                }
                isGameStart = true;
                
                AnimationController.Instance.ChangeAnimationState(State.Running);
                transform.position += Vector3.up * Time.deltaTime * 4f;
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
            
        }

        private void Fall()
        {
            if (transform.position.y > floorPosY && !isGrounded)
            {
                transform.position += Vector3.down * Time.deltaTime * +9.8f;
            }
            else if (transform.position.y <= floorPosY )
            {
                isGrounded = true;
            }
        }

        private void ChangeFinishState()
        {
            isFinish = true;
        }
        private void ChangeLevelFinishState()
        {
            isLevelFinish = true;
        }
        
        private void AnimationHandler()
        {
            if (isGrounded)
            {
                AnimationController.Instance.ChangeAnimationState(State.Running);
            }
            else if (!isGrounded)
            {
                AnimationController.Instance.ChangeAnimationState(State.Falling);
            }
        }
    }
    
   
}