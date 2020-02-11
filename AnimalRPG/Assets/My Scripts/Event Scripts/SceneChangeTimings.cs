using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTimings : MonoBehaviour
{
    string path;
    private string NonCombatScenesPath = "Assets/Scenes/OverworldScenes/";
    private int characterNumber = 30;
    private int amountOfCharactersToCut;

    private void Start()
    {
        SceneManager.sceneLoaded += del_SceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= del_SceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= del_SceneLoaded;
    }

    void del_SceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Debug.Log(string.Format("Scene file path: {0}", scene.path));
        if(ScenePathCheck(scene))
        {
            ESceneChange.Instance.EventTriggerToNonCombatScene();
            ECombatStartEnd.Instance.EventTrigger();
            EUISetup.Instance.OnUISetup();
            EQuestConditionDone.Instance.EventTrigger();
        }
        else
        {
            ECombatStartEnd.Instance.EventTrigger();
            ESetupCombatScene.Instance.EventTrigger();
            EUISetup.Instance.OnCombatUISetup();
        }
        Debug.Log("Scene Loaded");
    }

    bool ScenePathCheck(Scene scene)
    {
        path = scene.path;

        path = path.Remove(characterNumber);
        Debug.Log(string.Format("The shortened scene path: {0}", path));
        if (path.Equals(NonCombatScenesPath)) 
            return true;

        return false;
    }
}
