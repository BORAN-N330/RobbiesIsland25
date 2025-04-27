using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public string startName;

    public void RestartGameFromBeg() {
        SceneManager.LoadScene(startName);
    }
}
