using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
public class BaseStat
{
    public enum BaseStatType { Power, Toughness, Speed, SpellPower, Health, SpellSpeed, Mana}

    public List<StatBonus> BaseAdditives { get; set; }
    public List<StatDebuff> BaseDebuffs { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public BaseStatType StatType { get; set; }
    public int BaseValue { get; set; }
    public int StartValue { get; set; }
    public string StatName { get; set; }
    public string StatDescription { get; set; }
    public int FinalValue { get; set; }

    public BaseStat(int baseValue, string statName, string statDescription)
    {
        this.BaseAdditives = new List<StatBonus>();
        this.BaseDebuffs = new List<StatDebuff>(); 
        this.BaseValue = baseValue;
        this.StatName = statName;
        this.StatDescription = statDescription;
    }

    [Newtonsoft.Json.JsonConstructor]
    public BaseStat(BaseStatType statType, int baseValue, string statName)
    {
        this.BaseAdditives = new List<StatBonus>();
        this.BaseDebuffs = new List<StatDebuff>();      
        this.StatType = statType;
        this.BaseValue = baseValue;
        this.StatName = statName;
        this.StartValue = baseValue;
    }

    public void AddStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Add(statBonus);
    }

    public void RemoveStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Remove(BaseAdditives.Find(x => x.BonusValue == statBonus.BonusValue));
    }

    public int GetCalculatedStatValue()
    {
        this.FinalValue = 0;
        this.BaseAdditives.ForEach(x => this.FinalValue += x.BonusValue);
        this.BaseDebuffs.ForEach(y => this.FinalValue -= y.DebuffValue);        
        FinalValue += BaseValue;
        return FinalValue;
    }

    public int GetMaxCalculatedStatValue()
    {
        this.FinalValue = 0;
        this.BaseAdditives.ForEach(x => this.FinalValue += x.BonusValue);
        FinalValue += StartValue;
        return FinalValue;
    }
}
