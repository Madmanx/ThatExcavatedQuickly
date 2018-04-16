using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    [SerializeField]
    public string name;

    [SerializeField]
    public GameObject prefab;
}

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Database", order = 1)]
public class CharactersDatabase : ScriptableObject {

    [SerializeField]
    List<CharacterData> characters;

    public List<CharacterData> Characters
    {
        get
        {
            return characters;
        }
    }

    public void ResetAll()
    {
        characters = new List<CharacterData>();

        // Adding colors
        int idCharacter = 0;
        string[] strNames = { "Dweler", "Gunner" };
        Characters.Add(new CharacterData { name = strNames[idCharacter], prefab = Resources.Load<GameObject>("DwelerCharacter") as GameObject });
        Characters.Add(new CharacterData { name = strNames[++idCharacter], prefab = Resources.Load<GameObject>("GunnerCharacter") as GameObject });

    }
}
