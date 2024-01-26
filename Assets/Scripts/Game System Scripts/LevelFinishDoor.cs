using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishDoor : MonoBehaviour
{
    public GameObject levelFinshDoor;
    public Sprite openDoor;

    int enemyCount;
    public static int debrisCount;
    private void Update()
    {
        Debris[] debrisObjects = GameObject.FindObjectsOfType<Debris>();
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        debrisCount = debrisObjects.Length;
        
        
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