using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTypes : MonoBehaviour
{
    public enum WeaponsTypes
    {
        Shield, //can block projectiles and can shoot force to hit them back 
        Blaster, // The more the button is held the more powerful the blaster becomes
        Stun, //Allows you to shoot and stop the enemy, allowing you to drain there energy
        Laser, //Like an ult, neon ult 
        Normal, // normal P shooter
        Key //allows the player access different part of the map. needs to upgrade
    }

    
}
