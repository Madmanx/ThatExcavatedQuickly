using UnityEngine;
using Prototype.NetworkLobby;
using System.Collections;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook 
{
    public static int index = 0;

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        gamePlayer.GetComponent<PlayerInfo>().playerName = lobby.playerName;
        gamePlayer.GetComponent<PlayerInfo>().name = lobby.playerName + (lobby.isServer?"Server":"Client");
        gamePlayer.GetComponent<PlayerInfo>().playerColor = lobby.playerColor;
        gamePlayer.GetComponent<PlayerInfo>().playerIndex = index++;
        gamePlayer.GetComponent<PlayerInfo>().numPlayer = manager.numPlayers;
        gamePlayer.GetComponent<PlayerInfo>().readyToBegin = true;
    }
}
