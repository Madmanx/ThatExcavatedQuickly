using UnityEngine;


// Sync variable ? 
public enum EntityTurn { Player1, Player2 }

// My Turn
public enum GameState { CharacterSelection, Ingame, Win, Lose }

public class GameManager : MonoBehaviour {

    public static EntityTurn currentTurn;

    public static GameState currentState = GameState.CharacterSelection;

    public delegate void GameStateChange(PlayerInfo otherPlayerInfo);
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


    public void ChangeTurn(PlayerInfo currentPlayerInfo, PlayerInfo otherPlayerInfo)
    {
        currentTurn = (EntityTurn)currentPlayerInfo.playerIndex;
        OnGameStateChange(otherPlayerInfo);
    }

    // RefForGameLoopOnly
    // Change way to ? 
    public GameObject refCharacterSelection;
    public GameObject refIngame;


    public void StartGame(int mapSeed, PlayerInfo currentPlayerInfo, PlayerInfo otherPlayerInfo)
    {
        refCharacterSelection.SetActive(false);
        refIngame.SetActive(true);

        ChangeTurn(currentPlayerInfo, otherPlayerInfo);

        // ChangeGameStateFunction? 
        currentState = GameState.Ingame;
    }
}
