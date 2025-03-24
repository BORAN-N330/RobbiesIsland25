using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Health CharacterType;
    float health;
    public Slider slider;
    [SerializeField] Transform Player;
    bool Regain = false;
    void Start()
    {
        health = (float)CharacterType;

        slider.maxValue = health;
        slider.value = health - 1000;

        StartCoroutine(RegainHealth());

        Debug.Log(health.ToString());
    }

    public void Damage(float DamageValue)
    {
        health -= DamageValue;

        slider.value = health;

        Debug.Log(health.ToString());
        if (health <= 0)
        {
            Debug.Log("Player Died");
        }
    }

    IEnumerator RegainHealth()
    {
        Regain = true;

        if (health < (float)CharacterType)
        {
            health += 200;
            Debug.Log("I'm Reviving, YAY");
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return null;
        }
        Regain = false;
    }
}
