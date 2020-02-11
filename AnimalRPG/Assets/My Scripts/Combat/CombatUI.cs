using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    public RectTransform EnemySelectionPanel;
    Object enemyTarget { get; set; }
    public List<Button> EnemyButtons = new List<Button>();
    Targeting targeting;

    private void Awake()
    {
        targeting = GetComponent<Targeting>();
        enemyTarget = Resources.Load<Object>("Prefabs/UI/Enemy_Button");
        if (enemyTarget == null)
            Debug.Log("Enemy button asset not loaded!");
    }

    public void SetEnemySelection(int i, List<GameObject> enemyPos)
    {
        for(int x = 0; x < i; x++)
        {
            GameObject emptyTarget = (GameObject)Instantiate(enemyTarget);
            emptyTarget.GetComponent<CombatUIEnemy>().SetEnemy(enemyPos[x], x);
            emptyTarget.transform.SetParent(EnemySelectionPanel);
            targeting.SetEnemyButton(emptyTarget.GetComponent<Button>(), emptyTarget.GetComponent<CombatUIEnemy>());
            emptyTarget.SetActive(false);
        }
        //targeting.SetEnemyButtonList(EnemyButtons);
    }
}
