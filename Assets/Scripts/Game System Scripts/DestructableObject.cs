using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : ObjectHealth
{

    void Start()
    {
        ObjectGetComponents();
        SetObjectDefaultHealth(objectMaxHealth);
    }

}
