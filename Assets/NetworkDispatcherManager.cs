using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// ONLY FOR SERVER
public class NetworkDispatcherManager : NetworkBehaviour {

    public bool[] isReady = new bool[2]; // max could define by connectedPlayers.Count

    private static NetworkDispatcherManager singleton;

    public void Awake()
    {
        singleton = this;
        // Destroy other? -> networkdispatchers are disable on non-server clients from authority
    }

    public List<PlayerInfo> connectedPlayers = new List<PlayerInfo>();

    public static NetworkDispatcherManager Instance
    {
        get
        {
            // If not server will return an error
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
        
        bool allPlayerAreReady = true;
        for( int i = 0; i< connectedPlayers.Count; i++)
        {
            if(!isReady[i])
            {
                allPlayerAreReady = false;
            }
        }
        if (allPlayerAreReady)
        {
            EntityTurn randPlayerTurn = (EntityTurn)Random.Range(0, connectedPlayers.Count);
            PlayerInfo currentPlayerInfo = GetPlayerInfoFromTurn(randPlayerTurn);
            // Choose the seed of the map here
            int mapSeed = 0;

            RpcPropagateStartGame(mapSeed, currentPlayerInfo, GetOtherPlayerInfo(currentPlayerInfo));
            RpcSpawnPlayerCharacter();
        }
    }

    [ClientRpc]
    public void RpcPropagateStartGame(int _mapSeed, PlayerInfo playerToStartWith, PlayerInfo otherPlayerInfo)
    {
        GameManager.Instance.StartGame(_mapSeed, playerToStartWith, otherPlayerInfo);
    }

    [ClientRpc]
    public void RpcSpawnPlayerCharacter()
    {
        //NetworkSpawn
        PlayerInfo.Instance.gameObject.AddComponent<CharactersController>();
    }

    [ClientRpc]
    public void RpcPropagateChangeTurn()
    {
        PlayerInfo currentPlayerInfo = GetPlayerInfoFromTurn(GameManager.currentTurn);

        GameManager.Instance.ChangeTurn(currentPlayerInfo, GetOtherPlayerInfo(currentPlayerInfo));
    }

    #region Utils
    public PlayerInfo GetOtherPlayerInfo(PlayerInfo p)
    {
        for (int i = 0; i < connectedPlayers.Count; i++)
        {
            if (p != connectedPlayers[i])
                return p;
        }

        // Cas only one player
        return PlayerInfo.Instance;
    }

    public PlayerInfo GetPlayerInfoFromTurn(EntityTurn turn)
    {
        for (int i = 0; i < connectedPlayers.Count; i++)
        {
            if ((int)GameManager.currentTurn == (int)turn)
                return connectedPlayers[i];

        }

        // Cas only one player
        return PlayerInfo.Instance;
    }
    #endregion
}
