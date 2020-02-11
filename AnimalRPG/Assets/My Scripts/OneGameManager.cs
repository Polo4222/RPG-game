using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameController;
        gameController = GameObject.FindGameObjectsWithTag("GameController");
        if (gameController.Length > 1)
            Destroy(this.gameObject);
        else
            DontDestroyOnLoad(this.gameObject);
        Destroy(this);
    }
}
