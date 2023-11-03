using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField]
    private float maxSpeed = 2, acceleration = 50, deacceleration = 100;
    [SerializeField]
    private float currentSpeed = 0;
    private Vector2 oldMovementInput;
    public Vector2 MovementInput { get; set; }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (MovementInput.magnitude > 0 && currentSpeed >= 0)
        {
            oldMovementInput = MovementInput;
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        rb2d.velocity = oldMovementInput * currentSpeed;

    }

}


//    public GameObject player;
//    public float speed;
//    public float distancebetween;
//
//    private float distance;
//    void Start()
//    {
//        
//    }
//
//    
//    void Update()
//    {
//        distance = Vector2.Distance(transform.position, player.transform.position);
//        Vector2 direction = player.transform.position - transform.position;
//        direction.Normalize();
//        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//
//        if (distance <= distancebetween) 
//        {
//            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
//            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
//        
//        }
//
//
//    }
//}
