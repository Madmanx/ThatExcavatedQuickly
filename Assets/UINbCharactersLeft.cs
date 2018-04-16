using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINbCharactersLeft : MonoBehaviour {


    void OnEnable()
    {
        GameManager.OnUIRefresh += UpdateNbCharacter;
    }


    void OnDisable()
    {
        GameManager.OnUIRefresh -= UpdateNbCharacter;
    }


    void UpdateNbCharacter() {
        GetComponent<Text>().text = "Choose " + (3 - GameManager.Instance.selectedCharacters[PlayerInfo.Instance.PlayerIndex].Count).ToString() + " Characters.";
    }
}
