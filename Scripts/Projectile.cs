using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float velocity = 2000f;
    Weapon weapon;
    private void OnEnable()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * velocity, ForceMode.Force);
        Invoke("Hide", 0.3f);
       
    }
 
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        
        weapon = GetComponentInParent<Weapon>();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        try
        {
            transform.position = weapon.BullettTransform.position;
            transform.rotation = weapon.BullettTransform.rotation;
        }
        catch
        {

        }
        
    }
}
