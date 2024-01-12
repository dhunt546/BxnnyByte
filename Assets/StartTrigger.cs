using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{

    // Use this script that if something happens (ie; A certain debris is destroyed) then it will start the hatching of aliens)

    public GameObject triggerObject;
    private bool wasTrigger = false;
    private Spawner spawner;
    private bool hasTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        //If a trigger was ever added in the first place;
        if (triggerObject != null)
        {
            wasTrigger = true;
        }

        //if there was a trigger o
        if (hasTriggered)
        {
            //do nothing
        }
        else if (wasTrigger && triggerObject == null)
        {
            spawner.StartSpawningEggs();
            hasTriggered = true;

        }
    }
}
