using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardMovementSpeed = 1.0f;
    [SerializeField] private float jumpSpeed = 0.1f;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private bool isForward = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        Jump();
    }

    private void MoveForward()
    {
        rb.isKinematic = false;
        transform.Translate(Vector3.forward * Time.deltaTime * forwardMovementSpeed);
    }

    private void Jump()
    {
        if(Input.GetMouseButton(0))
        {
            rb.isKinematic = true;
            transform.Translate(0, jumpSpeed, forwardMovementSpeed * Time.deltaTime * Time.deltaTime);
        }
    }
}
