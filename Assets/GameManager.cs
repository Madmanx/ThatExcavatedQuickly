using UnityEngine;


// Sync variable ? 
public enum EntityTurn { Player1, Player2 }

// My Turn
public enum GameState { CharacterSelection, Ingame, Win, Lose }

public class GameManager : MonoBehaviour {

    public static EntityTurn currentTurn;

    public static GameState currentState = GameState.CharacterSelection;

    public delegate void GameStateChange(string name, Color color);
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


    public void ChangeTurn(int playerIndex, string otherPlayerName, Color otherPlayerColor)
    {
        currentTurn = (EntityTurn)playerIndex;
        OnGameStateChange(otherPlayerName, otherPlayerColor);
    }

    // RefForGameLoopOnly
    // Change way to ? 
    public GameObject refCharacterSelection;
    public GameObject refIngame;


    public void StartGame(int mapSeed, int playerIndexToStartWith, string otherPlayerName, Color otherPlayerColor)
    {
        refCharacterSelection.SetActive(false);
        refIngame.SetActive(true);

        ChangeTurn(playerIndexToStartWith, otherPlayerName, otherPlayerColor);

        // ChangeGameStateFunction? 
        currentState = GameState.Ingame;
    }
}
