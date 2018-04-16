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
        GetComponent<Text>().text = PlayerInfo.Instance.PlayerName;
        GetComponent<Text>().color = PlayerInfo.Instance.PlayerColor;
    }
}
