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

    public void UpdateUIEntityTurn(string playerName, Color playerColor)
    {
        GetComponent<Text>().text = playerName;
        GetComponent<Text>().color = playerColor;
    }
}
