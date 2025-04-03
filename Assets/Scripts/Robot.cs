using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Character
{
    [SerializeField] private float damageReduction = 0.6f;
    public new int Health
    {
        get { return Health; }
        set 
        {
            int incomingDamage = value;
            float calculatedDamage = incomingDamage * damageReduction;
            Health -= value; 
        }
    }
}
