using System.Collections;
using UnityEngine;

namespace Olcay
{
    public class StairCreator : MonoBehaviour
    {
        [SerializeField] private GameObject stairPre;

        private float timer = 0f;
        private float instantiateCD = 0f;

        private void Update()
        {
            GenerateStairs();
        }

        private void GenerateStairs()
        {
            if (Input.GetMouseButton(0))
            {
                timer += Time.deltaTime;
                
                if (timer >= instantiateCD)
                {
                    var pos = transform.position;
                    GameObject stair = ObjectPooler.Instance.SpawnFromPool("WaterStairs",new Vector3(pos.x, pos.y+0.01f, pos.z),
                        Quaternion.identity);
                    StartCoroutine(SetActiveFalseRoutine(stair));
                    //stair.SetActive(false);
                    //stair.transform.position = Vector3.zero;
                    /*GameObject stair = Instantiate(stairPre, new Vector3(pos.x, pos.y, pos.z),
                        Quaternion.identity);*/
                    
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
    }
}

