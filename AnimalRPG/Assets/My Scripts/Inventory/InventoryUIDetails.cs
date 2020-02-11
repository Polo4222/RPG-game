using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUIDetails : MonoBehaviour
{
    Item item;
    Button selectedItemButton, itemInteractButton;
    TextMeshProUGUI itemNameText, itemDescription, itemInteractButtonText;
    private void Start()
    {
        itemNameText = transform.FindChild("Item_Name").GetComponent<TextMeshProUGUI>();
        itemDescription = transform.FindChild("Item_Description").GetComponent<TextMeshProUGUI>();
        itemInteractButton = transform.FindChild("Interact_Button").GetComponent<Button>();
        itemInteractButtonText = itemInteractButton.transform.FindChild("Text (TMP)").GetComponent<TextMeshProUGUI>();
    }
    public void SetItem(Item item, Button selectedButton)
    {
        itemInteractButton.onClick.RemoveAllListeners();
        this.item = item;
        selectedItemButton = selectedButton;
        itemNameText.text = item.ItemName;
        itemDescription.text = item.Description;
        itemInteractButtonText.text = item.ActionName;
        itemInteractButton.onClick.AddListener(OnItemInteract);
    }

    public void OnItemInteract()
    {
        if (item.ItemType == Item.ItemTypes.Consumable)
        {
            InventoryController.Instance.ConsumeItem(item);
            Destroy(selectedItemButton.gameObject);
        }
        else if (item.ItemType == Item.ItemTypes.Weapon)
        {
            InventoryController.Instance.EquipItem(item);
            Destroy(selectedItemButton.gameObject);
        }
    }
}
