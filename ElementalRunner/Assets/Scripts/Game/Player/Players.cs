using System;
using System.Collections;
using Olcay.Animations;
using Olcay.Managers;
using Simla;
using Simla.Managers;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Olcay.Player
{
    public class Players : MonoBehaviour
    {
        [SerializeField] private GameObject boyPrefab;
        [SerializeField] private GameObject girlPrefab;
        private GameObject girlPlayer;
        private GameObject boyPlayer;


        private float instantiateCD => SettingsManager.GameSettings.stairInstantiateCD;
        private float stairSetActiveFalseCD => SettingsManager.GameSettings.stairSetActiveFalseCD;

        private float scaleDecreaseWithStair =>
            SettingsManager.GameSettings.characterLocalScaleDecreaseWithStair;

        private float scaleDecreaseWithBall => SettingsManager.GameSettings.characterLocalScaleDecreaseWithBall;
        private float characterDeadValue => SettingsManager.GameSettings.characterDeadControlValue;

        private bool isGirlActive;
        private bool isGameStart;
        private bool isMiniGameStart;

        public static event Action<bool> playerChanged;
        public static event Action playerCollisionWithMiniGame;
        public static event Action levelFinished;
        public static event Action<int> calculateFinishScore;
        public static event Action levelFailed;


        private Camera camera => Extentions.Camera;
        private int ballCount = 1;

        private float timer = 0f;

        private Color girlFogColor => SettingsManager.GameSettings.girlFogColor;
        private Color boyFogColor => SettingsManager.GameSettings.boyFogColor;

        private void Awake()
        {
            var position = transform.position;
            girlPlayer = Instantiate(girlPrefab, position, Quaternion.identity);
            girlPlayer.transform.parent = gameObject.transform;

            boyPlayer = Instantiate(boyPrefab, position, Quaternion.identity);
            boyPlayer.transform.parent = gameObject.transform;

            isGirlActive = Random.value < 0.5f; //linq lambda
            playerChanged?.Invoke(isGirlActive);
            SwapCurrentPlayer(isGirlActive);

            PlayerMovement.gameStarting += ChangeGameStartState;
        }

        private void Update()
        {
            GenerateStairs();
        }

        private void OnDestroy()
        {
            PlayerMovement.gameStarting -= ChangeGameStartState;
            StopAllCoroutines();
        }

        #region StairsGenerateAndSetActiveFalse

        private void GenerateStairs()
        {
            if (Input.GetMouseButton(0) && isGameStart && !isMiniGameStart && !Extentions.IsOverUi())
            {
                timer += Time.deltaTime;

                if (timer >= instantiateCD)
                {
                    var pos = transform.position;
                    if (isGirlActive)
                    {
                        GameObject stair = SpawnManager.Instance.SpawnStair("WaterStairs",
                            new Vector3(pos.x, pos.y + 0.01f, pos.z),
                            Quaternion.identity);
                        StartCoroutine(SetActiveFalseRoutine(stair));
                    }
                    else if (!isGirlActive)
                    {
                        GameObject stair = SpawnManager.Instance.SpawnStair("FireStairs",
                            new Vector3(pos.x, pos.y + 0.01f, pos.z),
                            Quaternion.identity);
                        StartCoroutine(SetActiveFalseRoutine(stair));
                    }

                    transform.localScale -=
                        new Vector3(scaleDecreaseWithStair, scaleDecreaseWithStair,
                            scaleDecreaseWithStair);
                    if (gameObject.transform.localScale.x < characterDeadValue)
                    {
                        FailDetection();
                    }

                    timer -= 0.2f;
                }
            }
        }

        private IEnumerator SetActiveFalseRoutine(GameObject stair)
        {
            yield return Extentions.GetWait(stairSetActiveFalseCD);
            stair.transform.position = Vector3.zero;
            stair.SetActive(false);
        }

        #endregion

        #region PlayerSwapWithGateInTrigger

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Gate"))
            {
                SwapCurrentPlayer(isGirlActive);
            }
        }

        private void SwapCurrentPlayer(bool isGirlActive)
        {
            if (isGirlActive)
            {
                RenderSettings.fogColor = boyFogColor;

                boyPlayer.SetActive(true);
                boyPlayer.transform.position = transform.position;
                boyPlayer.transform.parent = gameObject.transform;

                girlPlayer.transform.position = Vector3.zero;
                girlPlayer.SetActive(false);
                this.isGirlActive = false;
                playerChanged?.Invoke(this.isGirlActive);
            }
            else
            {
                RenderSettings.fogColor = girlFogColor;

                girlPlayer.SetActive(true);
                girlPlayer.transform.position = transform.position;
                girlPlayer.transform.parent = gameObject.transform;

                boyPlayer.transform.position = Vector3.zero;
                boyPlayer.SetActive(false);
                this.isGirlActive = true;
                playerChanged?.Invoke(this.isGirlActive);
            }
        }

        #endregion

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("MiniGame"))
            {
                isMiniGameStart = true;
                playerCollisionWithMiniGame?.Invoke();
                InvokeRepeating(nameof(ThrowABallRoutine), 0.2f, 1f);
            }
        }

        private void FailDetection()
        {
            isMiniGameStart = true;
            levelFailed?.Invoke();
            GameManager.Instance.Failed();
        }

        private void ThrowABallRoutine()
        {
            ballCount++;
            if (isGirlActive)
            {
                AnimationController.Instance.ChangeAnimationState(State.Throw);
                var pos = transform.position;
                var localScale = transform.localScale;
                var posY = localScale.y / 2f;
                SpawnManager.Instance.SpawnBall("WaterBalls",
                    new Vector3(pos.x, posY, pos.z + 0.1f),
                    Quaternion.identity);
                localScale -= new Vector3(scaleDecreaseWithBall, scaleDecreaseWithBall, scaleDecreaseWithBall);
                transform.localScale = localScale;
            }
            else
            {
                AnimationController.Instance.ChangeAnimationState(State.Throw);
                var pos = transform.position;
                var localScale = transform.localScale;
                var posY = localScale.y / 2f;
                SpawnManager.Instance.SpawnBall("FireBalls",
                    new Vector3(pos.x, posY, pos.z + 0.1f),
                    Quaternion.identity);
                localScale -= new Vector3(scaleDecreaseWithBall, scaleDecreaseWithBall, scaleDecreaseWithBall);
                transform.localScale = localScale;
            }

            if (isMiniGameStart && gameObject.transform.localScale.x <= characterDeadValue || ballCount >= 4)
            {
                CancelInvoke(nameof(ThrowABallRoutine));
                LevelCompleted();
            }
        }


        private void LevelCompleted()
        {
            levelFinished?.Invoke();
            AnimationController.Instance.ChangeAnimationState(State.Dance);
            calculateFinishScore?.Invoke(ballCount);
            ballCount = 0;
        }

        private void ChangeGameStartState()
        {
            isGameStart = true;
        }
    }
}