using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Olcay.Player;
using Simla;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera gameCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera startCamera;

    private void Awake()
    {
        PlayerMovement.gameStarting += StartCameraFalse;
        MiniGame.LevelFinished += StartCameraTrue;
    }

    private void StartCameraFalse()
    {
        startCamera.enabled = false;
    }

    private void StartCameraTrue()
    {
        startCamera.enabled = true;
    }
}
