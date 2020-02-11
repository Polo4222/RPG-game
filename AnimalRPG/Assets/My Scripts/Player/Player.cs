using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter
{
    public CharacterStats characterStats;
    public int currentHealth;
    public int maxHealth;
    public int maxMana;

    // Start is called before the first frame update
    void Awake()
    {     
        characterStats = new CharacterStats(20, 10, 10, maxHealth, maxMana, Spell.ClassSpell.Warrior, 100);
    }

    private void Start()
    {
        this.currentHealth = this.maxHealth;
        UIEventHandler.HealthChanged(characterStats.stats[3].GetCalculatedStatValue(), characterStats.stats[3].GetMaxCalculatedStatValue());
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }

    public void Die()
    {
        Debug.Log("Player is dead.");
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }
}
