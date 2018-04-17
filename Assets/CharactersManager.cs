using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour {

    public delegate void UICharactersRefresh();
    public static event UICharactersRefresh OnCharactersRefresh;

    // Not static to have one différente per player
    private static CharactersManager singleton;

    public int maxCharacterPerPlayer = 3;

    private CharacterData[] selectedCharacters;
    public int currentCharacter = 0;


    public CharacterData[] SelectedCharacters
    {
        get
        {
            return selectedCharacters;
        }
    }


    public static CharactersManager Instance
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
        selectedCharacters = new CharacterData[maxCharacterPerPlayer];
    }

    public bool AddCharacter(CharacterData c)
    {

        // Controls
        if (currentCharacter+1 > maxCharacterPerPlayer )
            return false;
        
        selectedCharacters[currentCharacter] = c;
        currentCharacter++;

        OnCharactersRefresh();



        return true;
    }


    public bool RemoveCharacter()
    {

        // Controls
        if (currentCharacter-1 < 0)
            return false;

        currentCharacter--;

        selectedCharacters[currentCharacter] = null;
        OnCharactersRefresh();

        return true;
    }

    public bool AllCharactersAreSelected()
    {
        return (currentCharacter == maxCharacterPerPlayer);
    }
}
