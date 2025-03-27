using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour
{
    public int currentAmmo = 6;
    public Text ammoText; // Assign the UI Text element in the Inspector

    void Start()
    {
        UpdateAmmoText();
    }

    public void UpdateAmmoText()
    {
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
}