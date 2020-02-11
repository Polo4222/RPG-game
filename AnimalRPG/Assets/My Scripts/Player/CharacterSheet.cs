using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class CharacterSheet : ICharacter
{
    public string CharacterName { get; set; }
    public string CharacterSlug { get; set; }
    public string CharacterModel { get; set; }
    public bool HasCharacterTakenTurn; 
    public CharacterStats characterStats;
    public Spell.ClassSpell CharacterClass { get; set; }
    List<Spell> CharacterSpells = new List<Spell>();

    [Newtonsoft.Json.JsonConstructor]
    CharacterSheet(int power, int toughness, int attackSpeed, int health, int mana, Spell.ClassSpell _class, int level, string characterName, string characterSlug, string characterModel)
    {
        characterStats = new CharacterStats(power, toughness, attackSpeed, health, mana, _class, level);
        CharacterName = characterName;
        CharacterSlug = characterSlug;
        CharacterModel = characterModel;
    }

    public CharacterSheet ShallowCopy()
    {
        return (CharacterSheet) this.MemberwiseClone();
    }

    public CharacterSheet DeepCopy()
    {
        CharacterSheet deepCopy = (CharacterSheet)this.MemberwiseClone();
        deepCopy.characterStats = new CharacterStats( characterStats.stats[0].GetCalculatedStatValue(), characterStats.stats[1].GetCalculatedStatValue(), characterStats.stats[2].GetCalculatedStatValue(), characterStats.stats[3].GetCalculatedStatValue(), characterStats.stats[4].GetCalculatedStatValue(), characterStats.CharacterClass, characterStats.Level);
        deepCopy.CharacterName = string.Copy(CharacterName);
        deepCopy.CharacterSlug = string.Copy(CharacterSlug);
        deepCopy.CharacterModel = string.Copy(CharacterModel);
        deepCopy.CharacterClass = CharacterClass;
        deepCopy.CharacterSpells = CharacterSpells;
        return deepCopy;
    }

    public void SetSpell(List<Spell> spells)
    {
        foreach(Spell spell in spells)
        {
            if(spell.TypeOfClassSpell == CharacterClass || spell.TypeOfClassSpell == Spell.ClassSpell.All)
                if(characterStats.Level >= spell.LevelRequirement)
                    CharacterSpells.Add(spell);
        }
    }

    public void TakeDamage(int damage)
    {
        int FinalDamageTaken = 0;
        FinalDamageTaken = damage - characterStats.stats[1].GetCalculatedStatValue();
        characterStats.stats[3].BaseValue -= FinalDamageTaken;

        ECombatEvents.Instance.TriggerEnemyDamageDealt();

        if (characterStats.stats[3].GetCalculatedStatValue() <= 0)
            Die();
    }

    public void TakeDamage(Spell spell, int SpellPower)
    {
        float FinalDamageTaken = 0;
        FinalDamageTaken =  SpellPower * (spell.Stats[0].GetCalculatedStatValue() / 100);
        characterStats.stats[3].BaseValue -= (int)FinalDamageTaken;

        ECombatEvents.Instance.TriggerEnemyDamageDealt();

        if (characterStats.stats[3].GetCalculatedStatValue() <= 0)
            Die();
    }

    public void Die()
    {
        //Dies
        ECombatEvents.Instance.TriggerEnemyDies();
    }
}
