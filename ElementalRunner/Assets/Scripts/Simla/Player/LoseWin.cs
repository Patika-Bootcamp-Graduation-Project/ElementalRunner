using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseWin : MonoBehaviour
{
    private float startScale;

    [SerializeField] private ScaleChanger scaleChanger;

    private void Start()
    {
        startScale = gameObject.transform.localScale.x;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag.Equals("Finish"))
        {
            Debug.Log("Collision is detected");

            if(gameObject.transform.localScale.x <= startScale)
            {
                Debug.Log("Lose");
            }
            else
            {
                Debug.Log("Win");
            }

            if(scaleChanger.score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", scaleChanger.score);
            }
            Debug.Log(PlayerPrefs.GetInt("HighScore", 0));
        }
    }
}
