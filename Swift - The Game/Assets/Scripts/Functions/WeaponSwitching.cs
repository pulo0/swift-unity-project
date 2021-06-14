using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    
    void Update()
    {
        SelectWeapon();
    }

    void SelectWeapon()
    {
        int index = 0;
        foreach (Transform weapon in transform)
        {
            if(index == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            index++;
        }
    }
}
