using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debris : MonoBehaviour
{
    //add to score
    //remove debris
    // Start is called before the first frame update


    public void CleanDebris()
    {
        //Debug.Log("destroy object");
        Destroy(gameObject);
    }
}
