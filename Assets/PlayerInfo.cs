using UnityEngine;
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

        if (hasAuthority)
            this.name = playerName + (isServer ? "Server" : "Client");
    }

    // Will tell the server if the player is ready
    // From player to server : global state
    public void PlayerReady()
    {
        if (!hasAuthority)
            return;

        CmdPlayerReady(Instance.playerIndex);
    }

    [Command]
    public void CmdPlayerReady(int playerIndex)
    {
        NetworkDispatcherManager.Instance.TryStartGame(playerIndex);
    }

    // Only if also server
    public void RegisterPlayerInfo()
    {
        if (isServer)
            NetworkDispatcherManager.Instance.connectedPlayers.Add(this);
    }

    public void CopyCharactersSelectedToPossessedCharacters()
    {

        int[] ids = new int[CharactersManager.Instance.SelectedCharacters.Length];
        for(int i = 0; i < ids.Length; i++)
        {
            ids[i] = DatabaseManager.Instance.CharactersDb.GetIdFromCharacterData(CharactersManager.Instance.SelectedCharacters[i]);
        }

        CmdCopyCharactersSelectedToPossessedCharacters(ids);
    }


    [Command]
    public void CmdCopyCharactersSelectedToPossessedCharacters(int[] selectedCharacters)
    {
        for (int i = 0; i < selectedCharacters.Length; i++)
        {
            CharacterData cd = DatabaseManager.Instance.CharactersDb.GetCharacterDataFromId(selectedCharacters[i]);

            GameObject go = Instantiate(cd.networkPrefab, transform.position, Quaternion.identity) as GameObject;
            NetworkServer.SpawnWithClientAuthority(go, connectionToClient);

            // Ref to characters controller
            GetComponent<CharactersController>().possessedCharacters.Add(go);
        }
     
    }

}
