using UnityEngine;

public class LoseWin : MonoBehaviour
{
    private float startScale;
    public int score=0;

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

            if(score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            Debug.Log(PlayerPrefs.GetInt("HighScore", 0));
        }
    }
}
