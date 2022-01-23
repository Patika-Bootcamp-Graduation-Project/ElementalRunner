using System;
using System.Collections;
using UnityEngine;

namespace Olcay
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

        private void ChangeMat(bool isGirlActive)
        {
            if (isGirlActive)
            {
                StartCoroutine(ChangeMatRoutine(fireMat));
            }
            else
            {
                StartCoroutine(ChangeMatRoutine(waterMat));
            }
        }

        private IEnumerator ChangeMatRoutine(Material mat)
        {
            yield return new WaitForSeconds(2f);
            mesh.sharedMaterial = mat;
        }
    }
}