using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderHealth : MonoBehaviour
{
    public int SpiderHP = 1;
    public GameObject Spider_Alien;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SpiderHP <= 0)
        {
            Destroy(Spider_Alien);
        }
    }
}
