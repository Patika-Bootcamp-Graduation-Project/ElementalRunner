using UnityEngine;


namespace Olcay
{
    public class PlayerMovement : MonoBehaviour
    {
        private float speed = 5f;

        private void Update()
        {
            ForwardMovement();
        }

        private void ForwardMovement()
        {
            transform.position += Vector3.forward * Time.deltaTime * speed;
        }
    }
}