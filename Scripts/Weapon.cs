using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponMode
{
    Singe,
    Continuously
}

[RequireComponent(typeof(Ammo))]
public class Weapon : MonoBehaviour
{
    // [SerializeField] GameObject hitEffect;
    [SerializeField] int weaponDamage = 50;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] float Range = 10000f;
    [SerializeField] float timeBetweenShots = 0.1f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletTransform;
    public Transform BullettTransform => bulletTransform;
    
    [SerializeField]
    List<GameObject> bulletPool = new List<GameObject>();

    bool changeWeaponMode = false;
    Camera FPCamera;
    Animator animator;
    Coroutine fireCoroutine;
    Ammo ammo;
    Recoil recoil;

    [SerializeField]
    WeaponMode weaponMode;
    bool canShoot = true;
    [SerializeField]
    Vector3 recoilVector = new Vector3(-0.3f, 0.3f, 0f);
    private void Awake()
    {
        FPCamera = FindObjectOfType<Camera>();
        animator = GetComponent<Animator>();
        ammo = GetComponent<Ammo>();
        recoil = FindObjectOfType<Recoil>().GetComponent<Recoil>();
        CreateBullet(10);
        

    }
   
    void Update()
    {
        Fire();
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Reload");
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(changeWeaponMode == false)
            {
                changeWeaponMode = true;
                weaponMode = WeaponMode.Continuously;
            }
            else
            {
                changeWeaponMode = false;
                weaponMode = WeaponMode.Singe;
            }
        }
    }

    private void Fire()
    {
        if (weaponMode == WeaponMode.Continuously)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (ammo.AmmoAmount > 0)
                {
                    fireCoroutine = StartCoroutine(ShootContinuoulsy());
                    
                }
                else
                {
                    // sound effect and mb auto reload
                }

            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (fireCoroutine == null) return;
                StopCoroutine(fireCoroutine);
                animator.SetBool("Shoot", false);
            }
        }
        else if(weaponMode == WeaponMode.Singe)
        {
            if (Input.GetMouseButtonDown(0) && canShoot)
            {
                if(ammo.AmmoAmount > 0)
                {
                    StartCoroutine(ShootSingle());
                }
                else
                {

                }
            }
            else if(Input.GetMouseButtonUp(0))
            {
                animator.SetBool("Shoot", false);
            }
        }
        
    }

    private IEnumerator ShootContinuoulsy()
    {

        while (ammo.AmmoAmount > 0)
        {
            animator.SetBool("Shoot", true);
            ammo.ReduceAmmoAmount();
            DisplayMuzzleFlash();
            ProcessRayCast();
            RequestBullet();
            recoil.AddRecoil(recoilVector);
            yield return new WaitForSeconds(0.1f);
        }
        animator.SetBool("Shoot", false);

    }

    private IEnumerator ShootSingle()
    {
        canShoot = false;
        animator.SetBool("Shoot", true);
        ammo.ReduceAmmoAmount();
        DisplayMuzzleFlash();
        ProcessRayCast();
        RequestBullet();
        recoil.AddRecoil(recoilVector);
        yield return new WaitForSeconds(timeBetweenShots);
        animator.SetBool("Shoot", false);
        canShoot = true;
        
    }

    private void DisplayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRayCast()
    {
        RaycastHit hit;
        var hasHit = Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, Range);
        if (hasHit)
        {
            // CreateHitImpact(hit);
            IDamagable target = hit.transform.GetComponent<IDamagable>();
            if (target != null)
            {
                target.TakeDamage(weaponDamage);
            }

        }
        else
        {
            return;
        }
    }

    
    

    public List<GameObject> CreateBullet(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var bullet = Instantiate(bulletPrefab, bulletTransform.transform.position, bulletTransform.transform.rotation);
            bullet.transform.parent = bulletTransform;
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
        return bulletPool;
    }
    public GameObject RequestBullet()
    {
        foreach (var bullet in bulletPool)
        {
            if (bullet.activeInHierarchy == false)
            {
                bullet.SetActive(true);
                return bullet;
            }
            bullet.SetActive(false);
            
        }
        return null;
    }
    //private void CreateHitImpact(RaycastHit hit)
    //{
    //    var hitVFX = Instantiate(hitEffect, hit.point, Quaternion.RotateTowards(hitEffect.transform.rotation, player.rotation, 90));
    //    Destroy(hitVFX, 1f);
    //}



}
