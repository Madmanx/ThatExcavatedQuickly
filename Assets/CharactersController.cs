using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharactersController : NetworkBehaviour {

    //public delegate void UICharacterControlledRefresh();
    //public static event UICharacterControlledRefresh OnCharacterControlledRefresh;

    public List<GameObject> possessedCharacters = new List<GameObject>();

    private static CharactersController singleton;

    public void Awake()
    {
        singleton = this;
    }

    public int currentPossessedCharacter = 0;

    public static CharactersController Instance
    {
        get
        {
            return singleton;
        }

        set
        {
            singleton = value;
        }
    }

    public void Update()
    {
        if( GameManager.currentState == GameState.Ingame)
        {
            if (GameManager.currentTurn == (EntityTurn)PlayerInfo.Instance.playerIndex)
            {
                // My Controls
                if(Input.GetKeyDown(KeyCode.Tab))
                {
                    ControlNextCharacter();
                }

                // My Controls
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ControlNextCharacter();
                }
            }

        }
    }
    private void ControlNextCharacter()
    {
        currentPossessedCharacter++;
        if (currentPossessedCharacter > possessedCharacters.Count) currentPossessedCharacter = 0;
    }

    public void Shoot()
    {
        possessedCharacters[currentPossessedCharacter].GetComponent<NetworkPawnController>().CmdDoFire();
    }

    public void Init()
    {
        for (int i = 0; i < CharactersManager.Instance.SelectedCharacters.Length; i++)
        {
            GameObject go = Instantiate(CharactersManager.Instance.SelectedCharacters[i].networkPrefab, transform.position, Quaternion.identity) as GameObject;
            possessedCharacters.Add(go);
        }
        PlayerInfo.Instance.CmdAssignation();
    }

    [Command]
    public void CmdAssignation()
    {
        for (int i = 0; i < possessedCharacters.Count; i++)
        {
            NetworkServer.SpawnWithClientAuthority(possessedCharacters[i], PlayerInfo.Instance.gameObject);
        }
    }

    //public void Init()
    //{
    //    int[] ids = new int[CharactersManager.Instance.SelectedCharacters.Length];
    //    for (int i = 0; i < ids.Length; i++)
    //    {
    //        ids[i] = DatabaseManager.Instance.CharactersDb.GetIdFromCharacterData(CharactersManager.Instance.SelectedCharacters[i]);
    //    }

    //    CmdCopyCharactersSelectedToPossessedCharacters(ids);
    //}


    //[Command]
    //public void CmdCopyCharactersSelectedToPossessedCharacters(int[] selectedCharacters)
    //{
    //    for (int i = 0; i < selectedCharacters.Length; i++)
    //    {
    //        CharacterData cd = DatabaseManager.Instance.CharactersDb.GetCharacterDataFromId(selectedCharacters[i]);

    //        GameObject go = Instantiate(cd.networkPrefab, transform.position, Quaternion.identity) as GameObject;

    //        Debug.Log(PlayerInfo.Instance.connectionToClient);
    //        NetworkServer.SpawnWithClientAuthority(go, PlayerInfo.Instance.connectionToClient);

    //        Debug.Log(possessedCharacters.Count);
    //        possessedCharacters.Add(go);
    //        // Ref to characters controller
    //    }
    //}

    //[ClientRpc]
    //public void RpcAssign(GameObject go)
    //{
    //    possessedCharacters.Add(go);
    //}

}
