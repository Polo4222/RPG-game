using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public GameObject m_Blocker;
    public GameObject m_Thot;

    //List<string> m_QuestDialogue;

    private Queue<string> m_QuestDialogue;

    bool m_bHasChrisGotHisQuest = false;
    public bool m_bHasThotBeenSmited = false;
    private bool m_bInCombat = false;

    private void Awake()
    {
        EQuestConditionDone.Instance.EQuestConditionCheck += del_QuestCondition;
        ESceneChange.Instance.ESetUpScene += del_SetupSearches;
        ECombatStartEnd.Instance.ECombatStarted += del_CombatStarted;
    }

    private void OnDisable()
    {
        EQuestConditionDone.Instance.EQuestConditionCheck -= del_QuestCondition;
        ESceneChange.Instance.ESetUpScene -= del_SetupSearches;
        ECombatStartEnd.Instance.ECombatStarted -= del_CombatStarted;
    }

    private void OnDestroy()
    {
        EQuestConditionDone.Instance.EQuestConditionCheck -= del_QuestCondition;
        ESceneChange.Instance.ESetUpScene -= del_SetupSearches;
        ECombatStartEnd.Instance.ECombatStarted -= del_CombatStarted;
    }

    private void LateUpdate()
    {
        if (m_bInCombat == false)
        {
            del_QuestCondition();
        }
    }

    void del_SetupSearches(int i)
    {
        if(i == 1)
        {
            if (m_Blocker == null)
            {
                m_Blocker = GameObject.FindGameObjectWithTag("Blocker");
            }
            if (m_Thot == null)
            {
                m_Thot = GameObject.FindGameObjectWithTag("Enemy");
            }
        }
    }

    void del_QuestCondition()
    {
        //this
        if(m_bHasChrisGotHisQuest == true && m_Blocker.activeSelf == true)
        {
            m_Blocker.SetActive(false);
        }
        if (m_bHasChrisGotHisQuest == true && m_Thot.activeSelf == false)
        {
            m_bHasThotBeenSmited = true;
        }
    }

    public int[] QuestDialogue()
    {
        int[] DialogueToSay = new int[2];
        if (m_bHasChrisGotHisQuest == false)
        {
            DialogueToSay.SetValue(0, 0);
            DialogueToSay.SetValue(5, 1);
            return DialogueToSay;
        }
        else if (m_bHasThotBeenSmited == true)
        {
            Debug.Log("Convo about killing chan");
            DialogueToSay.SetValue(6, 0);
            DialogueToSay.SetValue(9, 1);
            return DialogueToSay;
        }
        DialogueToSay.SetValue(-1, 0);
        DialogueToSay.SetValue(-1, 1);
        return DialogueToSay;
    }

    public void QuestConditionFullFilled()
    {
        Debug.Log("Convo is bestie");
        m_bHasChrisGotHisQuest = true;
    }

    void del_CombatStarted()
    {
        if (m_bInCombat == false)
        {
            Debug.Log("Combat Started");
            m_bInCombat = true;
        }
        else if (m_bInCombat == true)
        {
            m_bInCombat = false;
        }
    }
}