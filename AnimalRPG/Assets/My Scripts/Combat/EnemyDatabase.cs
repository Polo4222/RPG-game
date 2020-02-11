using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class EnemyDatabase : MonoBehaviour
{
    #region Singleton

    private static EnemyDatabase _instance;
    public static EnemyDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EnemyDatabase();
            }

            return _instance;
        }
    }

    #endregion

    List<CharacterSheet> TypesOfEnemies { get; set; }

    private void Awake()
    {
        BuildDatabase();
    }

    private void BuildDatabase()
    {
        TypesOfEnemies = JsonConvert.DeserializeObject<List<CharacterSheet>>(Resources.Load<TextAsset>("JSON/Enemies").ToString());
    }

    public CharacterSheet GetEnemyCharacter(string characterSlug)
    {
        foreach(CharacterSheet character in TypesOfEnemies)
        {
            if (character.CharacterSlug == characterSlug)
            {
                return character.DeepCopy();
            }
        
        }

        Debug.LogWarning("That enemy does not exist! Check Spelling!");
        return null;
    }

    public int GetNumberOfEnemiesInDatabase()
    {
        int i = 0;
        foreach(CharacterSheet characterSheet in TypesOfEnemies)
        {
            i++;
        }
        return i;
    }
}
