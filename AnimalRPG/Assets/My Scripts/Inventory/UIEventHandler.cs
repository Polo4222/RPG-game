using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHandler : MonoBehaviour
{
    public delegate void ItemEventHandler(Item item);
    public static event ItemEventHandler OnItemAddedToInventory;
    public static event ItemEventHandler OnItemEquipped;

    public delegate void PlayerHealthEventHandler(int currentHealth, int maxHealth);
    public static event PlayerHealthEventHandler OnPlayerHealthChanged;

    public delegate void StatsChangeEventHandler();
    public static event StatsChangeEventHandler OnStatsChange;

    public delegate void PlayerLevelEventHandler();
    public static event PlayerLevelEventHandler OnPlayerLevelUp;

    public static void ItemAddedToInventory(Item item)
    {
        Debug.Log(item.ItemName);
        if (OnItemAddedToInventory == null)
        {
            Debug.Log("The event for Item added to inventory is null!");
        }
        else
        {
            OnItemAddedToInventory(item);
        }

    }

    public static void ItemEquipped(Item item)
    {
        if (OnItemEquipped == null)
        {
            Debug.Log("The event for Item to be equiped is null");
        }
        else
        {
            OnItemEquipped(item);
        }
        
    }

    public static void HealthChanged(int currentHealth, int maxHealth)
    {
        if (OnPlayerHealthChanged == null)
        {
            Debug.Log("The event for player health changing is null");
        }
        else
        {
            OnPlayerHealthChanged(currentHealth, maxHealth);
        }
        
    }

    public static void StatsChanged()
    {
        if (OnStatsChange == null)
        {
            Debug.Log("The event for Stats changing is null");
        }
        else
        {
            OnStatsChange();
        }
       
    }

    public static void PlayerLevelUp()
    {
        if (OnPlayerLevelUp == null)
        {
            Debug.Log("The event for Item to be equiped is null");
        }
        else
        {
            OnPlayerLevelUp();
        }
        
    }
}
