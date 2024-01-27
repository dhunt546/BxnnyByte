using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : ObjectHealth
{
    [SerializeField, Range(2, 15)] private float maxHealth = 2f;
    // Start is called before the first frame update
    private void Start()
    {
        ObjectGetComponents();
        SetObjectDefaultHealth(maxHealth);
    }

}
