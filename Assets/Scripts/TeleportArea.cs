using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportArea : MonoBehaviour
{
    public string sceneToTP = "";
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Player") {
            SceneManager.LoadScene(sceneToTP);
        }
    }
}
