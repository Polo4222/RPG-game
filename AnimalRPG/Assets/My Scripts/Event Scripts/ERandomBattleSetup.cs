using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ERandomBattleSetup
{
    #region singleton
    private static ERandomBattleSetup _instance;
    private static readonly object padlock = new object();
    public static ERandomBattleSetup instance
    {
        get
        {
            lock(padlock)
            {
                if(_instance == null)
                {
                    _instance = new ERandomBattleSetup();
                }
                return _instance;
            }
        }
    }

    #endregion

    public event Action OnRandomBattleEncounterSetup;

    public void TriggerEvent(Scene scene, LoadSceneMode sceneMode)
    {
        if(OnRandomBattleEncounterSetup == null)
        {
            Debug.LogWarning("On Random Battle Encounter event is null! Nothing is assigned to it!");
        }
        else
        {
            OnRandomBattleEncounterSetup();
        }
        
        RemoveEvent();
    }

    private void RemoveEvent()
    {
        SceneManager.sceneLoaded -= TriggerEvent;
    }
}
