using System;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Olcay
{
    public class Players : MonoBehaviour
    {
        [SerializeField] private GameObject boyPrefab;
        [SerializeField] private GameObject girlPrefab;
        private GameObject girlPlayer;
        private GameObject boyPlayer;
        private bool isGirlActive;

        private void Awake()
        {
            girlPlayer = Instantiate(girlPrefab, transform.position, transform.rotation);
            girlPlayer.transform.parent = this.gameObject.transform;
            isGirlActive = true;

            boyPlayer = Instantiate(boyPrefab, Vector3.zero, Quaternion.identity);
            boyPlayer.SetActive(false);
        }

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
                }
                else
                {
                    girlPlayer.SetActive(true);
                    girlPlayer.transform.position = transform.position;
                    girlPlayer.transform.parent = gameObject.transform;
                
                    boyPlayer.transform.position=Vector3.zero;
                    boyPlayer.SetActive(false);
                    isGirlActive = true;
                }
                
            }
        }

    }

}
