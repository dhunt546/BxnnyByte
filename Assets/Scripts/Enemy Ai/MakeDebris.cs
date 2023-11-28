using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDebris : MonoBehaviour
{
    // created by Devin Hunt

    public GameObject debris;
    float debrisTimer;
    bool isAble = false;
    private EnemyAI enemyAi;
    float wandering;
    float timerUp;
    float timerDown;
    float madeMess;

    void Start()
    {   //Get EmenyAI script.
        enemyAi = FindObjectOfType<EnemyAI>();
       
    }

    
    void Update()
    {   //Assigns wandering to EnemyAI wanderTimer value.
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

    // Checks to make debris
    void CauseDebris()
    {
        debrisTimer += Time.deltaTime;

            if (debrisTimer >= wandering)
            {
                timerUp++;
                debrisTimer = 0;
                //Debug.Log("timerUp =" + timerUp);

                if (timerUp >= 5) 
                {
                    //Debug.Log("makingdebris");
                    Instantiate(debris, transform.position, Quaternion.identity);
                    madeMess++;
                    isAble = false;
                    if (madeMess == 2)
                    {
                        //Debug.Log("MakeDebris is turned off");
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

    public void ClearDebris() 
    {
        Destroy(debris);
    }

}
