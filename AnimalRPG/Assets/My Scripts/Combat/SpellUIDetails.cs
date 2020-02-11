using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellUIDetails : MonoBehaviour
{
    Spell spell;
    Button selectedSpellButton, spellInteractButton;
    TextMeshProUGUI spellNameText, spellDescription, spellInteractButtonText;
    Targeting targeting;

    // Start is called before the first frame update
    void Start()
    {
        spellNameText = transform.FindChild("Spell_Name").GetComponent<TextMeshProUGUI>();
        spellDescription = transform.FindChild("Spell_Description").GetComponent<TextMeshProUGUI>();
        spellInteractButton = transform.FindChild("Interact_Button").GetComponent<Button>();
        spellInteractButtonText = spellInteractButton.transform.FindChild("Text (TMP)").GetComponent<TextMeshProUGUI>();
        targeting = GameObject.Find("CombatManager").GetComponent<Targeting>();
    }

    public void setSpell(Spell _spell, Button _selectedButton)
    {
        spellInteractButton.onClick.RemoveAllListeners();
        this.spell = _spell;
        selectedSpellButton = _selectedButton;
        spellNameText.text = spell.SpellName;
        spellDescription.text = spell.Description;
        spellInteractButtonText.text = "Cast";

        //Check if the player has the mana to cast spell
        if (targeting.player.characterStats.stats[4].GetCalculatedStatValue() < spell.ManaCost)
            spellInteractButton.interactable = false;
        else if (spellInteractButton.interactable == false)
            spellInteractButton.interactable = true;

        spellInteractButton.onClick.AddListener(delegate { OnSpellCast(); });
    }

    public void OnSpellCast()
    {
        Debug.Log("Spell is cast, does something OP!");
        targeting.SpellTargeting(spell);

    }
}
