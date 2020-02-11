using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpellMenuFinishedSetup : MonoBehaviour
{

    GameObject _ComsumableMenu;
    GameObject _SpellMenu;


    // Start is called before the first frame update
    void Start()
    {
        ESetupCombatScene.Instance.OnCombatSceneLoad += del_SetupCombatMenus;
        EUISetup.Instance.CombatUIHasBeenSetup += del_CloseMenus;
    }

    private void OnDisable()
    {
        ESetupCombatScene.Instance.OnCombatSceneLoad -= del_SetupCombatMenus;
        EUISetup.Instance.CombatUIHasBeenSetup -= del_CloseMenus;
    }

    private void OnDestroy()
    {
        ESetupCombatScene.Instance.OnCombatSceneLoad -= del_SetupCombatMenus;
        EUISetup.Instance.CombatUIHasBeenSetup -= del_CloseMenus;
    }

    void del_SetupCombatMenus()
    {
        _ComsumableMenu = GameObject.FindGameObjectWithTag("ItemMenu");
        _SpellMenu = GameObject.FindGameObjectWithTag("SpellMenu");
    }

    void del_CloseMenus()
    {
        _ComsumableMenu.SetActive(false);
        _SpellMenu.SetActive(false);
    }
}
