using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawnAliens : MonoBehaviour
{
    // created by Devin Hunt

    GameObject spawnling;

    private EnemyAI enemyAi;

    bool isAble = false;

    float spawnTimer;
    float wandering;
    float timerUp;
    float timerDown;
    float totalSpawnlings;

    void Start()
    {
        enemyAi = FindObjectOfType<EnemyAI>();
        spawnling = GameObject.Find("Spider_Alien");
    }

    void Update()
    {


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
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= wandering)
        {
            timerUp++;

            spawnTimer = 0;

            if (timerUp >= 5)
            {
                if( spawnling != null)
                {
                    Instantiate(spawnling, transform.position, Quaternion.identity);

                    totalSpawnlings++;

                    isAble = false;
                }         

                if (totalSpawnlings == 3)
                {
                    GetComponent<AlienSpawnAliens>().enabled = false;
                }
            }
        }
    }

    void CountDown()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= wandering)
        {
            timerDown--;
       
            spawnTimer = 0;

            if (timerDown <= -20)
            {
                isAble = true;
                timerDown = 0;
            }
        }
    }
}