using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simla
{
    public class SpawnManager : MonoSingleton<SpawnManager>
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public GameObject SpawnStair(string tag, Vector3 position, Quaternion rotation)
        {
            return ObjectPooler.Instance.SpawnFromPool(tag, position, rotation);
        }

        public void SpawnBall(string tag, Vector3 position, Quaternion rotation)
        {
            ObjectPooler.Instance.SpawnFromPool(tag, position, rotation);
        }
    }
}
