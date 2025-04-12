using System.Collections;
using System.Drawing;
using UnityEngine;

public class RabidEnemy : MonoBehaviour
{
    public float maxHealth = 10f;
    public float knockBack = 5f;
    float health;

    Rigidbody rb;

    //have collision with player
    //when player collides with "Enemy" tag, call onHit in player

    private void Start() {
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        //check if player is in bounds
        //run at player
        //use AI nav?
    }

    public void shotByPlayer() {
        //this is called when the player shoots an enemy
        health--;

        //gives the enemy knowckback when shot
        rb.AddForce(transform.forward * -1 * knockBack, ForceMode.Impulse);

        if (health <= 0) {
            //the enemy is dead

            //make collider a trigger
            //delete model
            Die();
        } else {
            GetComponent<AudioSource>().Play();
        }
    }

    public void Die() {
        rb.useGravity = false;
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<EnemyNavigation>().Die();
        gameObject.GetComponent<EnemyNavigation>().enabled = false;
        GetComponent<Animator>().SetTrigger("die");
        StartCoroutine(WaitForDeath());
    }

    IEnumerator WaitForDeath() {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
