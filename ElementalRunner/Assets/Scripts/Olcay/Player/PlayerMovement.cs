using System;
using System.Collections;
using Olcay.Animations;
using Olcay.Managers;
using UnityEngine;

namespace Olcay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private float speed = 5f;
        [SerializeField] private Transform floorPos;
        [SerializeField] private bool isGrounded;
        private float floorPosY => floorPos.position.z;

        private bool isFinish;
        [SerializeField] private bool isLevelFinish;

        private bool isGameStart;
        public static event Action gameStarting;

        private void Awake()
        {
            Players.playerCollisionWithFinish += ChangeFinishState;
            Players.playerCollisionWithLevelFinish += ChangeLevelFinishState;
            Players.levelFailed += LevelFailed;
            ScaleChanger.levelFailed += LevelFailed;
        }

        private void OnDestroy()
        {
            Players.playerCollisionWithFinish -= ChangeFinishState;
            Players.playerCollisionWithLevelFinish -= ChangeLevelFinishState;
            Players.levelFailed -= LevelFailed;
            ScaleChanger.levelFailed -= LevelFailed;
        }

        private void Update()
        {
            HandleGameStart();
            if (!isGameStart)
            {
                return;
            }

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

        void HandleGameStart()
        {
            if (Input.GetMouseButtonUp(0) && !isFinish && Extentions.IsOverUi())
            {
                GameManager.Instance.StartThisLevel();
                isGameStart = true;
                gameStarting?.Invoke();
            }
        }

        private void ForwardMovement()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        private void HandleInput()
        {
            if (Input.GetMouseButton(0) && !isFinish && !Extentions.IsOverUi())
            {
                //AnimationController.Instance.ChangeAnimationState(State.Running);
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
            else if (transform.position.y <= floorPosY)
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

        private void LevelFailed()
        {
            ChangeFinishState();
            ChangeLevelFinishState();
            AnimationController.Instance.ChangeAnimationState(State.SweepFall);
        }

        private void AnimationHandler()
        {
            if (isGrounded && !isFinish)
            {
                AnimationController.Instance.ChangeAnimationState(State.Running);
            }
            else if (!isGrounded && !isFinish)
            {
                AnimationController.Instance.ChangeAnimationState(State.Falling);
            }
        }
    }
}