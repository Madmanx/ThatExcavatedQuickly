using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPawnController : NetworkBehaviour {

    public Weapon currentGun;

    public void Start()
    {
        
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;
    }

    [Command]
    public void CmdDoFire()
    {
        // ... instantiate the rocket facing right and set it's velocity to the right. 
        GameObject bulletInstance = Instantiate(currentGun.Ammo, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        bulletInstance.GetComponent<Rigidbody>().velocity = new Vector2(currentGun.speed, 0);
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
