using UnityEngine;

public class WeaponPickUpTrigger : MonoBehaviour
{
    [SerializeField] private WeaponStats weaponStats;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            var weaponSwitch = other.GetComponentInParent<SphereMovement>().WeaponHandler;
            weaponSwitch.SetGameObjectPosition(gameObject, weaponStats);
            Destroy(this);
        }
    }
}
