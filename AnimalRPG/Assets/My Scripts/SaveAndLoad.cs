using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    [SerializeField]
    GameObject m_player;

    static readonly string SAVE_PLAYER_FILE = "player.json";

    string filename;

    public SavePlayerData LoadedPlayerData;

    private void Update()
    {
        if (m_player == null)
        {
            m_player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    public void Save()
    {
        if(m_player == null)
        {
            m_player = GameObject.FindGameObjectWithTag("Player");
        }
        
        SavePlayerData savePlayerData = new SavePlayerData() { playerPosition = m_player.transform.position, playerRotation = m_player.transform.rotation };

        string json = JsonUtility.ToJson(savePlayerData);

        filename = Path.Combine(Application.persistentDataPath, SAVE_PLAYER_FILE);

        if (File.Exists(filename))
        {
            File.Delete(filename);
        }

        File.WriteAllText(filename, json);

        Debug.Log(string.Format("File saved to: {0}", filename));
    }

    public void Load()
    {
        if (filename == null)
        {
            filename = Path.Combine(Application.persistentDataPath, SAVE_PLAYER_FILE);
        }

        if (!File.Exists(filename))
        {
            Debug.LogError("Warning: Tried to load a file that does not exist!");
            return;
        }

        string jsonFromFile = File.ReadAllText(filename);

        LoadedPlayerData = JsonUtility.FromJson<SavePlayerData>(jsonFromFile);
        Debug.Log(string.Format("Player load position: {0}  Player load rotation: {1}", LoadedPlayerData.playerPosition, LoadedPlayerData.playerRotation));
    }
}
