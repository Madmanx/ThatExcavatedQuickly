using UnityEngine;


// Sync variable ? 
public enum EntityTurn { Player1, Player2 }

// My Turn
public enum GameState { CharacterSelection, Ingame, Win, Lose }

public class GameManager : MonoBehaviour {

    public static EntityTurn currentTurn;

    public static GameState currentState = GameState.CharacterSelection;

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


    public void ChangeTurn()
    {
        currentTurn = (currentTurn == EntityTurn.Player1) ? EntityTurn.Player2 : EntityTurn.Player1;
        OnGameStateChange();
    }

    // RefForGameLoopOnly
    // Change way to ? 
    public GameObject refCharacterSelection;
    public GameObject refIngame;


    public void StartGame()
    {
        refCharacterSelection.SetActive(false);
        refIngame.SetActive(true);

        currentTurn = EntityTurn.Player1;
        OnGameStateChange();

        // ChangeGameStateFunction? 
        currentState = GameState.Ingame;
    }
}
