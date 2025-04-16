using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    Ray ray;

    [Header("Shooting Properties")]
    public float range = 10f;
    public float waitTime = 1.0f;

    bool isAbleToShoot = true;

    [Header("Raycast Angle")]
    public Transform followTarget;
    public Transform rayOrigin;

    [Header("Crosshair Sight")]
    public GameObject rayFix;
    public Vector3 originCorrection = Vector3.zero;
    public Vector3 endCorrection = Vector3.zero;

    [Header("Animation")]
    public Animator rArmPivot;
    public AudioSource playerSpeaker;
    public ParticleSystem smoke;

    [Header("Bullets")]
    public GameObject bullethole;

    //ammo system
    AmmoManager ammoManager;

    private void Start() {
        ammoManager = GetComponent<AmmoManager>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (isAbleToShoot && ammoManager.hasAmmo()) {
                Shoot();
            }
        }
    }

    void Shoot() {

        bool isShooting = false;

        //ray = new Ray(transform.position, transform.forward); //from this object, forward
        ray = new Ray(rayOrigin.position + originCorrection, followTarget.transform.forward + endCorrection); //from this object, forward
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);


        //use data
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData, range)) {
            if (hitData.collider.tag == "Enemy") {
                //call enemy's hit function
                Debug.Log("Hit Enemy");

                hitData.collider.GetComponent<RabidEnemy>().shotByPlayer();

                isShooting = true;

            } else if (hitData.collider.tag == "Shootable") {
                Instantiate(bullethole, hitData.point + (hitData.normal * 0.01f), Quaternion.FromToRotation(Vector3.up, hitData.normal));

                isShooting = true;

            } else if (hitData.collider.tag == "NPC") {
                hitData.collider.gameObject.GetComponent<NPCDialogue>().Speak();
            }
        }

        if (isShooting) {
            isShooting = false;

            //decrement ammo
            ammoManager.ReduceAmmo();

            //start reload time

            isAbleToShoot = false;
            StartCoroutine(resetShootAbility());

            //animation
            rArmPivot.SetTrigger("shoot");

            //sound
            playerSpeaker.Play();

            //smoke particle
            smoke.Play();
        }
    }

    IEnumerator resetShootAbility() {
        yield return new WaitForSeconds(waitTime);
        isAbleToShoot = true;
    }
}
