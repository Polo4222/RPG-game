using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Targeting : MonoBehaviour
{
    public Button AttackButton;
    public Button SpellButton;
    public Button InventoryButton;
    public Button BackButton;

    [SerializeField]
    GameObject SpellMenu;

    List<Button> ListOfEnemyButtons = new List<Button>();
    List<CombatUIEnemy> ListOfCombatUIEnemy = new List<CombatUIEnemy>();
    List<CharacterSheet> ListOfEnemies = new List<CharacterSheet>();
    public Player player;

    private void Awake()
    {
        player = GameObject.Find("GameManager").GetComponent<Player>();
        AttackButton.onClick.AddListener(delegate { del_OnClickAttackButton(); });
        
        BackButton.gameObject.SetActive(false);
        ECombatEvents.Instance.OnPlayerAction += del_DisablePlayerInteractions;
    }

    private void OnDisable()
    {
        ECombatEvents.Instance.OnPlayerAction -= del_DisablePlayerInteractions;
    }

    private void OnDestroy()
    {
        ECombatEvents.Instance.OnPlayerAction -= del_DisablePlayerInteractions;
    }

    void del_OnClickAttackButton()
    {
        Debug.Log("You are attacking the enemy! WITH YOUR SWORD!");

        for (int i = 0; i < ListOfEnemies.Count; i++)
        {
            if (ListOfEnemyButtons == null)
                Debug.LogWarning("Buttons is null!");
            if (ListOfEnemies == null)
                Debug.LogWarning("Enemies is NULL!");

            Debug.Log(string.Format("For Loop count: {0}", i));

            ListOfEnemyButtons[i].gameObject.SetActive(true);
            ListOfCombatUIEnemy[i].SetCharcterSheetAndPlayerAttack(ListOfEnemies[i], player);
        }
        
        BackButton.gameObject.SetActive(true);
        BackButton.onClick.AddListener(delegate { del_OnClickBackButton(); });
        AttackButton.interactable = false;
    }

    public void del_OnBackButtonForSpells()
    {
        for (int i = 0; i < ListOfEnemyButtons.Count; i++)
        {
            ListOfEnemyButtons[i].onClick.RemoveAllListeners();
            ListOfEnemyButtons[i].gameObject.SetActive(false);
        }
        BackButton.onClick.RemoveAllListeners();
        BackButton.gameObject.SetActive(false);
        SpellMenu.SetActive(true);
    }

    public void del_OnClickBackButton()
    {
        for(int i = 0; i < ListOfEnemyButtons.Count; i++)
        {
            ListOfEnemyButtons[i].onClick.RemoveAllListeners();
            ListOfEnemyButtons[i].gameObject.SetActive(false);
        }
        BackButton.onClick.RemoveAllListeners();
        BackButton.gameObject.SetActive(false);
        AttackButton.interactable = true;
    }

    public void SetEnemyButtonList(List<Button> buttons)
    {
        foreach(Button button in buttons)
        {
            ListOfEnemyButtons.Add(button);
        }
    }

    public void SetEnemysList(List<CharacterSheet> characterSheets)
    {
        ListOfEnemies = characterSheets;
    }

    public void SetEnemyButton(Button button, CombatUIEnemy combatUIEnemy)
    {
        ListOfEnemyButtons.Add(button);
        ListOfCombatUIEnemy.Add(combatUIEnemy);
    }

    public void AllowPlayerToInteract()
    {
        AttackButton.interactable = true;
        SpellButton.interactable = true;
        InventoryButton.interactable = true;
        Debug.Log("It is the player's turn");
    }

    public void SpellTargeting(Spell spell)
    {
        Debug.Log("You are attacking the enemy! WITH YOUR SPEL... MAGIC!");

        for (int i = 0; i < ListOfEnemies.Count; i++)
        {
            if (ListOfEnemyButtons == null)
                Debug.LogWarning("Buttons is null!");
            if (ListOfEnemies == null)
                Debug.LogWarning("Enemies is NULL!");

            Debug.Log(string.Format("For Loop count: {0}", i));

            ListOfEnemyButtons[i].gameObject.SetActive(true);
            ListOfCombatUIEnemy[i].SetCharacterSheetAndPlayerSpell(ListOfEnemies[i], player, spell);
        }

        BackButton.gameObject.SetActive(true);
        BackButton.onClick.AddListener(delegate { del_OnBackButtonForSpells(); });
        AttackButton.interactable = false;
        SpellMenu.SetActive(false);
    }

    public void del_DisablePlayerInteractions()
    {
        for (int i = 0; i < ListOfEnemyButtons.Count; i++)
        {
            ListOfEnemyButtons[i].onClick.RemoveAllListeners();
            ListOfEnemyButtons[i].gameObject.SetActive(false);
        }

        BackButton.onClick.RemoveAllListeners();
        BackButton.gameObject.SetActive(false);

        AttackButton.interactable = false;
        SpellButton.interactable = false;
        InventoryButton.interactable = false;
        Debug.Log("It is the player's turn");
    }

    public void RemoveDeadEnemy(int EnemyDead)
    {
        //ListOfCombatUIEnemy.RemoveAt(EnemyDead);
        //ListOfEnemies.RemoveAt(EnemyDead);
        //ListOfEnemyButtons.RemoveAt(EnemyDead);
        Debug.Log(string.Format("There is {0} entries in the button targeting list", ListOfEnemyButtons.Count));
    }
}