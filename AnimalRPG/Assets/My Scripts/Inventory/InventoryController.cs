using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; set; }
    public PlayerWeaponController playerWeaponController;
    public ConsumableController consumableController;
    public InventoryUIDetails inventoryDetailsPanel;
    //public Item sword;

    public List<Item> playerItems;

    private void Awake()
    {
        playerItems = new List<Item>();
        ESceneChange.Instance.ESetUpScene += del_SetupUI;
        ESetupCombatScene.Instance.OnCombatSceneLoad += del_CombatUIandItemSetup;

    }

    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        playerWeaponController = GetComponent<PlayerWeaponController>();
        consumableController = GetComponent<ConsumableController>();
        

        GiveItem("sword");
        GiveItem("potion_log");

        ESceneChange.Instance.ESetUpScene += SetItemsInInventory;
    }

    void del_SetupUI(int i)
    {
        if (i == 1)
        {
            inventoryDetailsPanel = GameObject.FindGameObjectWithTag("ItemDetails").GetComponent<InventoryUIDetails>();
        }
    }

    void del_CombatUIandItemSetup()
    {
        inventoryDetailsPanel = GameObject.FindGameObjectWithTag("ItemDetails").GetComponent<InventoryUIDetails>();

        if (inventoryDetailsPanel == null)
            Debug.LogWarning("Inventory Details Panel is Null!!!");

        Debug.LogWarning("Setting up Comsubables in Inventory");

        SetConsumablesInInventory();
    }

    void SetItemsInInventory(int i)
    {
        foreach (Item item in playerItems)
        {
            UIEventHandler.ItemAddedToInventory(item);
        }
    }

    public void GiveItem(string itemSlug)
    {
        Item item = ItemDatabase.Instance.GetItem(itemSlug);
        playerItems.Add(item);
        Debug.Log(playerItems.Count + " items in inventory. Added: " + item.ItemName);
        UIEventHandler.ItemAddedToInventory(item);
    }

    public void SetItemDetails(Item item, Button selectedButton)
    {
        inventoryDetailsPanel.SetItem(item, selectedButton);
    }

    public void EquipItem(Item itemToEquip)
    {
        playerWeaponController.EquipWeapon(itemToEquip);

    }

    public List<Item> GetConsumableItems()
    {
        List<Item> consumbableItems = new List<Item>();
        foreach(Item item in playerItems)
        {
            if(item.ItemType == Item.ItemTypes.Consumable)
            {
                consumbableItems.Add(item);
            }
        }

        if (consumbableItems == null)
            return null;

        return consumbableItems;
    }

    void SetConsumablesInInventory()
    {
        foreach(Item item in GetConsumableItems())
        {
            UIEventHandler.ItemAddedToInventory(item);
        }
    }

    public void ConsumeItem(Item itemToConsume)
    {
        consumableController.ConsumeItem(itemToConsume);
    }
}
