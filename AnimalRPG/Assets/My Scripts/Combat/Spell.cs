using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Spell 
{
    public enum ClassSpell { Warrior, Mage, Rogue, All}
    public enum SpellTarget { Enemy, Ally, Self, All}
    public enum SpellType { Healing, Frost, Fire, AcidicDeathSlime, Lighting, Cure, Buffing, Debuffing}
    public SpellType TypeOfSpell { get; set; }
    public ClassSpell TypeOfClassSpell { get; set; }
    public SpellTarget TypeOfTarget { get; set; }
    public int NumberOfTargets { get; set; }
    public string Description { get; set; }
    public int ManaCost { get; set; }
    public string SpellName { get; set; }
    public bool SpellModifier { get; set; }
    public List<BaseStat> Stats { get; set; }
    public bool IsSpellLearned { get; set; }
    public int LevelRequirement { get; set; }

    [Newtonsoft.Json.JsonConstructor]
    public Spell(SpellType _spellType, ClassSpell _typeOfClassSpell, SpellTarget _spellTarget, int _numberOfTargets, string _description, int _manaCost, string _spellName, bool _spellModifier, List<BaseStat> _stats, bool _isSpellLearned, int _levelSpellIsLearned)
    {
        this.TypeOfSpell = _spellType;
        this.TypeOfClassSpell = _typeOfClassSpell;
        this.TypeOfTarget = _spellTarget;
        this.NumberOfTargets = _numberOfTargets;
        this.Description = _description;
        this.ManaCost = _manaCost;
        this.SpellName = _spellName;
        this.SpellModifier = _spellModifier;
        this.Stats = _stats;
        this.IsSpellLearned = _isSpellLearned;
        this.LevelRequirement = _levelSpellIsLearned;
    }

}