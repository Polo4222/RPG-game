﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBonus : MonoBehaviour
{
    public int BonusValue { get; set; }
    public StatBonus(int bonusValue)
    {
        this.BonusValue = bonusValue;
        Debug.Log("New stat bonus initiated.");
    }
}
