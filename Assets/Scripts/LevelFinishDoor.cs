using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishDoor : MonoBehaviour
{
    public GameObject levelFinshDoor;
    public Sprite openDoor;

    int enemyCount;
    int debrisCount;

    void Start()
    {
        
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        debrisCount = GameObject.FindGameObjectsWithTag("Debris").Length;
    }
  
    void Update()
    {     
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        debrisCount = GameObject.FindGameObjectsWithTag("Debris").Length;
        
        if (enemyCount == 0 && debrisCount == 0)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
       
       levelFinshDoor.GetComponent<SpriteRenderer>().sprite.Equals(openDoor);
       // Debug.Log(levelFinshDoor.GetComponent<SpriteRenderer>().sprite.name);
        levelFinshDoor.GetComponent<BoxCollider2D>().enabled = false;      
    }
}
