using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : ObjectHealth
{
    // Start is called before the first frame update
    void Start()
    {
        ObjectGetComponents();
        SetObjectDefaultHealth(3f);
    }


}
