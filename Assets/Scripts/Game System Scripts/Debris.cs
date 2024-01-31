using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : ObjectHealth
{
    void Start()
    {
        ObjectGetComponents();
        SetObjectDefaultHealth(objectMaxHealth);
    }

}
