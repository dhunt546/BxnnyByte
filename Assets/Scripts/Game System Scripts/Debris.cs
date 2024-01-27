using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : ObjectHealth
{
    float maxHealth = 2;
    // Start is called before the first frame update
    void Start()
    {
        ObjectGetComponents();
        SetObjectDefaultHealth(maxHealth);
    }

}
