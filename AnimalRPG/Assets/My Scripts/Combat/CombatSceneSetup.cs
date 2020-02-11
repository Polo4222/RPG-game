using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatSceneSetup : MonoBehaviour
{
    public GameObject FirstEnemyLocation;
    public GameObject SecondEnemyLocation;
    public GameObject ThirdEnemyLocation;
    public GameObject EnemySelectionPanel;

    public Slider PlayerHealth;
    [SerializeField]
    Slider PlayerMana;

    public List<string> EnemyToBeSpawned;           //Use the Character Slug name.
    List<GameObject> Enemies = new List<GameObject>();
    List<GameObject> ListOfEnemyUIInfo = new List<GameObject>();

    List<TextMeshProUGUI> EnemyNames = new List<TextMeshProUGUI>();
    List<TextMeshProUGUI> EnemyHealths = new List<TextMeshProUGUI>();
    List<Slider> Sliders = new List<Slider>();

    List<CharacterSheet> PossibleEnemiesInScene = new List<CharacterSheet>();

    CombatUI combatUI;
    Combat combat;
    Targeting targeting;
    CombatTurnOrder combatTurnOrder;
    GameObject gameManager;
    EnemyDatabase enemyDatabase;

    public RectTransform EnemyPanel;
    Object EnemyPanelPrefab;

    int m_AmountOfEnemies;

    private void Awake()
    {
        targeting = GetComponent<Targeting>();
        combatTurnOrder = GetComponent<CombatTurnOrder>();
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        if (gameManager == null)
            Debug.LogWarning("Scene has loaded before game manager has loaded!");
        enemyDatabase = gameManager.GetComponent<EnemyDatabase>();
        combat = gameManager.GetComponent<Combat>();
        EnemyPanelPrefab = Resources.Load<Object>("Prefabs/UI/Enemy_Info");

        combatUI = GetComponent<CombatUI>();

        Enemies.Add(FirstEnemyLocation);
        Enemies.Add(SecondEnemyLocation);
        Enemies.Add(ThirdEnemyLocation);
        m_AmountOfEnemies = (int)Random.Range(1.0f, 3.9f);
        Debug.Log(string.Format("The amount of enemies there should be {0}", m_AmountOfEnemies));

        GetTheEnemiesFromDatabase();

        SetupEnemyInfoUIandEnemyModels();

        SetPlayerMana();

        ECombatEvents.Instance.OnPlayerAction += del_UpdateMana;
    }

    // Start is called before the first frame update
    void Start()
    {
        combatUI.SetEnemySelection(m_AmountOfEnemies, Enemies);
        ECombatEvents.Instance.EnemyDamageTaken += del_UpdateEnemyInfoUI;
        ECombatEvents.Instance.EnemyDies += del_DeadEnemy;
        ECombatEvents.Instance.TriggerChangeTurn();
    }

    private void OnDestroy()
    {
        ECombatEvents.Instance.EnemyDamageTaken -= del_UpdateEnemyInfoUI;
        ECombatEvents.Instance.EnemyDies -= del_DeadEnemy;
        ECombatEvents.Instance.OnPlayerAction -= del_UpdateMana;
    }

    private void OnDisable()
    {
        ECombatEvents.Instance.EnemyDamageTaken -= del_UpdateEnemyInfoUI;
        ECombatEvents.Instance.EnemyDies -= del_DeadEnemy;
        ECombatEvents.Instance.OnPlayerAction -= del_UpdateMana;
    }

    void del_UpdateEnemyInfoUI()
    {
        for (int i = 0; i < EnemyNames.Count; i++)
        {
            EnemyHealths[i].text = Sliders[i].maxValue + "/" + PossibleEnemiesInScene[i].characterStats.stats[3].GetCalculatedStatValue();
            Sliders[i].value = PossibleEnemiesInScene[i].characterStats.stats[3].GetCalculatedStatValue();
        }
    }

    void SetupEnemyInfoUIandEnemyModels()
    {
        for (int i = 0; i < m_AmountOfEnemies; i++)
        {
            GameObject _GameObject = (GameObject)Instantiate(Resources.Load<Object>("Prefabs/Models/Enemies/" + PossibleEnemiesInScene[i].CharacterModel), Enemies[i].transform);
            _GameObject.name = PossibleEnemiesInScene[i].CharacterName + i.ToString();
            Enemies[i] = _GameObject;

            GameObject gameObject = (GameObject)Instantiate(EnemyPanelPrefab);
            gameObject.transform.SetParent(EnemyPanel);
            EnemyNames.Add(gameObject.transform.FindChild("EnemyName").GetComponent<TextMeshProUGUI>());
            EnemyNames[i].text = PossibleEnemiesInScene[i].CharacterName;

            Sliders.Add(gameObject.transform.FindChild("EnemyHealthBar").GetComponent<Slider>());

            EnemyHealths.Add(Sliders[i].transform.FindChild("EnemyHealthNumber").GetComponent<TextMeshProUGUI>());

            int EnemyHealthNumber = PossibleEnemiesInScene[i].characterStats.stats[3].BaseValue;

            EnemyHealths[i].text = EnemyHealthNumber + "/" + EnemyHealthNumber;
            Sliders[i].maxValue = EnemyHealthNumber;
            Sliders[i].value = EnemyHealthNumber;

            ListOfEnemyUIInfo.Add(gameObject);
        }
    }

    void GetTheEnemiesFromDatabase()
    {
        for (int i = 0; i < m_AmountOfEnemies; i++)
        {
            int a = (int)Random.Range(0.0f, enemyDatabase.GetNumberOfEnemiesInDatabase());

            Debug.Log(string.Format("The random output for which enemy type is selected: {0}", a));

            PossibleEnemiesInScene.Add(enemyDatabase.GetEnemyCharacter(EnemyToBeSpawned[a]));
        }

        targeting.SetEnemysList(PossibleEnemiesInScene);
        combatTurnOrder.setEnemies(PossibleEnemiesInScene);
    }

    void SetPlayerMana()
    {
        PlayerMana.maxValue = combat.m_PlayerStats.characterStats.stats[4].GetMaxCalculatedStatValue();
        PlayerMana.value = combat.m_PlayerStats.characterStats.stats[4].GetCalculatedStatValue();
    }

    void del_UpdateMana()
    {
        PlayerMana.value = combat.m_PlayerStats.characterStats.stats[4].GetCalculatedStatValue();
    }

    void del_DeadEnemy()
    {
        for(int i = 0; i < PossibleEnemiesInScene.Count; i++)
        {
            if(PossibleEnemiesInScene[i].characterStats.stats[3].GetCalculatedStatValue() <= 0 && Enemies[i].gameObject.activeSelf == true)
            {
                //Remove the model
                Enemies[i].gameObject.SetActive(false);
                Enemies.RemoveAt(i);

                //Remove it from the UI
                ListOfEnemyUIInfo[i].SetActive(false);
                ListOfEnemyUIInfo.RemoveAt(i);
                EnemyNames.RemoveAt(i);
                EnemyHealths.RemoveAt(i);
                Sliders.RemoveAt(i);

                //Maybe Not needed?
                targeting.RemoveDeadEnemy(i);
                Debug.Log(string.Format("Enemies list now has {0} entries in it", Enemies.Count));

                //Remove character that has died.
                PossibleEnemiesInScene.RemoveAt(i);
            }
        }
        if (PossibleEnemiesInScene.Count == 0)
            CombatWin();
    }

    void CombatWin()
    {
        combat.PlayerWon();
        Debug.Log("You killed all the enemies...... Well done Serial Killer");
    }
}
