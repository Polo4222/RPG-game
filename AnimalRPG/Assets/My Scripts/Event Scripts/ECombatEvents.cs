using System;
using System.Collections;
using System.Collections.Generic;


public class ECombatEvents
{
    #region singleton
    private static ECombatEvents _instance;
    private static readonly object padlock = new object();

    public static ECombatEvents Instance
    {
        get
        {
            lock(padlock)
            {
                if (_instance == null)
                {
                    _instance = new ECombatEvents();
                }
            return _instance;
            }
        }
    }
    #endregion

    public event Action OnPlayerAction;
    public event Action ChangeTurn;

    public event Action EnemyDamageTaken;   //Enemy has taken damage

    public event Action EnemyDealsDamage;   //Enemy is dealing damage

    public event Action EnemyDies;      //Possible to pass in the enemy that dies so it can easily be seached an removed from necessary places. 

    public void TriggerOnPlayerAction()
    {
        OnPlayerAction();
    }

    public void TriggerChangeTurn()
    {
        ChangeTurn();
    }

    public void TriggerEnemyDamageDealt()
    {
        EnemyDamageTaken();
    }

    public void TriggerEnemyDealsDamage()
    {
        EnemyDealsDamage();
    }

    public void TriggerEnemyDies()
    {
        EnemyDies();
    }
}
