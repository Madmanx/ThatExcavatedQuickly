using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;

public class GameManager : MonoBehaviour {

    public delegate void UIRefresh();
    public static event UIRefresh OnUIRefresh;

    public List<CharacterData>[] selectedCharacters;
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

    public void Start()
    {

        // DJAJDNJA
        Debug.Log("test");
#if UNITY_EDITOR
        Debug.Log("test");
        selectedCharacters = new List<CharacterData>[1];
        #   else
        Debug.Log(LobbyManager.singleton.numPlayers);
        selectedCharacters = new List<CharacterData>[LobbyManager.singleton.numPlayers];
        #endif

    }

    public void AddCharacters(int PlayerIndex, CharacterData c)
    {
        if (selectedCharacters[PlayerIndex].Count > 2)
            return;
     
        if (!selectedCharacters[PlayerIndex].Contains(c))
            selectedCharacters[PlayerIndex].Add(c);
        else
            selectedCharacters[PlayerIndex].Remove(c);
        OnUIRefresh();
    }
}
