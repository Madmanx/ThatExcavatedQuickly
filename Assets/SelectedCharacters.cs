using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCharacters : MonoBehaviour {

    void OnEnable()
    {
        CharactersManager.OnCharactersRefresh += UpdateCharactersSelected;
    }


    void OnDisable()
    {
        CharactersManager.OnCharactersRefresh -= UpdateCharactersSelected;
    }

    void UpdateCharactersSelected()
    {
        for (int i=0; i< transform.childCount; i++)
        {
            if (CharactersManager.Instance.SelectedCharacters[i] == null)
                transform.GetChild(i).gameObject.SetActive(false);
            else
            {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).GetComponent<Image>().sprite = CharactersManager.Instance.SelectedCharacters[i].sprite;
            }
        }
    }
}
