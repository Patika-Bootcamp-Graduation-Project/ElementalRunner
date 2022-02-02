using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Olcay.Managers;
using Simla;
using UnityEngine;

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

        public static event Action<bool> playerChanged; //Observer
        public static event Action playerCollisionWithFinish;

        public static event Action CalculateFinishScore;
        //public static event Action<bool,Vector3> playerSetUp;

        private void Awake()
        {
            girlPlayer = Instantiate(girlPrefab, transform.position, transform.rotation);
            girlPlayer.transform.parent = this.gameObject.transform;
            isGirlActive = true;
            playerChanged?.Invoke(isGirlActive);

            boyPlayer = Instantiate(boyPrefab, Vector3.zero, Quaternion.identity);
            boyPlayer.SetActive(false);

            //startScale = gameObject.transform.localScale.x;
        }

        private void Update()
        {
            GenerateStairs();
        }


        #region StairsGenerateAndSetActiveFalse

        private void GenerateStairs()
        {
            if (Input.GetMouseButton(0) && !isFinish)
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
                        new Vector3(0.05f, 0.05f,
                            0.05f); //every stair will decrease players scale 0.05 or we can change this value with Gamesettings
                    if (gameObject.transform.localScale.x < 1f) //fail olmasın zıplayamasın.
                    {
                        GameManager.Instance.Failed(); //its will be change with UI Manager.
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
                if (isGirlActive)
                {
                    boyPlayer.SetActive(true);
                    boyPlayer.transform.position = transform.position;
                    boyPlayer.transform.parent = gameObject.transform;

                    girlPlayer.transform.position = Vector3.zero;
                    girlPlayer.SetActive(false);
                    isGirlActive = false;
                    playerChanged?.Invoke(isGirlActive);
                }
                else
                {
                    girlPlayer.SetActive(true);
                    girlPlayer.transform.position = transform.position;
                    girlPlayer.transform.parent = gameObject.transform;

                    boyPlayer.transform.position = Vector3.zero;
                    boyPlayer.SetActive(false);
                    isGirlActive = true;
                    playerChanged?.Invoke(isGirlActive);
                }
            }
            
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
                
                InvokeRepeating(nameof(ThrowABallRoutine),1f,1f);
            }
            else if (other.gameObject.CompareTag("LevelFinish"))
            {
                CalculateFinishScore?.Invoke();
            }
        }

        private void ThrowABallRoutine()
        {
                if (isGirlActive)
                {
                    var pos = transform.position;
                    var posY = transform.localScale.y / 2f;
                    SpawnManager.Instance.SpawnBall("WaterBalls",
                        new Vector3(pos.x, posY, pos.z + 0.1f),
                        Quaternion.identity);
                    transform.localScale -= new Vector3(1f,1f,1f);
                }
                else
                {
                    var pos = transform.position;
                    var posY = transform.localScale.y / 2f;
                    SpawnManager.Instance.SpawnBall("FireBalls",
                        new Vector3(pos.x, posY, pos.z + 0.1f),
                        Quaternion.identity);
                    transform.localScale -= new Vector3(1f,1f,1f);
                }

                if (gameObject.transform.localScale.x <= 1f && isFinish)
                {
                    CancelInvoke();
                    //game finish
                    // //o anki basamağın üstündeki colliderdan alırız x kaç olduğunu
                }
        }
    }
}