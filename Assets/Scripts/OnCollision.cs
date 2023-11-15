using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour    //Created by Devin H.
{

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Enemy")  //Can be changed for to a hit attack.
        {
            Healthbar.health--;
            if (Healthbar.health <= 0)
            {
                //Gamemanger.isGameOver = true;     //Script needed to be created to run gameover scene on death for this line to work.
                gameObject.SetActive(false);
            }

        }


    }
}
