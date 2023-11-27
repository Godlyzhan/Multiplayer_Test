using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "CreateWeapon/WeaponStats", order = 1)]
public class WeaponStats : ScriptableObject
{
    public WeaponTypes.WeaponsTypes WeaponType;

    [Header("Blaster Stats")]
    public GameObject bulletPrefab;
    public float minScale = 0.5f;
    public float maxScale = 2.0f;
    public float maxHoldTime = 2.0f;
    public float minForce = 5.0f;
    public float maxForce = 20.0f;
    public float maxDamage = 100.0f;
    public float coolDown = 2f;

}
