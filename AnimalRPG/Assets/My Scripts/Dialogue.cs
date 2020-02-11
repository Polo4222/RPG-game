using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.AI;

public class Dialogue : MonoBehaviour
{
    GameObject m_TheFuckingCamera;
    Quest m_QuestComponent;
    DialogueSentences m_CurrentDialogue;
    Camera cam;

    TextMeshProUGUI m_TextBox;
    TextMeshProUGUI m_CharacterNameBox;
    GameObject m_dialogueBox;
    GameObject m_dialogueButtonNEXT;
    NavMeshAgent m_navMeshAgent;
    bool m_bInCombat = false;

    private Queue<int> m_QuestCharacterNames;
    private Queue<string> m_QuestDialogue;

    [SerializeField]
    string[] CharacterNames;
    // Start is called before the first frame update
    void Start()
    {
        m_QuestComponent = GetComponent<Quest>();
        m_QuestDialogue = new Queue<string>();
        m_QuestCharacterNames = new Queue<int>();
        ECombatStartEnd.Instance.ECombatStarted += del_CombatStarted;
        //ERandomBattleSetup.instance.OnRandomBattleEncounterSetup += del_CombatStarted;
    }

    private void OnDisable()
    {
        ECombatStartEnd.Instance.ECombatStarted -= del_CombatStarted;
        //ERandomBattleSetup.instance.OnRandomBattleEncounterSetup -= del_CombatStarted;
    }

    private void OnDestroy()
    {
        ECombatStartEnd.Instance.ECombatStarted -= del_CombatStarted;
        //ERandomBattleSetup.instance.OnRandomBattleEncounterSetup -= del_CombatStarted;
    }

    // Update is called once per frame
    void Update()
    {  

       if(m_bInCombat == false)
       {
           if(cam == null)
           {
               m_TheFuckingCamera = GameObject.FindGameObjectWithTag("MainCamera");
               cam = m_TheFuckingCamera.GetComponent<Camera>();
           }

           SetUpDialogueWindowsAndButtons();

           if(Input.GetMouseButtonDown(0))
           {
               Talk();
           }
       }
        
    }

    void Talk()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log(string.Format("The collider was {0}", hit.collider));
            if (hit.collider.gameObject.tag == "NPC")
            {
                Debug.Log("Having a conversation. Fuck Off");
                m_CurrentDialogue = hit.collider.gameObject.GetComponent<DialogueSentences>();
                DialougeTalking();
            }
            else if (hit.collider.gameObject.tag == "Interactable")
            {
                hit.collider.gameObject.GetComponent<Interactable>().MoveToInteraction(m_navMeshAgent);
            }
        }
    }

    //this
    void SetUpDialogueWindowsAndButtons()
    {
        if (m_dialogueBox == null)
        {
            m_dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");

            m_dialogueButtonNEXT = GameObject.FindGameObjectWithTag("DialogueNext");
            m_dialogueButtonNEXT.GetComponent<Button>().onClick.AddListener(delegate { OnClickDisplayNextDialogue(); });

            m_TextBox = GameObject.FindGameObjectWithTag("DBox").GetComponent<TextMeshProUGUI>();
            m_CharacterNameBox = GameObject.FindGameObjectWithTag("CharacterDName").GetComponent<TextMeshProUGUI>();
            m_dialogueBox.SetActive(false);
        }
    }

    void DialougeTalking()
    {
        int[] DialougeToSay = new int[2];
        m_dialogueBox.SetActive(true);
        //m_TextBox.text = m_QuestComponent.QuestDialogue();
        DialougeToSay = m_QuestComponent.QuestDialogue();
        
        m_QuestDialogue.Clear();

        for (int i = DialougeToSay[0];i <= DialougeToSay[1]; i++)
        {
            m_QuestCharacterNames.Enqueue(m_CurrentDialogue.Name[i]);
            m_QuestDialogue.Enqueue(m_CurrentDialogue.Sentences[i]);
            //Debug.LogWarning(m_QuestCharacterNames.Count);
            //Debug.LogWarning(m_CurrentDialogue.Sentences[i]);
        }
        CharacterNames = m_CurrentDialogue.CharacterNames;
        m_TextBox.text = m_QuestDialogue.Dequeue();
        m_CharacterNameBox.text = NameToDisplay();
        m_QuestCharacterNames.Dequeue();

        m_QuestComponent.QuestConditionFullFilled();
        EQuestConditionDone.Instance.EventTrigger();
    }

    public void OnClickDisplayNextDialogue()
    {
        if (m_QuestDialogue.Count == 0)
        {
            m_dialogueBox.SetActive(false);
        }
        Debug.LogWarning(m_QuestCharacterNames.Peek());
        Debug.LogWarning(m_QuestDialogue.Peek());
        m_TextBox.text = m_QuestDialogue.Dequeue();
        m_CharacterNameBox.text = NameToDisplay();
        m_QuestCharacterNames.Dequeue();
    }

    string NameToDisplay()
    {
        if (m_QuestCharacterNames.Peek() == 0)
        {
            return CharacterNames[0];
        }
        if (m_QuestCharacterNames.Peek() == 1)
        {
            return CharacterNames[1];
        }

        return "ERROR";
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
