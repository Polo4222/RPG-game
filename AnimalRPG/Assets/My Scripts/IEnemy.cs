using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    int EnemyID { get; set; }
    int Health { get; set; }
    int Armour { get; set; }
    int Mana { get; set; }
}
