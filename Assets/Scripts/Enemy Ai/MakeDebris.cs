using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDebris : MonoBehaviour
{
    // created by Devin Hunt

    public GameObject debris;
    float debrisTimer;
    bool isAble;
    private EnemyAI enemyAi;
    float wandering;
    private float timer;

    void Start()
    {   //Get EmenyAI script.
        enemyAi = FindObjectOfType<EnemyAI>();
    }

    
    void Update()
    {   //Assigns wandering to EnemyAI wanderTimer value.
         wandering = enemyAi.wanderTimer;

    }

    // Checks to make debris
    void CauseDebris()
    {
        debrisTimer += Time.deltaTime;

        if (isAble == true) 
        {
           
            if (debrisTimer >= wandering)
            {
                Instantiate(debris, transform.position,Quaternion.identity);  
            }
        }
      

    }

}
