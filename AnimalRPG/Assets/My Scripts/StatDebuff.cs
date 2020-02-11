using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDebuff : MonoBehaviour
{
    public int DebuffValue { get; set; }

    public StatDebuff(int DebuffValue)
    {
        this.DebuffValue = DebuffValue;
    }
}
