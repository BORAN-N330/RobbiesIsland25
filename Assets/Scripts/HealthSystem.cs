using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    //[SerializeField] Health CharacterType;
    float health;
    public Slider slider;
    //[SerializeField] Transform Player;
    bool regain = true;

    public float iframesTime = 1f;
    bool iframes = false;

    //default values
    //use public to show variables in inspector
    public float maxHealth = 10f;
    public float regainAmt = 1f;
    public float regenTime = 2f;


    void Start()
    {
        health = maxHealth;

        slider.maxValue = health;
        slider.value = health;

        Debug.Log(health.ToString());
    }

    private void Update() {

        if (health < maxHealth) {
            if (regain && iframes == false) {
                regain = false;
                StartCoroutine(RegainHealth());
            }
        }
    }

    public void Damage(float DamageValue)
    {
        if (iframes == false) {

            iframes = true;
            StartCoroutine(IFramesCooldown());

            health -= DamageValue;

            DisplayHealth();

            Debug.Log(health.ToString());
            if (health <= 0) {
                Debug.Log("Player Died");

                PlayerDeath();
            }
        }
    }

    void DisplayHealth() {
        slider.value = health;
    }

    public void PlayerDeath() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator RegainHealth()
    {
        health += regainAmt;
        DisplayHealth();
        Debug.Log("I'm Reviving, YAY");
        
        yield return new WaitForSeconds(regenTime);
        regain = true;
    }

    IEnumerator IFramesCooldown() {
        yield return new WaitForSeconds(iframesTime);
        iframes = false;
    }
}
