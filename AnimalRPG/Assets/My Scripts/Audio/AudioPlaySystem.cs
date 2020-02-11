using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlaySystem : MonoBehaviour
{
    public AudioSource StartAudio;
    public AudioSource LoopAudio;
    public double SetEndTime;

    // Start is called before the first frame update
    void Awake()
    {
        
        if (SetEndTime != 0)
        {
            StartAudio.SetScheduledEndTime(SetEndTime);
            
            Debug.Log(string.Format("Playing first song for {0} seconds", SetEndTime));
        }
        else
        {
            StartAudio.Play();
            Debug.Log("Playing starting song");
        }
        
    }

    void LateUpdate()
    {
        

        if (!StartAudio.isPlaying && !LoopAudio.isPlaying)
        {
            LoopAudio.Play();
            Debug.Log("Playing loop song");
        }
    }
}
