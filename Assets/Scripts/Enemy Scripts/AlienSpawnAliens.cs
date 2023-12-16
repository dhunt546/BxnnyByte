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
        spawnling = GameObject.FindGameObjectWithTag("Spider_Alien");
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
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= wandering)
        {
            timerUp++;

            spawnTimer = 0;

            if (timerUp >= 5)
            {
                Instantiate(spawnling, transform.position * 2.0f , Quaternion.identity);

                totalSpawnlings++;

                isAble = false;

                if (totalSpawnlings == 2)
                {
                    GetComponent<MakeDebris>().enabled = false;
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

            if (timerDown <= -10)
            {
                isAble = true;
                timerDown = 0;
            }
        }
    }
}