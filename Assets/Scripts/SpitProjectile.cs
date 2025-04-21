using System.Collections;
using UnityEngine;

public class SpitProjectile : MonoBehaviour
{
    
    public Vector3 targetPos = Vector3.zero;
    public float speed = 5f;
    public bool isMoving = false;
    public float lifetime = 10f;

    private void FixedUpdate() {
        //move to target position
        if (isMoving) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
        }

        StartCoroutine(Despawn());
    }

    public void SetTarget(Vector3 targetPos, float speed) {
        this.targetPos = targetPos;
        this.speed = speed;
    }

    public void SetInMotion() {
        isMoving = true;
    }

    IEnumerator Despawn() {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
