using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simla
{
    public class SpawnManager : MonoSingleton<SpawnManager>
    {
        public GameObject SpawnStair(string tag, Vector3 position, Quaternion rotation)
        {
            return ObjectPooler.Instance.SpawnFromPool(tag, position, rotation);
        }

        public GameObject SpawnBall(string tag, Vector3 position, Quaternion rotation)
        {
            return ObjectPool.Instance.GetPooledObject(tag, position, rotation);
        }
    }
}
