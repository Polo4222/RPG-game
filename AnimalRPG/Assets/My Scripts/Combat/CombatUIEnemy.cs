using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CombatUIEnemy : MonoBehaviour
{
    public GameObject Enemy;
    CharacterSheet characterSheet;
    Player player;
    Spell spell;

    public void SetEnemy(GameObject enemy, int i)
    {
        this.Enemy = enemy;
        SetupEnemyTarget(i);
    }

    void SetupEnemyTarget(int i)
    {
        this.transform.FindChild("Enemy_Name").GetComponent<TextMeshProUGUI>().text = "Enemy" + i.ToString();
    }

    public void SetCharcterSheetAndPlayerAttack(CharacterSheet character, Player _player)
    {
        characterSheet = character;
        player = _player;
        this.gameObject.GetComponent<Button>().onClick.AddListener(delegate { del_DealDamage(); });

        Debug.LogWarning(string.Format("Setting Listener on this button {0}", this.gameObject.name));
    }

    public void SetCharacterSheetAndPlayerSpell(CharacterSheet character, Player _player, Spell spell)
    {
        characterSheet = character;
        player = _player;
        this.spell = spell;
        this.gameObject.GetComponent<Button>().onClick.AddListener(delegate { del_DealSpellDamage(); });
    }

    public void del_DealSpellDamage()
    {
        player.characterStats.stats[4].BaseValue -= spell.ManaCost;
        characterSheet.TakeDamage(spell, player.characterStats.stats[0].GetCalculatedStatValue());
        ECombatEvents.Instance.TriggerOnPlayerAction();
        ECombatEvents.Instance.TriggerEnemyDamageDealt();
        ECombatEvents.Instance.TriggerChangeTurn();
        Debug.Log("Changing Turn");
    }

    public void del_DealDamage()
    {
        Debug.Log(string.Format("Deal damage to enemy, Dealing {0}", player.characterStats.stats[0].GetCalculatedStatValue() - characterSheet.characterStats.stats[1].GetCalculatedStatValue()));
        characterSheet.TakeDamage(player.characterStats.stats[0].GetCalculatedStatValue());
        ECombatEvents.Instance.TriggerOnPlayerAction();
        ECombatEvents.Instance.TriggerEnemyDamageDealt();
        ECombatEvents.Instance.TriggerChangeTurn();
        Debug.Log("Changing Turn");
    }
}
