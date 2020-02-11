using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController playerController;

    public float m_Speed_CHANGEABLE = 8;
    public float m_TurnSpeed_CHANGEABLE = 150;
    private Vector3 m_MoveDirection;
    private Quaternion m_CurrentRotation;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(playerController.isGrounded)
        //{
            m_MoveDirection = new Vector3(Input.GetAxis("Horizontal"), (-9.81f * Time.deltaTime), Input.GetAxis("Vertical"));
            m_MoveDirection *= m_Speed_CHANGEABLE;

        //}
        //else
        //{
        //    m_MoveDirection = new Vector3(0.0f, (-9.81f * Time.deltaTime), 0.0f);
        //}

        ChangeDirection();
       
        playerController.Move(m_MoveDirection * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        //Debug.Log(string.Format("Player's current rotation: {0}", transform.rotation.y));
        //Debug.Log(string.Format("Vertical Input: {0}", Input.GetAxis("Vertical")));
        //Debug.Log(string.Format("Horizontal Input: {0}", Input.GetAxis("Horizontal")));
        m_CurrentRotation = transform.rotation;

        #region NSEWMovement
        if(Input.GetAxis("Horizontal") <= 0.3f && Input.GetAxis("Horizontal") >= -0.3f && Input.GetAxis("Vertical") >= 0.3f)        //Forwards
        {
            if (m_CurrentRotation.eulerAngles.y <= 359 || m_CurrentRotation.eulerAngles.y >= 1)
            {
                if(m_CurrentRotation.eulerAngles.y >= 180)
                {
                    //Debug.LogWarning("True");
                    TurnRight();
                }
                else
                {
                    //Debug.LogWarning("True");
                    TurnLeft();
                }
            }
        }
        else if (Input.GetAxis("Horizontal") <= 0.3f && Input.GetAxis("Horizontal") >= -0.3f && Input.GetAxis("Vertical") <= -0.3f)             //Backwards
        {
            if(m_CurrentRotation.eulerAngles.y >= 181 || m_CurrentRotation.eulerAngles.y <= 179)
            {
                if(m_CurrentRotation.eulerAngles.y <= 359 && m_CurrentRotation.eulerAngles.y >= 181)
                {
                    //Debug.LogWarning("True");
                    TurnLeft();
                }
                else
                {
                    //Debug.LogWarning("True");
                    TurnRight();
                }
            }
        }

         if (Input.GetAxis("Vertical") <= 0.3f && Input.GetAxis("Vertical") >= -0.3f && Input.GetAxis("Horizontal") >= 0.3f)         //Right
         {
             if(m_CurrentRotation.eulerAngles.y <= 89 || m_CurrentRotation.eulerAngles.y >= 91)
             {
                 if(m_CurrentRotation.eulerAngles.y >= 270 || m_CurrentRotation.eulerAngles.y >= 0 && m_CurrentRotation.eulerAngles.y <= 89)
                 {
                    //Debug.LogWarning("True");
                    TurnRight();
                 }
                 else
                 {
                    //Debug.LogWarning("True");
                    TurnLeft();
                 }
             }
         }
         else if (Input.GetAxis("Vertical") <= 0.3f && Input.GetAxis("Vertical") >= -0.3f && Input.GetAxis("Horizontal") <= -0.3f)                                                      //Left
         {
             //Debug.Log("True");
             if (m_CurrentRotation.eulerAngles.y <= 269 || m_CurrentRotation.eulerAngles.y >= 271)
             {
                 //Debug.Log("True");
                 if (m_CurrentRotation.eulerAngles.y >= 90 && m_CurrentRotation.eulerAngles.y <=269)
                 {
                    //Debug.LogWarning("True");
                    TurnRight();
                 }
                 else
                 {
                    //Debug.LogWarning("True");
                    TurnLeft();
                 }
             }
         }/**/
        #endregion

        #region DiagonalMovement
        if (Input.GetAxis("Horizontal") >= 0.3f && Input.GetAxis("Vertical") >= 0.3f)                   //Up Right
        {   
            if (m_CurrentRotation.eulerAngles.y <= 43 || m_CurrentRotation.eulerAngles.y >= 47)
            {
                if (m_CurrentRotation.eulerAngles.y <= 43)
                {
                    //Debug.Log("True");
                    TurnRight();
                    
                }
                else if (m_CurrentRotation.eulerAngles.y >= 225)
                {
                    TurnRight();
                    //Debug.Log("True");
                }
                else if (m_CurrentRotation.eulerAngles.y <= 225 && m_CurrentRotation.eulerAngles.y >= 47)
                {
                    TurnLeft();
                }
            }
        }
        else if(Input.GetAxis("Horizontal") >= 0.3f && Input.GetAxis("Vertical") <= -0.3f)          //Down Right
        {
            if (m_CurrentRotation.eulerAngles.y <= 133 || m_CurrentRotation.eulerAngles.y >= 137)
            {
                if(m_CurrentRotation.eulerAngles.y == 0 || m_CurrentRotation.eulerAngles.y <= 133)
                {
                   //Debug.LogWarning("True");
                    TurnRight();
                }
                else if (m_CurrentRotation.eulerAngles.y >= 315)
                {
                   //Debug.LogWarning("True");
                    TurnRight();
                }
                else if (m_CurrentRotation.eulerAngles.y <= 315 && m_CurrentRotation.eulerAngles.y >= 137)
                {
                    //Debug.LogWarning("True");
                    TurnLeft();
                }
            }
        }
        else if (Input.GetAxis("Horizontal") <= -0.3f && Input.GetAxis("Vertical") <= -0.3f)    //Down Left
        {
            if (m_CurrentRotation.eulerAngles.y <= 223 || m_CurrentRotation.eulerAngles.y >= 227)
            {
                //Debug.Log("True");
                if (m_CurrentRotation.eulerAngles.y == 0 || m_CurrentRotation.eulerAngles.y >= 227)
                {
                   // Debug.LogWarning("True");
                    TurnLeft();
                }
                else if (m_CurrentRotation.eulerAngles.y <= 45)
                {
                    //Debug.LogWarning("True");
                    TurnLeft();
                }
                else if (m_CurrentRotation.eulerAngles.y >= 45 && m_CurrentRotation.eulerAngles.y <= 223)
                {
                    //Debug.LogWarning("True");
                    TurnRight();
                }
            }
        }
        else if (Input.GetAxis("Horizontal") <= -0.3f && Input.GetAxis("Vertical") >= 0.3f)         //Up Left
        {
            //Debug.Log("True");
            if (m_CurrentRotation.eulerAngles.y <= 313 || m_CurrentRotation.eulerAngles.y >= 317)
            {
                if (m_CurrentRotation.eulerAngles.y == 0 || m_CurrentRotation.eulerAngles.y >= 317)
                {
                    TurnLeft();
                }
                else if (m_CurrentRotation.eulerAngles.y <= 135)
                {
                    TurnLeft();
                }
                else if (m_CurrentRotation.eulerAngles.y >= 135 && m_CurrentRotation.eulerAngles.y <= 313)
                {
                    TurnRight();
                }
            }
        }
        #endregion
    }

    private void TurnRight()
    {
        Vector3 NewRotation = new Vector3(0.0f, 0.0f, 0.0f);
        NewRotation.y = NewRotation.y + (m_TurnSpeed_CHANGEABLE * Time.deltaTime);
        transform.Rotate(NewRotation);
    }
    private void TurnLeft()
    {
        Vector3 NewRotation = new Vector3(0.0f, 0.0f, 0.0f);
        NewRotation.y = NewRotation.y + ((m_TurnSpeed_CHANGEABLE * -1.0f) * Time.deltaTime);
        transform.Rotate(NewRotation);
    }
}