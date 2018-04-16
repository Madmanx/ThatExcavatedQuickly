using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour {

    void OnEnable()
    {
        PlayerInfo.OnPlayerInfoReady += LoadCharacters;
    }


    void OnDisable()
    {
        PlayerInfo.OnPlayerInfoReady -= LoadCharacters;
    }

    private bool isReady = false;
    private int currentSelectedCharacterIndex = 0;
    private int nbCharacters;

    private GameObject[] goCharactersToSelect;
    private List<CharacterData> AllCharacters;

    private void LoadCharacters()
    {
        AllCharacters = DatabaseManager.Instance.CharactersDb.Characters;
        nbCharacters = AllCharacters.Count;
        goCharactersToSelect = new GameObject [nbCharacters];
        for (int iC = 0; iC < nbCharacters; iC++)
        {
            CharacterData cd = AllCharacters[iC];
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + cd.prefab.GetComponent<Collider>().bounds.extents.y, transform.position.z);
            goCharactersToSelect[iC] = Instantiate(cd.prefab, pos, Quaternion.identity, transform);

            if (iC != 0)
                goCharactersToSelect[iC].SetActive(false);

        }

        isReady = true;
    }

    public void Update()
    {
        if(isReady)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                NextCharacter();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                PreviousCharacter();
            }
            else if (Input.GetKeyDown(KeyCode.KeypadEnter) /*|| Input.GetKeyDown(KeyCode.*/)
            {
                AddSelectedCharacter(AllCharacters[currentSelectedCharacterIndex]);
            }
        }
           
    }

    public void AddSelectedCharacter(CharacterData _characterData)
    {

        // AIAIDIADIAJ
        return;

        GameManager.Instance.selectedCharacters[PlayerInfo.Instance.PlayerIndex].Add(_characterData);
    }

    public void NextCharacter()
    {
        goCharactersToSelect[currentSelectedCharacterIndex].SetActive(false);
        currentSelectedCharacterIndex++;
        if (currentSelectedCharacterIndex >= nbCharacters) currentSelectedCharacterIndex = 0;
        goCharactersToSelect[currentSelectedCharacterIndex].SetActive(true);
    }

    public void PreviousCharacter()
    {
        goCharactersToSelect[currentSelectedCharacterIndex].SetActive(false);
        currentSelectedCharacterIndex--;
        if (currentSelectedCharacterIndex < 0) currentSelectedCharacterIndex = nbCharacters - 1;
        goCharactersToSelect[currentSelectedCharacterIndex].SetActive(true);
    }

}
