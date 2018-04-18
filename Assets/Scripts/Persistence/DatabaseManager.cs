using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour {

    private static DatabaseManager singleton;

    public static DatabaseManager Instance
    {
        get
        {
            return singleton;
        }
    }

    public CharactersDatabase CharactersDb
    {
        get
        {
            if(!charactersDb) charactersDb = Resources.Load<CharactersDatabase>("Data") as CharactersDatabase;
            return charactersDb;
        }
    }

    private CharactersDatabase charactersDb;

    public void Awake()
    {
        singleton = this;
    }
}
