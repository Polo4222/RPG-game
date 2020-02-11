using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI 
{
    #region singleton
    private static EnemyAI _instance;
    private static object padlock = new object();

    public static EnemyAI Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new EnemyAI();
            }
            return _instance;
        }
    }
    #endregion

    public void EnemyTurn(List<CharacterSheet> Enemies, Player player)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].HasCharacterTakenTurn == false)
            {   
                
                Enemy(Enemies[i], player);
                return;
            }
                
        }
    }

    public void Enemy(CharacterSheet Enemy)
    {                                
               
    }                                
                                     
    public void Enemy(CharacterSheet Enemy, Player player)
    {
        //Insert Randomiser for the AI to choose which different attack. At the moment is it just basic attacking;
        EnemyBasicAttack(Enemy, player);
    }                                
                                     
    public void Enemy(CharacterSheet Enemy, Player player, List<CharacterSheet> Allies)
    {

    }

    void EnemyBasicAttack(CharacterSheet Enemy, Player player)
    {
        Enemy.HasCharacterTakenTurn = true;
        player.characterStats.stats[3].BaseValue -= Enemy.characterStats.stats[0].GetCalculatedStatValue() - player.characterStats.stats[1].GetCalculatedStatValue();
        ECombatEvents.Instance.TriggerEnemyDealsDamage();
        ECombatEvents.Instance.TriggerChangeTurn();
    }

    void EnemyBasicAttack(CharacterSheet Enemy, CharacterSheet Ally)
    {

    }
}
