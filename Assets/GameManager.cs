using UnityEngine;
using UnityEngine.Networking;

// Sync variable ? 
public enum EntityTurn { Player1, Player2 }

// My Turn
public enum GameState { CharacterSelection, Ingame, Win, Lose }

public class GameManager : NetworkBehaviour {

    public static EntityTurn currentTurn;

    public static GameState currentState = GameState.CharacterSelection;

    public bool[] isReady = new bool[2];

    public delegate void GameStateChange();
    public static event GameStateChange OnGameStateChange;

    private static GameManager singleton;

    public static GameManager Instance
    {
        get
        {
            return singleton;
        }
    }

    public void Awake()
    {
        singleton = this;
    }

    [ClientRpc]
    public void RpcChangeTurn() { 
        currentTurn = (currentTurn == EntityTurn.Player1) ? EntityTurn.Player2 : EntityTurn.Player1;
        OnGameStateChange();
    }

    // RefForGameLoopOnly
    // Change way to ? 
    public GameObject refCharacterSelection;
    public GameObject refIngame;

    public void Update()
    {
        if (!isServer)
            return;

        if (isReady[0] && isReady[1])
        {
            RpcStartGame();
        }

    }

    //public void StartGame()
    //{
    //    if(isReady[0] && isReady[1])
    //    {
    //        RpcStartGame();
    //    }
     
    //}

    [ClientRpc]
    public void RpcStartGame()
    {
        refCharacterSelection.SetActive(false);
        refIngame.SetActive(true);

        currentTurn = EntityTurn.Player1;
        OnGameStateChange();

        // ChangeGameStateFunction? 
        currentState = GameState.Ingame;
    }
}
