﻿using UnityEngine;
using UnityEngine.Networking;

public class PlayerInfo : NetworkBehaviour {

    public delegate void PlayerInfoReady();
    public static event PlayerInfoReady OnPlayerInfoReady;

    //From Lobby
    [SyncVar(hook = "OnChangePlayerName")]
    public string playerName = "Player 1";

    [SyncVar(hook = "OnChangePlayerColor")]
    public Color playerColor = Color.red;

    [SyncVar(hook = "OnChangePlayerIndex")]
    public int playerIndex = -1;

    [SyncVar(hook = "OnChangeNumPlayer")]
    public int numPlayer = 0;

    [SyncVar(hook = "OnReadyToBegin")]
    public bool readyToBegin = false;

    public static PlayerInfo singleton;

    public static PlayerInfo Instance
    {
        get
        {
            return singleton;
        }
    }

    public void OnChangePlayerName(string value)
    {
        playerName = value;
    }

    public void OnChangePlayerColor(Color value)
    {
        playerColor = value;
    }

    public void OnChangePlayerIndex(int value)
    {
        playerIndex = value;
    }

    public void OnChangeNumPlayer(int value)
    {
        numPlayer = value;
    }

    public void OnReadyToBegin(bool value)
    {
        readyToBegin = value;
    }

    // After enable ? 
    public override void OnStartLocalPlayer()
    {
        if (!hasAuthority)
            return;

        // Store mine
        singleton = this;

        // Client
        if (readyToBegin)
            OnPlayerInfoReady();
        else
            Debug.LogError("Didn't sync hook");
    }

    public void Start()
    {
        if (isServer)
            RegisterPlayerInfo();

        this.name = playerName + (isServer ? "Server" : "Client");
    }

    // Will tell the server if the player is ready
    // From player to server : global state
    public void PlayerReady()
    {
        if (!hasAuthority)
            return;

        CmdPlayerReadyForPlayer(Instance.playerIndex);
    }

    [Command]
    public void CmdPlayerReadyForPlayer(int playerIndex)
    {
        NetworkDispatcherManager.Instance.TryStartGame(playerIndex);
    }

    public void WorldReady()
    {
        if (!hasAuthority)
            return;

        CmdWorldReadyForPlayer(Instance.playerIndex);
    }

    [Command]
    public void CmdWorldReadyForPlayer(int playerIndex)
    {
        NetworkDispatcherManager.Instance.SpawnCharacters(playerIndex);
    }

    // Only if also server
    public void RegisterPlayerInfo()
    {
        if (isServer && NetworkDispatcherManager.Instance)
            NetworkDispatcherManager.Instance.connectedPlayers.Add(this);
    }



}
