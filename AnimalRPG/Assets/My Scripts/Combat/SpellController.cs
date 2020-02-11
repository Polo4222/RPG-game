using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellController : MonoBehaviour
{
    public static SpellController Instance { get; set; }
    public SpellUIDetails spellUIDetails;

    List<Spell> playerSpells;
    List<Spell> currentKnownSpells;

    SpellDatabase database;
    Player player;

    private void Awake()
    {
        playerSpells = new List<Spell>();
        currentKnownSpells = new List<Spell>();
        ECombatStartEnd.Instance.ECombatStarted += del_SetupUICombat;

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        player = GetComponent<Player>();
        database = GetComponent<SpellDatabase>();
        
    }

    private void Start()
    {
        SetPlayerSpellList();
    }

    void del_SetupUICombat()
    {
        spellUIDetails = GameObject.FindGameObjectWithTag("SpellDetails").GetComponent<SpellUIDetails>();
    }

    void SetPlayerSpellList()
    {
        //Set what spells the player can use
        foreach (Spell spell in database.GetListOfSpells())
        {
            if (spell.TypeOfClassSpell == player.characterStats.CharacterClass || spell.TypeOfClassSpell == Spell.ClassSpell.All)
            {
                playerSpells.Add(spell);
            }
        }
    }

    public List<Spell> GetAvailableSpells()
    {
        UpdatePlayerSpellList();

        return currentKnownSpells;
    }

    void UpdatePlayerSpellList()
    {
        if (currentKnownSpells != null)
            currentKnownSpells.Clear();

        foreach(Spell spell in playerSpells)
        {
            if(spell.LevelRequirement <= player.characterStats.Level)
            {
                currentKnownSpells.Add(spell);
            }
        }
    }

    public void SetSpellDetails(Spell spell, Button button)
    {
        spellUIDetails.setSpell(spell, button);
    }
}
