using UnityEngine;
using UnityEngine.UI;

public class UIEntityTurn : MonoBehaviour {

    void OnEnable()
    {
        GameManager.OnGameStateChange += UpdateUIEntityTurn;
    }


    void OnDisable()
    {
        GameManager.OnGameStateChange -= UpdateUIEntityTurn;
    }

    public void UpdateUIEntityTurn(PlayerInfo currentPlayingPlayerInfo)
    {
        GetComponent<Text>().text = currentPlayingPlayerInfo.playerName;
        GetComponent<Text>().color = currentPlayingPlayerInfo.playerColor;
    }
}
