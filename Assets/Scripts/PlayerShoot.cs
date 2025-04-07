using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    Ray ray;

    [Header("Shooting Properties")]
    public float range = 10f;

    [Header("Raycast Angle")]
    public Transform followTarget;
    public Transform rayOrigin;

    [Header("Crosshair Sight")]
    public GameObject rayFix;
    public Vector3 originCorrection = Vector3.zero;
    public Vector3 endCorrection = Vector3.zero;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }

    void Shoot() {
        
        //decrement ammo (I reccomend storing ammo in gamemanager)
        //start reload time

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
            }
        }
    }
}
