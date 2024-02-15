using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarmer : EnemyAbstract
{
    
    void Start()
    {
        EnemyGetComponents();
    }

   
    void Update()
    {
        EnemyUpdate();
    }
}
