using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
    }
}
