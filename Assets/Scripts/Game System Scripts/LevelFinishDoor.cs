using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishDoor : MonoBehaviour
{
    public GameObject levelFinshDoor;
    public Sprite openDoor;

    int enemyCount;
    int debrisCount;

    private void Start()
    {        
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        debrisCount = GameObject.FindGameObjectsWithTag("Debris").Length;
    }
  
    private void Update()
    {     
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        debrisCount = GameObject.FindGameObjectsWithTag("Debris").Length;
        
        if (enemyCount == 0 && debrisCount == 0)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {       
       levelFinshDoor.GetComponent<SpriteRenderer>().sprite = openDoor;       
       levelFinshDoor.GetComponent<BoxCollider2D>().enabled = false;      
    }
}