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
            PlayerInfo otherPlayerInfo = GetOtherPlayerInfo(currentPlayerInfo);
            // Choose the seed of the map here
            int mapSeed = 0;

            RpcPropagateStartGame(mapSeed, currentPlayerInfo.playerIndex, otherPlayerInfo.playerName, otherPlayerInfo.playerColor);
            RpcSpawnPlayerCharacter();
        }
    }

    // Can't pass component through network because no serialize function
    // RpcPropagateStartGame(int _mapSeed, PlayerInfo playerToStartWith, PlayerInfo otherPlayerInfo)
    [ClientRpc]
    public void RpcPropagateStartGame(int _mapSeed, int playerIndexToStartWith, string otherPlayerName, Color otherPlayerColor)
    {
        GameManager.Instance.StartGame(_mapSeed, playerIndexToStartWith, otherPlayerName, otherPlayerColor);
    }

    [ClientRpc]
    public void RpcSpawnPlayerCharacter()
    {
        // Where
        CharactersController.Instance.Init();
    }

    [ClientRpc]
    public void RpcPropagateChangeTurn()
    {
        PlayerInfo currentPlayerInfo = GetPlayerInfoFromTurn(GameManager.currentTurn);
        PlayerInfo otherPlayerInfo = GetOtherPlayerInfo(currentPlayerInfo);

        GameManager.Instance.ChangeTurn(currentPlayerInfo.playerIndex, otherPlayerInfo.playerName, otherPlayerInfo.playerColor);
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
