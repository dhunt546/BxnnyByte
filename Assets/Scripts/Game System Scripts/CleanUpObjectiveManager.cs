using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUpObjectiveManager : MonoBehaviour        // created by Devin Hunt
{
    public static int enemyCount;
    public static int debrisCount;

    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        debrisCount = GameObject.FindGameObjectsWithTag("Debris").Length;
    }

    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        debrisCount = GameObject.FindGameObjectsWithTag("Debris").Length;
    }
}
