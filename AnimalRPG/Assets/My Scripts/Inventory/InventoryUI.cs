using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public RectTransform inventoryPanel;
    public RectTransform scrollViewContent;
    InventoryUIItem itemContainer { get; set; }
    bool menuIsActive { get; set; }
    Item currentSelected { get; set; }

    private void Awake()
    {
        ESceneChange.Instance.ESetUpScene += del_SetupUI;
        ECombatStartEnd.Instance.ECombatStarted += del_SetupCombatUI;
        UIEventHandler.OnItemAddedToInventory += ItemAdded;

        itemContainer = Resources.Load<InventoryUIItem>("Prefabs/UI/Item_Container");
        if(itemContainer == null)
        {
            Debug.Log("Failed to Load Asset Correctly");
        }
    }

    void del_SetupCombatUI()
    {
        inventoryPanel = (RectTransform)GameObject.FindGameObjectWithTag("ItemMenu").transform;
        scrollViewContent = (RectTransform)GameObject.FindGameObjectWithTag("Content").transform;
    }

    void del_SetupUI(int i)
    {
        if (i == 1)
        {
            inventoryPanel = (RectTransform)GameObject.FindGameObjectWithTag("ItemMenu").transform;
            scrollViewContent = (RectTransform)GameObject.FindGameObjectWithTag("Content").transform;
        }
    }

    public void ItemAdded(Item item)
    {
        InventoryUIItem emptyItem = Instantiate(itemContainer);
        emptyItem.SetItem(item);
        emptyItem.transform.SetParent(scrollViewContent);
    }

    private void OnDestroy()
    {
        UIEventHandler.OnItemAddedToInventory -= ItemAdded;
        ESceneChange.Instance.ESetUpScene -= del_SetupUI;
    }

    private void OnDisable()
    {
        UIEventHandler.OnItemAddedToInventory -= ItemAdded;
        ESceneChange.Instance.ESetUpScene -= del_SetupUI;
    }

}
