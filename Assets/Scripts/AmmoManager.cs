using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoManager : MonoBehaviour
{
    public int maxAmmo = 6;
    int currentAmmo;
    public TMP_Text ammoText; // Assign the UI Text element in the Inspector

    void Start()
    {
        UpdateAmmoText();
        currentAmmo = maxAmmo;
    }

    public void UpdateAmmoText()
    {
        //display the ammo text
        ammoText.text = "Ammo: " + currentAmmo.ToString();
    }

    public void ReduceAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            UpdateAmmoText();
        }
    }

    public void ReloadAmmo() {
        currentAmmo = maxAmmo;
    }

    public bool hasAmmo() {
        if (currentAmmo > 0) {
            return true;
        }
        else {
            return false;
        }
    }
}