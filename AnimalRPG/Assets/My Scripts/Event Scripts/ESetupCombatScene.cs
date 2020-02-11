using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESetupCombatScene 
{
    #region singleton

    private static ESetupCombatScene _instance;
    private static readonly object padlock = new object();

    public static ESetupCombatScene Instance
    {
        get
        {
            lock(padlock)
            {
                if(_instance == null)
                {
                    _instance = new ESetupCombatScene();
                }
                return _instance;
            }
        }
    }

    #endregion

    public event Action OnCombatSceneLoad;


    public void EventTrigger()
    {
        OnCombatSceneLoad();
    }
}
