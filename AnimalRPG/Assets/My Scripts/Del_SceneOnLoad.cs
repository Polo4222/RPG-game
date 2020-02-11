using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Del_SceneOnLoad : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelIsFinishedLoading;
    }

    void OnLevelIsFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelIsFinishedLoading;
    }
}
