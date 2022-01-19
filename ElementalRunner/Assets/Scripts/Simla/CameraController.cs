using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Vector3 offset = new Vector3(0, 7, -12);

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
