using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    public List<BaseStat> stats = new List<BaseStat>();
    public Spell.ClassSpell CharacterClass { get; set; }
    public int Level;

    public CharacterStats(int power, int toughness, int Speed, int health, Spell.ClassSpell _class, int level)
    {
        stats = new List<BaseStat>()
        {
            new BaseStat(BaseStat.BaseStatType.Power, power, "Power"),
            new BaseStat(BaseStat.BaseStatType.Toughness, toughness, "Toughness"),
            new BaseStat(BaseStat.BaseStatType.Speed, Speed, "Speed"),
            new BaseStat(BaseStat.BaseStatType.Health, health, "Health")
        };
        CharacterClass = _class;
        Level = level;
    }

    public CharacterStats(int power, int toughness, int Speed, int health, int mana, Spell.ClassSpell _class, int level)
    {
        stats = new List<BaseStat>()
        {
            new BaseStat(BaseStat.BaseStatType.Power, power, "Power"),
            new BaseStat(BaseStat.BaseStatType.Toughness, toughness, "Toughness"),
            new BaseStat(BaseStat.BaseStatType.Speed, Speed, "Speed"),
            new BaseStat(BaseStat.BaseStatType.Health, health, "Health"),
            new BaseStat(BaseStat.BaseStatType.Mana, mana, "Mana")
        };
        CharacterClass = _class;
        Level = level;
    }

    public BaseStat GetStat(BaseStat.BaseStatType stat)
    {
        return this.stats.Find(x => x.StatType == stat);
    }

    public void AddStatBonus(List<BaseStat> statBonuses)
    {
        foreach(BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).AddStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }

    public void RemoveStatBonus(List<BaseStat> statBonuses)
    {
        foreach (BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).RemoveStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }
}
