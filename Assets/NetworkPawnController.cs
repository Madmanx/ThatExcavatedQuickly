using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPawnController : NetworkBehaviour {

    public Weapon currentGun;

    void Update()
    {
        if (!isLocalPlayer)
            return;
    }

    [Command]
    public void CmdDoFire()
    {
        Vector3 dir;
        if (currentGun.type == TypeOfWeapon.Gun)
        {
            dir = transform.forward;
        } else
        {
            dir = (transform.forward + 2* Vector3.up).normalized;
        }
        GameObject bulletInstance = Instantiate(currentGun.Ammo, currentGun.transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        Physics.IgnoreCollision(bulletInstance.GetComponent<Collider>(), GetComponent<Collider>());
        bulletInstance.GetComponent<Rigidbody>().velocity = dir * currentGun.speed;
        NetworkServer.Spawn(bulletInstance.gameObject);
        RpcDoFire();
    }


    [ClientRpc]
    public void RpcDoFire()
    {
        //var gun = GetComponentInChildren<Gun>();
        //gun.ShootFX();
    }

    //public void ShootFX()
    //{
    //    // ... set the animator Shoot trigger parameter and play the audioclip.
    //    anim.SetTrigger("Shoot");
    //    GetComponent<AudioSource>().Play();
    //}

}
