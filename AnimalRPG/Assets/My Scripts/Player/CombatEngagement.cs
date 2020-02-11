using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CombatEngagement : MonoBehaviour
{
    GameObject _GameManager;
    Combat _Combat;
    float _Timer;
    public float Timer_CHANGEABLE;
    public float ChancePercentage_CHANGEABLE;

    private void Start()
    {
        _Timer = Timer_CHANGEABLE;
    }

    private void Update()
    {
        if (_GameManager == null)
        {
            _GameManager = GameObject.FindGameObjectWithTag("GameController");
            _Combat = _GameManager.GetComponent<Combat>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log(CombatManager.Instance.IsPlayerInRangeOfEnemyJustFoughtWith(this.gameObject));
            if (_Combat.IsPlayerInRangeOfEnemyJustFoughtWith(this.gameObject) == false)
            {
                _Combat.CombatStarted(other.gameObject, this.gameObject);
                Debug.Log("We are figthing");
                ChangeToFightScene(2);
            }
            else
            {
                Debug.Log("No one here to fight");
                other.gameObject.SetActive(false);
                //EQuestConditionDone.Instance.EventTrigger();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "RandomCombatEncounter")
        {
            //Debug.Log("In Random Combat area");
            _Timer = _Timer - Time.deltaTime;
            if(_Timer <= 0)
            {
                if(ChancePercentage_CHANGEABLE >= Random.Range(0.0f, 100.0f))
                {
                    _Timer = Timer_CHANGEABLE;
                    _Combat.CombatStarted(this.gameObject);
                    ChangeToRandomFightScene(3);
                }
            }
        }
    }

    void ChangeToFightScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }

    void ChangeToRandomFightScene(int SceneNumber)
    {
        SceneManager.sceneLoaded += ERandomBattleSetup.instance.TriggerEvent;
        SceneManager.LoadScene(SceneNumber);
    }
}