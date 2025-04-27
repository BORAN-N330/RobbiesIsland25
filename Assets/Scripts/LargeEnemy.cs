using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

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

    //[Header("Spit")]
    //public GameObject spitProjectile;
    //public Transform originPoint;
    //public Transform player;
    //public float projectileSpeed = 0.1f;
    //bool isSpitting;
    //public float spitCooldownSec = 3f;

    RabidEnemy enemyScript;

    private void Start() {

        enemyScript = GetComponent<RabidEnemy>();

        hpBarIn = Instantiate(hpBar, canvas.transform);
        nameTag = hpBarIn.transform.GetChild(0).GetComponent<TMP_Text>();

        nameTag.text = enemyName;
    }

    private void Update() {
        hpBar.maxValue = enemyScript.maxHealth;

        hpBarIn.value = (int)enemyScript.health;

        //spit object

        //if (isSpitting == false) {
        //    isSpitting = true;

            //make spit sound
            //instatiate spit object

        //    GameObject inSpitProj = Instantiate(spitProjectile, originPoint.TransformPoint(originPoint.position), Quaternion.identity);
        //    inSpitProj.transform.position = transform.TransformPoint(originPoint.position);

            //Debug.Log("Spitting");

            //set to target
        //    inSpitProj.GetComponent<SpitProjectile>().SetTarget(player.position, projectileSpeed);
        //    inSpitProj.GetComponent<SpitProjectile>().SetInMotion();

        //    StartCoroutine(SpitCooldown());
        //}
    }

    private void OnDestroy() {

        if (doorObject != null) {
            doorObject.SetTrigger(animatorTrigger);
        }
        Debug.Log("Death");

        Destroy(hpBar);
    }

    //IEnumerator SpitCooldown() {
    //    yield return new WaitForSeconds(spitCooldownSec);
    //    isSpitting = false;
    //}
}
