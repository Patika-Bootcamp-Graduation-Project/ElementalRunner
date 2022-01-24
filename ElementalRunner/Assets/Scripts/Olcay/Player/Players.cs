using System;
using System.Collections;
using UnityEngine;
using Simla;

namespace Olcay
{
    public class Players : MonoBehaviour
    {
        [SerializeField] private GameObject boyPrefab;
        [SerializeField] private GameObject girlPrefab;
        
        private float timer = 0f;
        private float instantiateCD = 0f;
        
        
        private GameObject girlPlayer;
        private GameObject boyPlayer;
        private bool isGirlActive;

        public static event Action<bool> playerChanged; //Observer

        private void Awake()
        {
            girlPlayer = Instantiate(girlPrefab, transform.position, transform.rotation);
            girlPlayer.transform.parent = this.gameObject.transform;
            isGirlActive = true;
            playerChanged?.Invoke(isGirlActive);

            boyPlayer = Instantiate(boyPrefab, Vector3.zero, Quaternion.identity);
            boyPlayer.SetActive(false);
        }
        private void Update()
        {
            GenerateStairs();
        }

        #region StairsGenerateAndSetActiveFalse
        private void GenerateStairs()
        {
            if (Input.GetMouseButton(0))
            {
                timer += Time.deltaTime;
                
                if (timer >= instantiateCD)
                {
                    var pos = transform.position;
                    if (isGirlActive)
                    {
                        GameObject stair = SpawnManager.Instance.SpawnStair("WaterStairs",new Vector3(pos.x, pos.y+0.01f, pos.z),
                            Quaternion.identity);
                        StartCoroutine(SetActiveFalseRoutine(stair));
                    }
                    else
                    {
                        GameObject stair = SpawnManager.Instance.SpawnStair("FireStairs",new Vector3(pos.x, pos.y+0.01f, pos.z),
                            Quaternion.identity);
                        StartCoroutine(SetActiveFalseRoutine(stair));
                    }

                    timer -= 0.2f;

                }
            }
        }

        private IEnumerator SetActiveFalseRoutine(GameObject stair)
        {
            yield return new WaitForSeconds(2f);
            stair.transform.position=Vector3.zero;
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
                
                    girlPlayer.transform.position=Vector3.zero;
                    girlPlayer.SetActive(false);
                    isGirlActive = false;
                    playerChanged?.Invoke(isGirlActive);
                }
                else
                {
                    girlPlayer.SetActive(true);
                    girlPlayer.transform.position = transform.position;
                    girlPlayer.transform.parent = gameObject.transform;
                
                    boyPlayer.transform.position=Vector3.zero;
                    boyPlayer.SetActive(false);
                    isGirlActive = true;
                    playerChanged?.Invoke(isGirlActive);
                }
                
            }
        }
        #endregion
    }

}
