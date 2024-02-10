using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Animator stateDrivenCamAnim;
    private bool isInBigRoom = true;
   

    private void Awake()
    {
        stateDrivenCamAnim = GetComponent<Animator>();
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isInBigRoom = !isInBigRoom;
            stateDrivenCamAnim.SetBool("isInBigRoom", isInBigRoom);
        }
     
    }

}
