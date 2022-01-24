using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simla
{
    public class SpawnManager : MonoSingleton<SpawnManager>
    {
        public void SpawnStair(string tag, Vector3 position, Quaternion rotation)
        {
            ObjectPooler.Instance.SpawnFromPool(tag, position, rotation);
        }
    }
}
