using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapons : ScriptableObject
{
    public int damage = 10;
    public float attackDelay = 2;
    public float retroceso = 2;

}
