using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    void GoToScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }

    public void ChangeScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }
}
