using System;
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
        Players.playerCollisionWithLevelFinish += StartCameraTrue;
        MiniGame.LevelFinished += StartCameraTrue;
        Players.levelFailed += StartCameraTrue;
        ScaleChanger.levelFailed += StartCameraTrue;
    }

    private void OnDestroy()
    {
        PlayerMovement.gameStarting -= StartCameraFalse;
        Players.playerCollisionWithLevelFinish -= StartCameraTrue;
        MiniGame.LevelFinished -= StartCameraTrue;
        Players.levelFailed -= StartCameraTrue;
        ScaleChanger.levelFailed -= StartCameraTrue;
    }

    private void StartCameraFalse()
    {
        //startCamera.enabled = false;
        startCamera.Priority -= 2;

    }

    private void StartCameraTrue()
    {
        //startCamera.enabled = true;
        startCamera.Priority += 2;
    }
}
