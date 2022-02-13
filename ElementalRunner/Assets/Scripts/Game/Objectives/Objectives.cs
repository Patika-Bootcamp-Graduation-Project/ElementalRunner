using System;
using System.Security.Cryptography;
using UnityEngine;

namespace Olcay.Objectives
{
    public class Objectives : MonoBehaviour
    {
        
        public static event Action<string> collisionWithObjective;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                collisionWithObjective?.Invoke(gameObject.tag);
                Destroy(gameObject);
            }
        }
    }
}

