using System;
using Olcay.Animations;
using Olcay.Managers;
using Simla.Managers;
using UnityEngine;

namespace Olcay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private float speed => SettingsManager.GameSettings.characterSpeed;
        private float characterUpValue => SettingsManager.GameSettings.characterUpValue;
        private float gravity => SettingsManager.GameSettings.gravity;
        [SerializeField] private Transform floorPos;
        private bool isGrounded;
        private float floorPosY => floorPos.position.z;

        private bool isGameStart;
        private bool isMiniGameStart;
        private bool isLevelFinish;


        public static event Action gameStarting;

        private void Awake()
        {
            Players.playerCollisionWithMiniGame += MiniGameStarted;
            Players.levelFinished += ChangeLevelFinishState;
            Players.levelFailed += LevelFailed;
            ScaleChanger.levelFailed += LevelFailed;
        }

        private void OnDestroy()
        {
            Players.playerCollisionWithMiniGame -= MiniGameStarted;
            Players.levelFinished -= ChangeLevelFinishState;
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

            if (!isMiniGameStart)
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
            if (Input.GetMouseButtonUp(0) && Extentions.IsOverUi() && !isGameStart)
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
            if (Input.GetMouseButton(0) && !isMiniGameStart && !Extentions.IsOverUi())
            {
                transform.position += Vector3.up * Time.deltaTime * characterUpValue;
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
                transform.position += Vector3.down * Time.deltaTime * gravity;
            }
            else if (transform.position.y <= floorPosY)
            {
                isGrounded = true;
            }
        }

        private void MiniGameStarted()
        {
            isMiniGameStart = true;
        }

        private void ChangeLevelFinishState()
        {
            isLevelFinish = true;
        }

        private void LevelFailed()
        {
            MiniGameStarted();
            ChangeLevelFinishState();
            AnimationController.Instance.ChangeAnimationState(State.SweepFall);
        }

        private void AnimationHandler()
        {
            if (isGrounded && !isMiniGameStart)
            {
                AnimationController.Instance.ChangeAnimationState(State.Running);
            }
            else if (!isGrounded && !isMiniGameStart)
            {
                AnimationController.Instance.ChangeAnimationState(State.Falling);
            }
        }
    }
}