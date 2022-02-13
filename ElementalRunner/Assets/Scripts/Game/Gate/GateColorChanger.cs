using System;
using System.Collections;
using Olcay.Player;
using UnityEngine;

namespace Olcay.Gate
{
    public class GateColorChanger : MonoBehaviour
    {
        private MeshRenderer _mesh;
        private MeshRenderer mesh => _mesh = GetComponent<MeshRenderer>();

        [SerializeField] private Material waterMat;
        [SerializeField] private Material fireMat;


        private void Awake()
        {
            Players.playerChanged += ChangeMat;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
            Players.playerChanged -= ChangeMat;
        }

        private void ChangeMat(bool isGirlActive)
        {
            StartCoroutine(isGirlActive ? ChangeMatRoutine(fireMat) : ChangeMatRoutine(waterMat));
        }

        private IEnumerator ChangeMatRoutine(Material mat)
        {
            yield return Extentions.GetWait(2f);
            mesh.sharedMaterial = mat;
        }
    }
}