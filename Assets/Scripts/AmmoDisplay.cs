using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmmoDisplay : MonoBehaviour
{
    public int ammo;
    public bool isFiring;
    // Use this forr initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !isFiring && ammo > 0)
        {
            isFiring = true;
            ammo--;
            isFiring = false;
        }
    }

}