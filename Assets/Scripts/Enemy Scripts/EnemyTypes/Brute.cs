using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brute : EnemyAbstract
{
    // Start is called before the first frame update
    void Start()
    {
        EnemyGetComponents();
    }


    // Update is called once per frame
    void Update()
    {
        EnemyUpdate();
    }
}
