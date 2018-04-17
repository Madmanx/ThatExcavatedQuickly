using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReadyBtn : MonoBehaviour {

    void OnEnable()
    {
        CharactersManager.OnCharactersRefresh += ControlBtn;
    }


    void OnDisable()
    {
        CharactersManager.OnCharactersRefresh -= ControlBtn;
    }


    void ControlBtn()
    {
        GetComponent<Button>().interactable = CharactersManager.Instance.AllCharactersAreSelected();
    }

    public void ReadyFct()
    {
        PlayerInfo.Instance.Cmd_PlayerReady();
    }
}
