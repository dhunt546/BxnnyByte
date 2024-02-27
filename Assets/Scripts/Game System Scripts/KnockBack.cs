using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class KnockBack : MonoBehaviour       //Jacob Foran https://www.youtube.com/watch?v=QnsGSCXknUY
{
    public float thrust;
    public float knocktime;
    Transform playerTransform;
 

    public void Knockback()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
       

        // Check if the player GameObject is found
        if (playerObject != null)
        {
            // Get the player's transform reference
            playerTransform = playerObject.transform;
        }

        Rigidbody2D Enemy = GetComponent<Rigidbody2D>();
        if (playerObject != null && Enemy != null)
        {
            Enemy.isKinematic = false;
            Vector2 difference = Enemy.transform.position - playerObject.transform.position;
            difference = difference.normalized * thrust;
            Enemy.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(KnockCo(Enemy));
        }


    }

    private IEnumerator KnockCo(Rigidbody2D Enemy)
    {
        if(Enemy != null)
        {
            yield return new WaitForSeconds(knocktime);
            Enemy.velocity = Vector2.zero;
            Enemy.isKinematic = true;
        }
    }

}
