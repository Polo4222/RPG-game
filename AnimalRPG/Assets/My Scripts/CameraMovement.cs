using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    GameObject m_Player;
    Quaternion quat;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        quat.x = 75;
        quat.y = 0;
        quat.z = 0;
        quat.w = 180;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        {
            Vector3 pos = m_Player.transform.position;
            pos.y = pos.y + 140;
            pos.z = pos.z - 140;
            gameObject.transform.position = pos;

            gameObject.transform.rotation = quat;
        }
    }
}
