using UnityEngine;
using UnityEngine.SceneManagement;

public class teleportArea : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            ChangeSceneTo(sceneName);
        }
    }

    void ChangeSceneTo(string sn) {
        SceneManager.LoadScene(sn);
    }
}
