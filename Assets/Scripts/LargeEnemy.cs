using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LargeEnemy : MonoBehaviour
{

    [Tooltip("Make sure to attach object with animator")]
    public Animator doorObject;
    public string animatorTrigger = "OpenDoor";

    [Header("UI")]
    public string enemyName = "Rayb-oss";
    public Slider hpBar;
    public Canvas canvas;
    TMP_Text nameTag;
    Slider hpBarIn;

    RabidEnemy enemyScript;

    private void Start() {

        enemyScript = GetComponent<RabidEnemy>();

        Debug.Log("Adding stuff");
        hpBarIn = Instantiate(hpBar, canvas.transform);
        nameTag = hpBarIn.transform.GetChild(0).GetComponent<TMP_Text>();

        nameTag.text = enemyName;
    }

    private void Update() {
        hpBar.maxValue = enemyScript.maxHealth;

        Debug.Log(enemyScript.health);
        hpBarIn.value = (int)enemyScript.health;
    }

    private void OnDestroy() {

        if (doorObject != null) {
            doorObject.SetTrigger(animatorTrigger);
        }
        Debug.Log("Death");

        Destroy(hpBar);
    }
}
