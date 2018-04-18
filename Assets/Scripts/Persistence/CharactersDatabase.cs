using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    [SerializeField]
    public int id;

    [SerializeField]
    public string name;

    [SerializeField]
    public GameObject prefab;

    [SerializeField]
    public GameObject networkPrefab;

    [SerializeField]
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Database", order = 1)]
public class CharactersDatabase : ScriptableObject {

    [SerializeField]
    List<CharacterData> characters;

    private Sprite[] allSprite;

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
        allSprite = Resources.LoadAll<Sprite>("Worms_image");


        // Adding colors
        int idCharacter = 0;
        string[] strNames = { "Dweler", "Gunner" };
        Characters.Add(new CharacterData { id = idCharacter,  name = strNames[idCharacter], networkPrefab = Resources.Load<GameObject>("NetDwelerCharacter"), prefab = Resources.Load<GameObject>("DwelerCharacter") as GameObject, sprite= allSprite[8] as Sprite });
        Characters.Add(new CharacterData { id = ++idCharacter, name = strNames[idCharacter], networkPrefab = Resources.Load<GameObject>("NetGunnerCharacter"), prefab = Resources.Load<GameObject>("GunnerCharacter") as GameObject, sprite = allSprite[13] as Sprite });

    }

    public int GetIdFromCharacterData(CharacterData _c)
    {
        return characters.Find(a => a == _c).id;
    }

    public CharacterData GetCharacterDataFromId(int id)
    {
        return characters.Find(a => a.id == id);
    }
}
