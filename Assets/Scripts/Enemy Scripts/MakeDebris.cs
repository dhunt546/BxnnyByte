using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDebris : MonoBehaviour
{
    // created by Devin Hunt

    public GameObject debris;

    private EnemyAI enemyAi;
    
    bool isAble = false;

    float debrisTimer;
    float wandering;
    float timerUp;
    float timerDown;
    float madeMess;

    void Start()
    { 
        enemyAi = FindObjectOfType<EnemyAI>();      
    }
  
    void Update()
    {  
         wandering = enemyAi.wanderTimer;

        if (isAble == true)
        {
            CauseDebris();
        }
        
        if (isAble == false)
        {
            CountDown();
        }       
    }
   
    void CauseDebris()
    {
        debrisTimer += Time.deltaTime;

            if (debrisTimer >= wandering)
            {
                timerUp++;

                debrisTimer = 0;               

                if (timerUp >= 5) 
                {                  
                    Instantiate(debris, transform.position, Quaternion.identity);

                    madeMess++;

                    isAble = false;

                    if (madeMess == 2)
                    {                      
                        GetComponent<MakeDebris>().enabled = false;
                    }
                }               
            }     
    }

    void CountDown()
    { 
        debrisTimer += Time.deltaTime;

        if (debrisTimer >= wandering)
        {
            timerDown--;
            //Debug.Log("timerDown =" + timerDown);
            debrisTimer = 0;

            if (timerDown <= -10)
            {
                isAble = true;
                timerDown = 0;
            }
        }
    }
}