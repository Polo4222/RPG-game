using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Combat : MonoBehaviour
{
    public bool m_bCombatStarted = false;

    bool m_bTurnOrder = false;
    bool m_bJustHadAFight = false;

    float timer = 5;

    GameObject m_playerHealth;
    Slider m_playerHealthBar;
    TextMeshProUGUI m_playerHealthText;

    GameObject m_TurnOrderDis;
    TextMeshProUGUI m_TurnText;

    public Player m_PlayerStats;

    Dialogue m_Dialogue;

    public GameObject m_Player;
    CharacterController m_characterController;

    SaveAndLoad saveAndLoad;

    Vector3 m_EnemyInGameWorld;
    Vector3 m_PlayerWorldLocation;
    Quaternion m_PlayerWorldRotation;

    // Start is called before the first frame update
    void Awake()
    {
        m_Dialogue = GetComponent<Dialogue>();
        saveAndLoad = GetComponent<SaveAndLoad>();
        m_PlayerStats = GetComponent<Player>();
        ECombatStartEnd.Instance.ECombatStarted += del_CombatStarted;
        ECombatEvents.Instance.EnemyDealsDamage += del_PlayerTakenDamage;
    }

    private void OnDisable()
    {
        ECombatStartEnd.Instance.ECombatStarted -= del_CombatStarted;
        ECombatEvents.Instance.EnemyDealsDamage -= del_PlayerTakenDamage;
    }

    private void OnDestroy()
    {
        ECombatStartEnd.Instance.ECombatStarted -= del_CombatStarted;
        ECombatEvents.Instance.EnemyDealsDamage -= del_PlayerTakenDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (m_bJustHadAFight == true && m_Player != null)
        {
            if (m_Player.transform.position != m_PlayerWorldLocation)
            {
                Debug.Log(string.Format("Player Current Location: {0}", m_Player.transform.position));
                Debug.Log("Setting Player Location");
                Debug.Log(string.Format("Player Location: {0}", m_PlayerWorldLocation));
                Debug.Log(string.Format("Player Rotation: {0}", m_PlayerWorldRotation));

                m_characterController.enabled = false;
                m_Player.transform.position = new Vector3(m_PlayerWorldLocation.x, m_PlayerWorldLocation.y, m_PlayerWorldLocation.z);
                m_characterController.enabled = true;
                m_bJustHadAFight = false;
            }
            
        }
        else if (m_bJustHadAFight == true)
        {
            Debug.Log("Finding Player");
            m_Player = GameObject.FindGameObjectWithTag("Player");
            m_characterController = m_Player.GetComponent<CharacterController>();
            //m_Player.transform.SetPositionAndRotation()
        }

        if (m_bCombatStarted == true)
        {
            setUpEnemyHealth();

            TurnBasedCombat();
        }           
    }

    public void PlayerWon()
    {
        CombatStopped();
        SceneManager.LoadScene(1);
       
        m_bJustHadAFight = true;
    }

    void del_PlayerTakenDamage()
    {
        m_playerHealthBar.value = m_PlayerStats.characterStats.stats[3].GetCalculatedStatValue();
    }

    void setUpEnemyHealth()
    {
        if (m_playerHealth == null)
        {
            m_playerHealth = GameObject.FindGameObjectWithTag("PlayerHealth");
            m_playerHealthBar = m_playerHealth.GetComponent<Slider>();
            m_playerHealthText = m_playerHealth.GetComponentInChildren<TextMeshProUGUI>();
            m_playerHealthBar.maxValue = m_PlayerStats.characterStats.stats[3].GetMaxCalculatedStatValue();
            m_playerHealthBar.value = m_PlayerStats.characterStats.stats[3].GetCalculatedStatValue();

            m_TurnOrderDis = GameObject.FindGameObjectWithTag("TurnText");
            m_TurnText = m_TurnOrderDis.GetComponent<TextMeshProUGUI>();
            
        }
    }

    void TurnBasedCombat()
    {
        if (m_bTurnOrder == false)
        {
            m_TurnText.SetText("Your Turn");
           //m_AttackButton.SetActive(true);
        }
        else
        {
            //m_AttackButton.SetActive(false);
            m_TurnText.SetText("Your Opponents Turn");
            //timer = timer - Time.deltaTime;
            //if(timer <= 0)
            //{
            //    m_PlayerStats.PlayerCurrentHealth = m_PlayerStats.PlayerCurrentHealth - 5;
            //    timer = 5;
            //    m_bTurnOrder = false;
            //}
            
        }
    }

    public void CombatStarted(GameObject Enemy, GameObject Player)
    {
        saveAndLoad.Save();
        //ECombatStartEnd.Instance.EventTrigger();
        ESceneChange.Instance.EventTriggerToCombatScene();
        m_EnemyInGameWorld = Enemy.transform.position;
        m_PlayerWorldLocation = Player.transform.position;
        m_PlayerWorldRotation = Player.transform.rotation;
    }

    public void CombatStarted(GameObject Player)
    {
        saveAndLoad.Save();
        ESceneChange.Instance.EventTriggerToCombatScene();
        m_PlayerWorldLocation = Player.transform.position;
        m_PlayerWorldRotation = Player.transform.rotation;
    }

    public void CombatStopped()
    {
        //ECombatStartEnd.Instance.EventTrigger();
        saveAndLoad.Load();
    }

    public bool IsPlayerInRangeOfEnemyJustFoughtWith(GameObject Player)
    {

        if (m_EnemyInGameWorld == null)
            return false;

        float dist;
        dist = Vector3.Distance(Player.transform.position, m_EnemyInGameWorld);

        Debug.Log(dist);
        if (dist <= 90.0f)
            return true;

        return false;
    }

    void del_CombatStarted()
    {
        if (m_bCombatStarted == false)
        {
            m_bCombatStarted = true;
        }
        else if (m_bCombatStarted == true)
        {
            m_bCombatStarted = false;
        }
    }
}