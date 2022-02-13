using System;
using System.Collections;
using Cinemachine;
using Olcay.Animations;
using Olcay.Managers;
using Simla;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Olcay.Player
{
    public class Players : MonoBehaviour
    {
        [SerializeField] private GameObject boyPrefab;
        [SerializeField] private GameObject girlPrefab;

        private float timer = 0f;
        private readonly float instantiateCD = 0f;


        private GameObject girlPlayer;
        private GameObject boyPlayer;
        private bool isGirlActive;
        [SerializeField] private bool isFinish = false;
        [SerializeField] private float startScale;
        private bool isGameStart;
        public static event Action<bool> playerChanged; //Observer
        public static event Action playerCollisionWithFinish;
        public static event Action playerCollisionWithLevelFinish;
        public static event Action<int> calculateFinishScore;

        public static event Action levelFailed;
        //public static event Action<bool,Vector3> playerSetUp;

        Color girlFog = new Color(0.4666667f, 0.8f, 0.7933347f, 1f);
        Color boyFog = new Color(0.8018868f, 0.4652457f, 0.4652457f, 1f);

        private Camera camera => Extentions.Camera;

        [SerializeField] private int ballCount = 1;

        private void Awake()
        {
            RenderSettings.fog = true;

            girlPlayer = Instantiate(girlPrefab, transform.position, transform.rotation);
            girlPlayer.transform.parent = this.gameObject.transform;
            //isGirlActive = true;


            boyPlayer = Instantiate(boyPrefab, transform.position, transform.rotation);
            boyPlayer.transform.parent = this.gameObject.transform;
            //boyPlayer.SetActive(false);
            isGirlActive = Random.value < 0.5f;
            /*if (isGirlActive)
            {
                boyPlayer.SetActive(false);
                RenderSettings.fogColor = girlFog;
                camera.backgroundColor = girlFog;
            }
            else
            {
                girlPlayer.SetActive(false);
                RenderSettings.fogColor = boyFog;
                camera.backgroundColor = boyFog;
            }*/

            playerChanged?.Invoke(isGirlActive);
            ChangeFog();

            //SwapCurrentPlayer(isGirlActive);
            //startScale = gameObject.transform.localScale.x;

            PlayerMovement.gameStarting += ChangeGameStartState;
            MiniGame.LevelFinished += LevelCompleted;
        }

        private void Update()
        {
            GenerateStairs();
        }

        private void OnDestroy()
        {
            PlayerMovement.gameStarting -= ChangeGameStartState;
            MiniGame.LevelFinished -= LevelCompleted;
            StopAllCoroutines();
        }

        private void ChangeFog()
        {
            if (isGirlActive)
            {
                boyPlayer.SetActive(false);
                RenderSettings.fogColor = girlFog;
                //camera.backgroundColor = girlFog;
            }
            else
            {
                girlPlayer.SetActive(false);
                RenderSettings.fogColor = boyFog;
                //camera.backgroundColor = boyFog;
            }
        }

        #region StairsGenerateAndSetActiveFalse

        private void GenerateStairs()
        {
            if (Input.GetMouseButton(0) && isGameStart && !isFinish && !Extentions.IsOverUi())
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
                        new Vector3(0.03f, 0.03f,
                            0.03f); //every stair will decrease players scale 0.05 or we can change this value with Gamesettings
                    if (gameObject.transform.localScale.x < 1f) //fail olmasın zıplayamasın.
                    {
                        FailDetection();
                    }

                    timer -= 0.2f;
                }
            }
        }

        private IEnumerator SetActiveFalseRoutine(GameObject stair)
        {
            yield return new WaitForSeconds(2f);
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
                girlPlayer.SetActive(true);
                girlPlayer.transform.position = transform.position;
                girlPlayer.transform.parent = gameObject.transform;

                boyPlayer.transform.position = Vector3.zero;
                boyPlayer.SetActive(false);
                this.isGirlActive = true;
                playerChanged?.Invoke(this.isGirlActive);
            }

            ChangeFog();
        }

        #endregion

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Finish"))
            {
                isFinish = true;
                playerCollisionWithFinish?.Invoke();
                //var pos = transform.position;
                //collisionWithFinish?.Invoke();
                //tap işlemi yapmamız lazım.  -> bunu araştırmamız gerekiyor.
                InvokeRepeating(nameof(ThrowABallRoutine), 1f, 1f);
            }
            /*else if (other.gameObject.CompareTag("LevelFinish"))
            {
                playerCollisionWithLevelFinish?.Invoke();
                CancelInvoke(nameof(ThrowABallRoutine));
                AnimationController.Instance.ChangeAnimationState(State.Dance);
                calculateFinishScore?.Invoke();
            }*/
        }

        private void FailDetection()
        {
            isFinish = true;
            levelFailed?.Invoke();
            GameManager.Instance.Failed(); //its will be change with UI Manager.
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
                localScale -= new Vector3(0.25f, 0.25f, 0.25f);
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
                localScale -= new Vector3(0.25f, 0.25f, 0.25f);
                transform.localScale = localScale;
            }

            if (isFinish && gameObject.transform.localScale.x <= 1f || ballCount >= 4)
            {
                //win vercek dans etcek
                
                LevelCompleted();
            }
        }


        private void LevelCompleted()
        {
            CancelInvoke(nameof(ThrowABallRoutine));
            playerCollisionWithLevelFinish?.Invoke();
            AnimationController.Instance.ChangeAnimationState(State.Dance);
            calculateFinishScore?.Invoke(ballCount);
            GameManager.Instance.Won();
            ballCount = 0;
            
        }

        private void ChangeGameStartState()
        {
            isGameStart = true;
        }
    }
}