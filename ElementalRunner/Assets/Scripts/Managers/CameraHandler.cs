using Olcay.Player;
using UnityEngine;

namespace Simla.Managers
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] private Cinemachine.CinemachineVirtualCamera startCamera;

        private void Awake()
        {
            PlayerMovement.gameStarting += StartCameraFalse;
            Players.levelFinished += StartCameraTrue;
            Players.levelFailed += StartCameraTrue;
            ScaleChanger.levelFailed += StartCameraTrue;
        }

        private void OnDestroy()
        {
            PlayerMovement.gameStarting -= StartCameraFalse;
            Players.levelFinished -= StartCameraTrue;
            Players.levelFailed -= StartCameraTrue;
            ScaleChanger.levelFailed -= StartCameraTrue;
        }

        private void StartCameraFalse()
        {
            startCamera.Priority -= 2;
        }

        private void StartCameraTrue()
        {
            startCamera.Priority += 2;
        }
    }
}