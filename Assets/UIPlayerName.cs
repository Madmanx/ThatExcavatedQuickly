using UnityEngine;
using UnityEngine.UI;

public class UIPlayerName : MonoBehaviour {

    void OnEnable()
    {
        PlayerInfo.OnPlayerInfoReady += UpdatePlayerName;
    }


    void OnDisable()
    {
        PlayerInfo.OnPlayerInfoReady -= UpdatePlayerName;
    }


    private void UpdatePlayerName()
    {
        GetComponent<Text>().text = PlayerInfo.Instance.playerName + " P" +PlayerInfo.Instance.playerIndex;
        GetComponent<Text>().color = PlayerInfo.Instance.playerColor;
    }
}
