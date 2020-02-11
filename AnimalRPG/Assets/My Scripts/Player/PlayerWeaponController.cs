using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject _Player;
    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }

    Item currentlyEquippedItem;
    IWeapon equippedWeapon;
    Player CharacterStats;
    void Awake()
    {

        ESceneChange.Instance.ESetUpScene += SetSearches;
    }

    void SetSearches(int i)
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        CharacterStats = _Player.GetComponent<Player>();
        playerHand = GameObject.FindGameObjectWithTag("PlayerHand");
    }

    public void EquipWeapon(Item itemToEquip)
    {
        if(EquippedWeapon != null)
        {
            CharacterStats.characterStats.RemoveStatBonus(EquippedWeapon.GetComponent<IWeapon>().Stats);
            Destroy(playerHand.transform.GetChild(0).gameObject);
        }

        currentlyEquippedItem = itemToEquip;
        EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug), playerHand.transform.position, playerHand.transform.rotation);
        equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();
        equippedWeapon.Stats = itemToEquip.Stats;
        EquippedWeapon.transform.SetParent(playerHand.transform);
        CharacterStats.characterStats.AddStatBonus(itemToEquip.Stats);
        UIEventHandler.ItemEquipped(itemToEquip);
        UIEventHandler.StatsChanged();
    }

    public void UnequipWeapon()
    {
        InventoryController.Instance.GiveItem(currentlyEquippedItem.ObjectSlug);
        CharacterStats.characterStats.RemoveStatBonus(equippedWeapon.Stats);
        Destroy(EquippedWeapon.transform.gameObject);
    }

    public void PerformAttack()
    {
        equippedWeapon.PerformAttack();
    }
}
