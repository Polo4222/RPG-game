using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComabtSceneUIInteractions : MonoBehaviour
{
    public GameObject ItemPanel;
    public GameObject SpellMenu;
    
    public void OnClickShowSpellMenu()
    {
        if(ItemPanel.activeSelf == true)
        {
            ItemPanel.SetActive(false);
        }
        if(SpellMenu.activeSelf == false)
        {
            SpellMenu.SetActive(true);
        }
        else if(SpellMenu.activeSelf == true)
        {
            SpellMenu.SetActive(false);
        }
    }

    public void OnClickShowItemMenu()
    {
        if (SpellMenu.activeSelf == true)
            SpellMenu.SetActive(false);

        if(ItemPanel.activeSelf == false)
        {
            ItemPanel.SetActive(true);
        }
        else if(ItemPanel.activeSelf == true)
        {
            ItemPanel.SetActive(false);
        }
    }
    
}
