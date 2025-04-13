using UnityEngine;

public class LargeEnemy : MonoBehaviour
{

    [Tooltip("Make sure to attach object with animator")]
    public Animator doorObject;
    public string animatorTrigger = "OpenDoor";

    private void OnDestroy() {

        if (doorObject != null) {
            doorObject.SetTrigger(animatorTrigger);
        }
        Debug.Log("Death");
    }
}
