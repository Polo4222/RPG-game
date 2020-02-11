using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpellUIItem : MonoBehaviour
{
    public Spell spell;

    public void SetSpell(Spell spell)
    {
        this.spell = spell;
        SetupSpellValues();
    }

    void SetupSpellValues()
    {
        this.transform.FindChild("Spell_Name").GetComponent<TextMeshProUGUI>().text = spell.SpellName;
    }
    
    public void OnSelectSpellButton()
    {
        SpellController.Instance.SetSpellDetails(spell, GetComponent<Button>());
        //Debug.Log("Pretend the spell does something OP");
    }

}
