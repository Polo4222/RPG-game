using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    public int EnemyID { get; set; }

    public KillGoal(int enemyID, string description, bool completed, int currentAmount, int amountRequired)
    {
        this.EnemyID = enemyID;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = amountRequired;
    }

    public override void init()
    {
        base.init();
        
    }

    void EnemyDied(IEnemy enemy)
    {
        if(enemy.EnemyID == this.EnemyID)
        {
            this.CurrentAmount++;
            Evaluate();
        }
    }
}
