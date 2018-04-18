using UnityEngine;
using UnityEngine.Networking;

public class PlayerInfo : NetworkBehaviour {

    public delegate void PlayerInfoReady();
    public static event PlayerInfoReady OnPlayerInfoReady;

    //From Lobby
    [SyncVar(hook ="OnChangePlayerName")]
    public string playerName = "Player 1";

    [SyncVar(hook = "OnChangePlayerColor")]
    public Color playerColor = Color.red;

    [SyncVar(hook = "OnChangePlayerIndex")]
    public int playerIndex = -1;

    [SyncVar(hook = "OnChangeNumPlayer")]
    public int numPlayer = 0;

    [SyncVar(hook = "OnReadyToBegin")]
    public bool readyToBegin = false;

    private static PlayerInfo singleton;

    public void Awake()
    {
        singleton = this;
    }

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
        // Client
        if (readyToBegin)
            OnPlayerInfoReady();
        else
            Debug.LogError("Didn't sync hook");
    }

    public void Start()
    {
        Debug.Log("IS server :" + isServer + " Or is CLient : " + isClient + " Or has Authority :" + hasAuthority);

        if(hasAuthority)
            this.name = playerName + (isServer ? "Server" : "Client");
    }

    // From player to server : global state

    [Command]
    public void Cmd_PlayerReady()
    {
        if (!hasAuthority)
            return;

        NetworkGameManager.Instance.isReady[playerIndex] = true;
        NetworkGameManager.Instance.TryStartGame();
    }

}
