using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkGameManager : NetworkBehaviour {

    public bool[] isReady = new bool[2];

    [ClientRpc]
    public void RpcChangeTurn()
    {
        GameManager.Instance.ChangeTurn();
    }

    private static NetworkGameManager singleton;

    public void Awake()
    {
        singleton = this;
    }

    public List<PlayerInfo> connectedPlayers = new List<PlayerInfo>();

    public static NetworkGameManager Instance
    {
        get
        {
            return singleton;
        }

        set
        {
            singleton = value;
        }
    }

    public void TryStartGame(int playerIndex)
    {
        isReady[playerIndex] = true;

        Debug.Log(PlayerInfo.Instance.numPlayer);
        Debug.Log(connectedPlayers.Count);

        // Here or catch on both button? 
        if (PlayerInfo.Instance.numPlayer == 2 && (isReady[0] && isReady[1])
            || PlayerInfo.Instance.numPlayer == 1 && (isReady[0]))
        {
            // Propagate
            RpcStartGame();
        }
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
