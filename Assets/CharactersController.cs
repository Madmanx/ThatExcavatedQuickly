using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour {

    //public delegate void UICharacterControlledRefresh();
    //public static event UICharacterControlledRefresh OnCharacterControlledRefresh;

    public int currentPossessedCharacter = 0;

    public void Update()
    {
        if( GameManager.currentState == GameState.Ingame)
        {
            if (GameManager.currentTurn == (EntityTurn)PlayerInfo.Instance.PlayerIndex)
            {
                // My Controls
                if(Input.GetKeyDown(KeyCode.Tab))
                {
                    ControlNextCharacter();
                }
            }

        }
    }

    private void ControlNextCharacter()
    {
        currentPossessedCharacter++;
        if (currentPossessedCharacter > CharactersManager.Instance.maxCharacterPerPlayer) currentPossessedCharacter = 0;
    }

}
