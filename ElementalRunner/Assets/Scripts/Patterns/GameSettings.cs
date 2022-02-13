using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("PlayerMovement")]
    public float characterSpeed =5f;
    public float characterUpValue = 4f;
    public float gravity = 9.8f;
    [Header("Players")]
    public Color girlFogColor = new Color(0.4666667f, 0.8f, 0.7933347f, 1f);
    public Color boyFogColor = new Color(0.8018868f, 0.4652457f, 0.4652457f, 1f);
    public float stairInstantiateCD = 0f;
    public float stairSetActiveFalseCD = 2f;
    public float characterLocalScaleDecreaseWithStair = 0.03f;
    public float characterLocalScaleDecreaseWithBall = 0.25f;
    public float characterDeadControlValue = 1f;

    [Header("ScaleChanger")] 
    public float characterScaleChangeValueWithObjectives = 0.1f;
    public int increaseScoreValue = 10;
    public int decreaseScoreValue = -5;

    [Header("Balls")] 
    public float ballSpeed = 10f;
    
}
