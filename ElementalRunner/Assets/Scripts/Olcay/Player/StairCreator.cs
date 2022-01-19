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
                    GameObject stair = Instantiate(stairPre, new Vector3(pos.x, pos.y, pos.z),
                        Quaternion.identity);
                    Destroy(stair, 2f);
                    timer -= 0.2f;
                }
            }
        }
    }
}

