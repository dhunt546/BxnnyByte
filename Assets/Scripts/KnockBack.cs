using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;

public class KnockBack : MonoBehaviour       //Jacob Foran https://www.youtube.com/watch?v=QnsGSCXknUY
{
    public float thrust;
    public float knocktime;


    private void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D  Enemy = Other.GetComponent<Rigidbody2D>();
            if(Enemy != null)
            {
                Enemy.isKinematic = false;
                Vector2 difference = Enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                Enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(Enemy));
            }
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
