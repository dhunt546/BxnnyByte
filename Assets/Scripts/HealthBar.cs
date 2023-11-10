using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour

{

    public Image[] healthImgs;
    PlayerHealth playerHealth;
    int health;
    void Start()
    {
        //playerHealth = GameObject.FindGameObjectsWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        health = playerHealth.health;

        switch(health)
        {
            case 5:
                foreach(Image img in healthImgs)
                {
                    img.gameObject.SetActive(true);
                }
                break;
           
            case 4:
                healthImgs[1].gameObject.SetActive(true);
                healthImgs[2].gameObject.SetActive(true);
                healthImgs[3].gameObject.SetActive(true);
                healthImgs[4].gameObject.SetActive(true);
                healthImgs[5].gameObject.SetActive(false);
                break;
           
            case 3:
                healthImgs[1].gameObject.SetActive(true);
                healthImgs[2].gameObject.SetActive(true);
                healthImgs[3].gameObject.SetActive(true);
                healthImgs[4].gameObject.SetActive(false);
                healthImgs[5].gameObject.SetActive(false);
                break;

            case 2:
                healthImgs[1].gameObject.SetActive(true);
                healthImgs[2].gameObject.SetActive(true);
                healthImgs[3].gameObject.SetActive(false);
                healthImgs[4].gameObject.SetActive(false);
                healthImgs[5].gameObject.SetActive(false);
                break;

            case 1:
                healthImgs[1].gameObject.SetActive(true);
                healthImgs[2].gameObject.SetActive(false);
                healthImgs[3].gameObject.SetActive(false);
                healthImgs[4].gameObject.SetActive(false);
                healthImgs[5].gameObject.SetActive(false);
                break;

            case 0:
                healthImgs[1].gameObject.SetActive(false);
                healthImgs[2].gameObject.SetActive(false);
                healthImgs[3].gameObject.SetActive(false);
                healthImgs[4].gameObject.SetActive(false);
                healthImgs[5].gameObject.SetActive(false);
                break;
        }
    }
}
