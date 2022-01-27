using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseWin : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag.Equals("Finish"))
        {
            Debug.Log("Collision is detected");

            if(gameObject.transform.localScale.x <= 1)
            {
                Debug.Log("Lose");
            }
            else
            {
                Debug.Log("Win");
            }
        }
    }
}
