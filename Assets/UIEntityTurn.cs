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

    public void UpdateUIEntityTurn()
    {
        //GetComponent<Text>().text = PlayerInfo.Instance.PlayerName;
        GetComponent<Text>().text = GameManager.currentTurn.ToString();
        //GetComponent<Text>().color = PlayerInfo.Instance.PlayerColor;
    }
}
