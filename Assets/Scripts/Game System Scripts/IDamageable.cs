using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    //public float HP { get; set; }
    public void Damage(float damageAmount)
    {
       // HP -= damageAmount;
    }
}

