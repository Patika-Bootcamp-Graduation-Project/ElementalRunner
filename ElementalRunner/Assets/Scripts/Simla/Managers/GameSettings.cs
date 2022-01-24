using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Game Settings")]
public class GameSettings : ScriptableObject
{
    public float characterSpeed;
    public float damage;
    public float maxScale;
    public float minScale;
    public float jumpScale;
    public float jumpHeight = 4;
    public float fallSpeed = 9.8f;
}
