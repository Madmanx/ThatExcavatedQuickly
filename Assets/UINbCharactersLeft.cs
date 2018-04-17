using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINbCharactersLeft : MonoBehaviour {


    void OnEnable()
    {
        CharactersManager.OnCharactersRefresh += UpdateNbCharacter;
    }


    void OnDisable()
    {
        CharactersManager.OnCharactersRefresh -= UpdateNbCharacter;
    }


    void UpdateNbCharacter() {
        if((3 - CharactersManager.Instance.currentCharacter) > 0)
        GetComponent<Text>().text = "Choose " + (3 - CharactersManager.Instance.currentCharacter).ToString() + " characters";
        else
        GetComponent<Text>().text = "";
    }
}
