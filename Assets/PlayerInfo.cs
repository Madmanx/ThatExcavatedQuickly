using UnityEngine;
using UnityEngine.Networking;

public class PlayerInfo : NetworkBehaviour {

    public delegate void PlayerInfoReady();
    public static event PlayerInfoReady OnPlayerInfoReady;

    string playerName = "Player 1";
    Color playerColor = Color.red;
    int playerIndex = -1;
    
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

    public string PlayerName
    {
        get
        {
            return singleton.playerName;
        }
    }

    public Color PlayerColor
    {
        get
        {
            return singleton.playerColor;
        }
    }

    public int PlayerIndex
    {
        get
        {
            return playerIndex;
        }
    }

    public void Start()
    {
        #if UNITY_EDITOR
        Init(Color.blue, "Remi", 0);
        #endif
    }

    public void Init(Color _selectedColor, string _selectedPlayerName, int _playerIndex)
    {
        playerColor = _selectedColor;
        playerName = _selectedPlayerName;
        playerIndex = _playerIndex;

        Debug.Log(playerColor);
        Debug.Log(playerName);
        Debug.Log(playerIndex);
        OnPlayerInfoReady();
    }
        
    [Command]
    public void Cmd_PlayerReady()
    {
        GameManager.Instance.isReady[playerIndex] = true;
    }
     

}
