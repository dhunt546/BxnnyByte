using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishDoor : MonoBehaviour
{
    public GameObject levelFinshDoor;

    void Start()
    {
        
    }
    void Update()
    {
        FinalDoorChecks();
    }
    
    void FinalDoorChecks()
    {
        if(gameObject.CompareTag("Enemy") && gameObject.CompareTag("Debris") == false)
        {
            levelFinshDoor.GetComponent<BoxCollider2D>().enabled = false;
            levelFinshDoor.SetActive(false);
        }
    }
}
