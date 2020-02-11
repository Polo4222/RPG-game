using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI health, level;
    [SerializeField] private Image healthFill, levelFill;
    [SerializeField] private Player player;

    GameObject gameManger;

    //Stats
    private List<TextMeshProUGUI> playerStatsTexts = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI playerStatPrefab;
    [SerializeField] private Transform playerStatPanel;

    //Equipped Weapon
    [SerializeField] private Sprite defaultWaponSprite;
    [SerializeField] private PlayerWeaponController playerWeaponController;
    [SerializeField] private TextMeshProUGUI weaponStatPrefab;
    [SerializeField] private Transform weaponStatPanel;
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private Image weaponIcon;
    private List<TextMeshProUGUI> weaponStatTexts = new List<TextMeshProUGUI>();
    
    void Awake()
    {
        UIEventHandler.OnPlayerHealthChanged += UpdateHealth;
        UIEventHandler.OnStatsChange += UpdateStats;
        UIEventHandler.OnItemEquipped += UpdateEquippedWeapon;
        ESceneChange.Instance.ESetUpScene += InitializeStats;

    }
    
    private void OnDisable()
    {
        UIEventHandler.OnPlayerHealthChanged -= UpdateHealth;
        UIEventHandler.OnStatsChange -= UpdateStats;
        ESceneChange.Instance.ESetUpScene -= InitializeStats;
    }

    private void OnDestroy()
    {
        UIEventHandler.OnPlayerHealthChanged -= UpdateHealth;
        UIEventHandler.OnStatsChange -= UpdateStats;
        ESceneChange.Instance.ESetUpScene -= InitializeStats;
    }
    private void Start()
    {
        gameManger = GameObject.FindGameObjectWithTag("GameController");
        playerWeaponController = gameManger.GetComponent<PlayerWeaponController>();
    }

    void UpdateHealth(int currentHealth, int maxHealth)
    {
        this.health.text = currentHealth.ToString();
        this.healthFill.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    void InitializeStats(int x)
    {
        //Need to implement the new stat code for below to work
        for (int i = 0; i < player.characterStats.stats.Count; i++)
        {
            playerStatsTexts.Add(Instantiate(playerStatPrefab));
            playerStatsTexts[i].transform.SetParent(playerStatPanel);
        }

        UpdateStats();
        Debug.Log("Setting up stats");
    }

    void UpdateStats()
    {
        for (int i = 0; i < player.characterStats.stats.Count; i++)
        {
            playerStatsTexts[i].text = player.characterStats.stats[i].StatName + ": " + player.characterStats.stats[i].GetCalculatedStatValue().ToString();
            
        }
    }

    void UpdateEquippedWeapon(Item item)
    {
        Debug.Log(item.ObjectSlug);
        weaponIcon.sprite = Resources.Load<Sprite>(item.ObjectSlug);
        weaponNameText.text = item.ItemName;


        //Need to implement the new stat code for below to work
        for (int i = 0; i < item.Stats.Count; i++)
        {
            weaponStatTexts.Add(Instantiate(weaponStatPrefab));
            weaponStatTexts[i].transform.SetParent(weaponStatPanel);
            weaponStatTexts[i].text = item.Stats[i].StatName+ ": " + item.Stats[i].GetCalculatedStatValue().ToString();
        }
        
        Debug.Log("Setting up stats");
    }

    public void UnequipWeapon()
    {
        weaponNameText.text = "---";
        weaponIcon.sprite = defaultWaponSprite;

        for(int i = 0; i < weaponStatTexts.Count; i++)
        {
            Destroy(weaponStatTexts[i].gameObject);
        }
        weaponStatTexts.Clear();
        playerWeaponController.UnequipWeapon();
    }
}
