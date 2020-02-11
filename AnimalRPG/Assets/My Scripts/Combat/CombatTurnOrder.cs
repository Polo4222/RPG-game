using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTurnOrder : MonoBehaviour
{
    enum TurnType { Player, Enemy, Ally }
    List<TurnType> TurnOrder = new List<TurnType>();
    List<CharacterSheet> Enemies = new List<CharacterSheet>();
    List<CharacterSheet> Allies = new List<CharacterSheet>();
    Player player;
    Combat combat; 
    Targeting targeting;

    private void Awake()
    {
        targeting = GetComponent<Targeting>();
        player = GameObject.Find("GameManager").GetComponent<Player>();
        combat = GameObject.Find("GameManager").GetComponent<Combat>();
        ECombatEvents.Instance.ChangeTurn += NextTurn;
        ECombatEvents.Instance.EnemyDies += del_EnemyDead;
    }

    private void OnDestroy()
    {
        ECombatEvents.Instance.ChangeTurn -= NextTurn;
        ECombatEvents.Instance.EnemyDies -= del_EnemyDead;
    }

    private void OnDisable()
    {
        ECombatEvents.Instance.ChangeTurn -= NextTurn;
        ECombatEvents.Instance.EnemyDies -= del_EnemyDead;
    }

    public void setEnemies(List<CharacterSheet> characterSheets)
    {
        Enemies = characterSheets;
        CreateTurnOrder();
    }

    void CreateTurnOrder()
    {
        Debug.Log("Creating new turn order");
        TurnOrder.Add(TurnType.Player);
        for(int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].HasCharacterTakenTurn = false;
            TurnOrder.Add(TurnType.Enemy);
        }
    }

    void WinLoseCondition()
    {
        if (Enemies.Count == 0)
            Win();              //Player Wins

        if (player.characterStats.stats[3].GetCalculatedStatValue() <= 0 && Allies.Count == 0)
            Lose();
    }

    void Win()
    {
        combat.PlayerWon();
    }

    void Lose()
    {
        //Implement when the player loses
    }

    void NextTurn()
    {
        //WinLoseCondition();

        if (TurnOrder.Count == 0)
            CreateTurnOrder();

        Debug.Log(string.Format("Next turn, there is {0} number of turns left. {1} is taking their turn", TurnOrder.Count, TurnOrder[0]));
        switch(TurnOrder[0])
        {
            case TurnType.Player:
                TurnOrder.RemoveAt(0);
                targeting.AllowPlayerToInteract();
                
                Debug.Log("Player's Turn");
                break;

            case TurnType.Enemy:
                Debug.Log("Enemy's Turn");
                TurnOrder.RemoveAt(0);
                EnemyAI.Instance.EnemyTurn(Enemies, player);
                break;

            case TurnType.Ally:
                TurnOrder.RemoveAt(0);
                break;

            default:
                CreateTurnOrder();
                Debug.Log("Creating a new turn order");
                break;
        }
    }
    
    void del_EnemyDead()
    {
        for(int i = 0; i < Enemies.Count; i++)
        {
            if(Enemies[i].characterStats.stats[3].GetCalculatedStatValue() <= 0)
            {
                int index = TurnOrder.LastIndexOf(TurnType.Enemy);
                TurnOrder.RemoveAt(index);
            }
        }
    }
}
