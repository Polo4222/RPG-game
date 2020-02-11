using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuButtons : MonoBehaviour
{
    public GameObject m_InventoryScreen;
    public GameObject m_ItemScreen;
    public GameObject m_CharacterScreen;
    public GameObject m_QuestScreen;
    public GameObject SpellScreen;
    
    public GameObject m_InventoryButton;
    public GameObject m_ItemButton;
    public GameObject m_CharacterButton;
    public GameObject m_QuestButton;
    public GameObject SpellButton;

    private void Start()
    {
        ESceneChange.Instance.EventTriggerToNonCombatScene();
        EUISetup.Instance.OnUISetup();
    }
    private void Awake()
    {
        ESceneChange.Instance.ESetUpScene += del_SetUpMenusAndButtons;
        EUISetup.Instance.UIHasBeenSetup += del_CloseUI;
    }
    private void OnDisable()
    {
        ESceneChange.Instance.ESetUpScene -= del_SetUpMenusAndButtons;
        EUISetup.Instance.UIHasBeenSetup -= del_CloseUI;
    }

    private void OnDestroy()
    {
        ESceneChange.Instance.ESetUpScene -= del_SetUpMenusAndButtons;
        EUISetup.Instance.UIHasBeenSetup -= del_CloseUI;
    }

    void del_CloseUI()
    {
        m_InventoryScreen.SetActive(false);
        m_ItemScreen.SetActive(false);
        m_CharacterScreen.SetActive(false);
        m_QuestScreen.SetActive(false);
        SpellScreen.SetActive(false);
    }

    void del_SetUpMenusAndButtons(int i)
    {
        
        if (i == 1)
        {
            m_InventoryScreen = GameObject.FindGameObjectWithTag("InventoryMenu");
            m_ItemScreen = GameObject.FindGameObjectWithTag("ItemMenu");
            m_CharacterScreen = GameObject.FindGameObjectWithTag("CharacterMenu");
            m_QuestScreen = GameObject.FindGameObjectWithTag("QuestMenu");
            SpellScreen = GameObject.FindGameObjectWithTag("SpellMenu");
            
            //Debug.Log("1");
            
            //Debug.Log("2");
            m_InventoryButton = GameObject.FindGameObjectWithTag("InventoryButton");
            m_InventoryButton.GetComponent<Button>().onClick.AddListener(delegate { OnClickShowInventory(); });
            m_ItemButton = GameObject.FindGameObjectWithTag("ItemButton");
            m_ItemButton.GetComponent<Button>().onClick.AddListener(delegate { OnClickShowItemScreen(); });
            m_CharacterButton = GameObject.FindGameObjectWithTag("CharacterButton");
            m_CharacterButton.GetComponent<Button>().onClick.AddListener(delegate { OnClickShowCharacterScreen(); });
            m_QuestButton = GameObject.FindGameObjectWithTag("QuestButton");
            m_QuestButton.GetComponent<Button>().onClick.AddListener(delegate { OnClickShowQuestScreen(); });
            SpellButton = GameObject.FindGameObjectWithTag("SpellButton");
            SpellButton.GetComponent<Button>().onClick.AddListener(delegate { OnClickShowSpellScreen(); });
            //Debug.Log("3");
            
        }
    }

    public void OnClickShowSpellScreen()
    {
        if(SpellScreen.activeSelf == false)
        {
            SpellScreen.SetActive(true);
            m_ItemScreen.SetActive(false);
            m_CharacterScreen.SetActive(false);
            m_QuestScreen.SetActive(false);
        }
    }

    public void OnClickShowInventory()
    {
        if(m_InventoryScreen.activeSelf == false)
        {
            m_InventoryScreen.SetActive(true);
            m_ItemScreen.SetActive(true);
            m_CharacterScreen.SetActive(false);
            m_QuestScreen.SetActive(false);
            SpellScreen.SetActive(false);
        }
        else
        {
            m_ItemScreen.SetActive(false);
            m_InventoryScreen.SetActive(false);
        }
    }

    public void OnClickShowItemScreen()
    { 
        if(m_CharacterScreen.activeSelf == true)
        {
            m_CharacterScreen.SetActive(false);
            m_ItemScreen.SetActive(true);
            SpellScreen.SetActive(false);
        }
        else if (m_QuestScreen.activeSelf == true)
        {
            m_QuestScreen.SetActive(false);
            m_ItemScreen.SetActive(true);
            SpellScreen.SetActive(false);
        }
    }

    public void OnClickShowCharacterScreen()
    {
        if (m_ItemScreen.activeSelf == true)
        {
            m_ItemScreen.SetActive(false);
            m_CharacterScreen.SetActive(true);
            SpellScreen.SetActive(false);
        }
        else if (m_QuestScreen.activeSelf == true)
        {
            m_QuestScreen.SetActive(false);
            m_CharacterScreen.SetActive(true);
            SpellScreen.SetActive(false);
        }
    }

    public void OnClickShowQuestScreen()
    {
        if (m_ItemScreen.activeSelf == true)
        {
            m_ItemScreen.SetActive(false);
            m_QuestScreen.SetActive(true);
            SpellScreen.SetActive(false);
        }
        else if (m_CharacterScreen.activeSelf == true)
        {
            m_CharacterScreen.SetActive(false);
            m_QuestScreen.SetActive(true);
            SpellScreen.SetActive(false);
        }
    }
}
