using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class SpellDatabase : MonoBehaviour
{
    #region Singleton

    private static SpellDatabase _instance;
    public static SpellDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SpellDatabase();
            }

            return _instance;
        }
    }

    #endregion

    private List<Spell> Spells { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        BuildDatabase();
    }

    private void BuildDatabase()
    {
        Spells = JsonConvert.DeserializeObject<List<Spell>>(Resources.Load<TextAsset>("JSON/Spell").ToString());
    }

    public Spell GetSpell(string spellName, int PlayerLevel)
    {
        foreach(Spell spell in Spells)
        {
            if(spell.SpellName == spellName && PlayerLevel >= spell.LevelRequirement)
            {
                return spell;
            }
        }
        Debug.LogWarning("Couldn't find the spell " + spellName);
        return null;
    }

    public List<Spell> GetListOfSpells()
    {
        return Spells;
    }
}
