using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishDoor : MonoBehaviour        // created by Devin Hunt
{
    public GameObject levelFinshDoor;
    
    public Sprite openDoor;

    void Update()
    {             
        if (CleanUpObjectiveManager.enemyCount == 0 && CleanUpObjectiveManager.debrisCount == 0)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {       
       levelFinshDoor.GetComponent<SpriteRenderer>().sprite = openDoor;       
       levelFinshDoor.GetComponent<BoxCollider2D>().enabled = false;      
    }
}