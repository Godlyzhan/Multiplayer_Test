using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Transform[] weaponPositions;
    [SerializeField] private List<WeaponStats> weaponStatsList = new List<WeaponStats>(); //Ensure there is a default state
    [SerializeField] private Transform ShootingPoint;

    private WeaponStats activeWeaponStats;
    private WeaponTypes.WeaponsTypes previousWeaponType;

    private int currentActiveWeapon = 0;
    private int weaponCount = 0;
    private int maxWeaponCount = 4;
    private int minWeaponCount = 0;

    private bool weaponSwapped;

    //Blaster 
    private GameObject currentBullet;
    private float chargeStartTime;
    private bool isCharging = false;
    private float chargeTime;
    private float coolDownTime;
    private Rigidbody bulletRigidBody;

    private void OnEnable()
    {
        inputHandler.NextWeaponSwitchEvent += NextWeapon;
        inputHandler.PreviousWeaponSwitchEvent += PreviousWeapon;
        inputHandler.ShootEvent += Shoot;
        inputHandler.ReleaseShotEvent += FireBaster;

        activeWeaponStats = weaponStatsList[0];
    }

    private void OnDisable()
    {
        inputHandler.NextWeaponSwitchEvent -= NextWeapon;
        inputHandler.PreviousWeaponSwitchEvent -= PreviousWeapon;
        inputHandler.ShootEvent -= Shoot;
        inputHandler.ReleaseShotEvent -= FireBaster;
    }

    private void Update()
    {
        coolDownTime += Time.deltaTime;

        if (isCharging && coolDownTime > activeWeaponStats.coolDown)
        {
            chargeStartTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeStartTime, 0, activeWeaponStats.maxHoldTime);
            float scale = Mathf.Lerp(activeWeaponStats.minScale, activeWeaponStats.maxScale, chargeTime / activeWeaponStats.maxHoldTime);
            currentBullet.transform.localScale = new Vector3(scale, scale, scale);
            currentBullet.transform.position = ShootingPoint.transform.position;
        }
    }

    public void SetGameObjectPosition(GameObject pickObject, WeaponStats weaponStats)
    {
        if (weaponCount < 4)
        {
            weaponCount++;
            weaponStatsList[weaponCount] = weaponStats;
            pickObject.transform.parent = weaponPositions[weaponCount];
            pickObject.transform.position = weaponPositions[weaponCount].position;
            pickObject.transform.eulerAngles = weaponPositions[weaponCount].eulerAngles;
        }
    }

    private void NextWeapon()
    {
        currentActiveWeapon = currentActiveWeapon > minWeaponCount ? currentActiveWeapon - 1: maxWeaponCount;
        activeWeaponStats = weaponStatsList[currentActiveWeapon];

        var newRotation = Mathf.Lerp(transform.eulerAngles.y , transform.eulerAngles.y + 72, 0.5f);
        transform.rotation = Quaternion.Euler(0f, newRotation, 0f);
    }

    private void PreviousWeapon()
    {
        currentActiveWeapon = currentActiveWeapon < maxWeaponCount ? currentActiveWeapon + 1: minWeaponCount;
        activeWeaponStats = weaponStatsList[currentActiveWeapon];

        var newRotation = Mathf.Lerp(transform.eulerAngles.y , transform.eulerAngles.y - 72, 0.5f);
        transform.rotation = Quaternion.Euler(0f, newRotation, 0f);
    }

    private void Shoot()
    {
        switch (activeWeaponStats.WeaponType)
        {
            case WeaponTypes.WeaponsTypes.Blaster:
                PowerUpBlaster();
                break;
            case WeaponTypes.WeaponsTypes.Shield:
                Shield();
                break;
            case WeaponTypes.WeaponsTypes.Stun:
                Stun();
                break;
            case WeaponTypes.WeaponsTypes.Laser:
                Laser();
                break;
            case WeaponTypes.WeaponsTypes.Normal:
                NormalWeapon();
                break;
            case WeaponTypes.WeaponsTypes.Key:
                Key();
                break;
        }
    }

    private void PowerUpBlaster()
    {
        isCharging = true;
        currentBullet = Instantiate(activeWeaponStats.bulletPrefab, ShootingPoint.position, Quaternion.identity);
        currentBullet.transform.localScale = Vector3.zero;
        bulletRigidBody = currentBullet.GetComponent<Rigidbody>();
    }

    private void FireBaster()
    {
        if (!isCharging && coolDownTime < activeWeaponStats.coolDown)
        {
            return;
        }

        float force = Mathf.Lerp(activeWeaponStats.minForce, activeWeaponStats.maxForce, chargeTime / activeWeaponStats.maxHoldTime);
        float damage = Mathf.Lerp(0, activeWeaponStats.maxDamage, chargeTime / activeWeaponStats.maxHoldTime);

        bulletRigidBody.velocity = Vector3.zero;
        bulletRigidBody.AddForce(ShootingPoint.forward * force, ForceMode.Impulse);

        coolDownTime = 0f;
        chargeTime = 0f;
        isCharging = false;
        chargeStartTime = 0f;
        Destroy(currentBullet.gameObject, 5f);
    }

    private void Shield()
    {
        //Blocks incoming enemy attacks
        //Shoots a force back, consumes a lot of energy
        //energy gets depleted when the shield is hit
    }

    private void Stun()
    {
        //Stun allow the player to drain energy from the NPC when the are stunned.
        //We cannot get energy from them once they are destroyed
    }

    private void Laser()
    {
        //Laser beam that does a lot of damage and is constantly on until the shoot button is felt
        //Consumes a lot of energy, does the most damage over time
    }

    private void NormalWeapon()
    {
        //Shoot constants energy bullets
    }

    private void Key()
    {
        //needed to extract energy from NPC and objects
    }
}

