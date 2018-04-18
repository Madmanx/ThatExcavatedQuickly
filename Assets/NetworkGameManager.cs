using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkGameManager : NetworkBehaviour {

    public bool[] isReady = new bool[2];

    public SyncListBool test = new SyncListBool() { false, false };

    private static NetworkGameManager singleton;

    public static NetworkGameManager Instance
    {
        get
        {
            return singleton;
        }
    }

    public void Awake()
    {
        test.Add(false);
        test.Add(false);
        singleton = this;
    }

    [ClientRpc]
    public void RpcChangeTurn()
    {
        GameManager.Instance.ChangeTurn();
    }


    public bool TryStartGame(int playerIndex)
    {
        Debug.Log(test[playerIndex]);

        // Here or catch on both button? 
        if (PlayerInfo.Instance.numPlayer == 2 && (isReady[0] && isReady[1])
            || PlayerInfo.Instance.numPlayer == 1 && (isReady[0]))
        {
            Debug.Log("startGameici");
            RpcStartGame();

            // Ne pas utiliser
            return true;
        }
        return false;
    }

    [Command]
    public void CmdTryStartGame(int playerIndex)
    {
        isReady[playerIndex] = true;

        // Here or catch on both button? 
        if (PlayerInfo.Instance.numPlayer == 2 && (isReady[0] && isReady[1])
            || PlayerInfo.Instance.numPlayer == 1 && (isReady[0]))
        {
            Debug.Log("startGameici");
            RpcStartGame();

        }
    }


    public override void OnStartLocalPlayer()
    {
        if(hasAuthority)
        {
            singleton = this;
        }


    }

    public void Update()
    {
        if (!isServer)
            return;

    }


    [ClientRpc]
    public void RpcStartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void SpawnPlayerCharacter(List<CharacterData> cP1, List<CharacterData> cP2)
    {
        //NetworkSpawn
    }


}
