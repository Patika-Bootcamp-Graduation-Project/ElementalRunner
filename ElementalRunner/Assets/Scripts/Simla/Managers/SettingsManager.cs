using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings Manager", menuName = "Game Settings")]
public class SettingsManager : ScriptableObject
{
    public float characterSpeed;
    public float damage;
    public float maxScale;
    public float minScale;
    public float jumpScale;
    public float jumpHeight = 4;
    public float fallSpeed = 9.8f;
}
