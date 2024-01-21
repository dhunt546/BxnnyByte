using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : ObjectHealth
{
    [SerializeField, Range(2, 15)] private float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        ObjectGetComponents();
        SetObjectDefaultHealth(maxHealth);
    }


}
