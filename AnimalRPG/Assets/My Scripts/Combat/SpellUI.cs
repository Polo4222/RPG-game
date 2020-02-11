using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUI : MonoBehaviour
{
    public RectTransform spellPanel;
    public RectTransform scrollViewContent;

    SpellUIItem spellContent;
    SpellController spellController;

    private void Awake()
    {
        ECombatStartEnd.Instance.ECombatStarted += del_SetupSpellsCombat;

        spellContent = Resources.Load<SpellUIItem>("Prefabs/UI/Spell_Container");
        if(spellContent == null)
        {
            Debug.LogError("Failed to load spell asset");
        }
        spellController = GetComponent<SpellController>();
    }

    void del_SetupSpellsCombat()
    {
        spellPanel = (RectTransform)GameObject.FindGameObjectWithTag("SpellMenu").transform;
        scrollViewContent = (RectTransform)GameObject.FindGameObjectWithTag("SpellContent").transform;

        foreach(Spell spell in spellController.GetAvailableSpells())
        {
            SetSpell(spell);
        }
    }

    public void SetSpell(Spell spell)
    {
        SpellUIItem emptySpell = Instantiate(spellContent);
        emptySpell.SetSpell(spell);
        emptySpell.transform.SetParent(scrollViewContent);
    }

    private void OnDestroy()
    {
        ECombatStartEnd.Instance.ECombatStarted -= del_SetupSpellsCombat;
    }

    private void OnDisable()
    {
        ECombatStartEnd.Instance.ECombatStarted -= del_SetupSpellsCombat;
    }
}
