using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// ONLY FOR SERVER
public class NetworkDispatcherManager : NetworkBehaviour {

    // Sync Bool List ? -> no because network dispatcher doesn't exist on client could be placed in GameManager(not on network but could with a control before) or PlayerInfo (but i don't want to surcharge) or Something connected to PlayerInfo
    public bool[] isPlayerReadyForStartGame = new bool[2]; // max could define by connectedPlayers.Count
    public bool[] isPlayerReadyForSpawnCharacters = new bool[2]; // max could define by connectedPlayers.Count

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

    // Here i know that 
    public void TryStartGame(int playerIndex)
    {
        isPlayerReadyForStartGame[playerIndex] = true;

        bool allPlayerAreReady = true;
        for (int i = 0; i < connectedPlayers.Count; i++)
        {
            if (!isPlayerReadyForStartGame[i])
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


        }
    }

    // Here i know that the world is generated
    public void SpawnCharacters(int playerIndex)
    {
        isPlayerReadyForSpawnCharacters[playerIndex] = true;
        bool allPlayerAreReady = true;
        for (int i = 0; i < connectedPlayers.Count; i++)
        {
            if (!isPlayerReadyForSpawnCharacters[i])
            {
                allPlayerAreReady = false;
            }
        }

        if (allPlayerAreReady)
        {
            RpcPropagateCharactersSpawn(playerIndex);
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
    public void RpcPropagateCharactersSpawn(int playerIndex)
    {

        // Get pos
        int[,] pos = new int[3, 2];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                int coord = Random.Range(1, 10);
                coord += playerIndex;
                pos[i, j] = coord;
            }
        }

        CharactersController.Instance.Init(pos);
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
