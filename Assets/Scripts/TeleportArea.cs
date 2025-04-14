using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportArea : MonoBehaviour
{
    public string sceneToTP = "";
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            SceneManager.LoadScene(sceneToTP);
        }
    }

}
