using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfWeapon { Gun, Grenade}
//struct
public class Weapon : MonoBehaviour {

    public TypeOfWeapon type;
    public GameObject Ammo;
    public float speed;

}
