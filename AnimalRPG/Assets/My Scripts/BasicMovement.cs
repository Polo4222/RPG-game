using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    float m_MoveSpeed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        float MoveZ = Input.GetAxis("Vertical");
        float MoveX = Input.GetAxis("Horizontal");

        Vector3 currentRot = new Vector3(0.0f, 0.0f, 0.0f);
        if(MoveX != 0 && MoveZ != 0)
        {
            currentRot.y = currentRot.y + (MoveX - MoveZ);
        }
       
        transform.Rotate(currentRot);
        transform.Translate((m_MoveSpeed * MoveX) * Time.deltaTime, 0, (m_MoveSpeed * MoveZ) * Time.deltaTime);
    }
}
