using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    [SerializeField]
    public string name;


}

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Database", order = 1)]
public class Database : ScriptableObject {

    [SerializeField]
    List<CharacterData> characters;


    public void ResetAll()
    {
        characters = new List<CharacterData>();

        // Adding colors
        int idCharacter = 0;
        string[] strNames = { "Jean", "Bob" };
        characters.Add(new CharacterData { name = strNames[idCharacter] });
        characters.Add(new CharacterData { name = strNames[++idCharacter] });

    }
}
