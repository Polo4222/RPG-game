using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    public int PlayerHealth = 100;
    public int PlayerMana = 100;

    public int PlayerCurrentHealth = 100;
    public int PlayerCurrentMana = 75;

    public GameObject m_PlayerHealth;
    public GameObject m_PlayerMana;
    public Slider m_PlayerHealthBar;
    public Slider m_PlayerManaBar;
    public TextMeshProUGUI m_PlayerHealthNumber;
    public TextMeshProUGUI m_PlayerManaNumber;

    private void Awake()
    {
        ESceneChange.Instance.ESetUpScene += Playerbars;
    }

    private void OnDisable()
    {
        ESceneChange.Instance.ESetUpScene -= Playerbars;
    }

    private void OnDestroy()
    {
        ESceneChange.Instance.ESetUpScene -= Playerbars;
    }
    // Update is called once per frame
    void Update()
    {
        m_PlayerHealthBar.value = PlayerCurrentHealth;
        m_PlayerManaBar.value = PlayerCurrentMana;
        //this
        if (m_PlayerHealthNumber != null)
        {
            m_PlayerHealthNumber.SetText(PlayerCurrentHealth.ToString() + "/" + PlayerHealth.ToString());
            m_PlayerManaNumber.SetText(PlayerCurrentMana.ToString() + "/" + PlayerMana.ToString());
        }
    }

    void Playerbars(int i)
    {
        if(m_PlayerHealth == null)
        {
            m_PlayerHealth = GameObject.FindGameObjectWithTag("PlayerHealth");
            m_PlayerHealthBar = m_PlayerHealth.GetComponent<Slider>();
            m_PlayerHealthNumber = m_PlayerHealth.GetComponentInChildren<TextMeshProUGUI>();
            m_PlayerHealthBar.maxValue = PlayerHealth;
        }
       
        if (m_PlayerMana == null)
        {
            m_PlayerMana = GameObject.FindGameObjectWithTag("PlayerMana");      
            m_PlayerManaBar = m_PlayerMana.GetComponent<Slider>();       
            m_PlayerManaNumber = m_PlayerMana.GetComponentInChildren<TextMeshProUGUI>();
            m_PlayerManaBar.maxValue = PlayerMana;
        }
        
    }
}
