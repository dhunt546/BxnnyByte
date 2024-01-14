using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingController : MonoBehaviour
{
    [SerializeField] private GameObject[] LightObjects;
    private bool isFlashing;

    float flickerDuration = 0.15f;

    
    [Range(1, 5)]
    public int lightsFlickerIntensity;
    void Start()
    {
        StartFlashing(LightObjects);
    }
    void StartFlashing(GameObject[] Lights)
    {

        if (!isFlashing)
        {
            GrabLights(Lights);
        }
    }

    float SetMaxIntensity(GameObject light)
    {
        Light2D light2D = light.GetComponent<Light2D>();
        float originalIntencity = light2D.intensity;
        return originalIntencity;
    }
    void GrabLights(GameObject[] lightlist)
    {
        foreach (GameObject light in lightlist)
        {
            float maxIntensity = SetMaxIntensity(light);
            Light2D light2D = light.GetComponent<Light2D>();
            FlickerAllLights(light2D, maxIntensity);
        }

    }
    
    float delayBetweenFlickers()
    {
        float minDelayBase = 3f;
        float maxDelayBase = 15f;

        float minDelay = minDelayBase / lightsFlickerIntensity;
        float maxDelay = maxDelayBase / lightsFlickerIntensity;


        float delay = Random.Range(minDelay,maxDelay);
        return delay;
    }
    float durationOfFlickers()
    {
        float minDurationBase = 0.5f;
        float maxDurationBase = 2f;

        float minDuration = minDurationBase * lightsFlickerIntensity;
        float maxDuration = maxDurationBase * lightsFlickerIntensity;

        float flickerDuration = Random.Range(minDuration,maxDuration);
        return flickerDuration;
    }
    void FlickerAllLights(Light2D lights, float maxIntensity)
    {
        float minDelayRange = delayBetweenFlickers();
        float randomDuration = durationOfFlickers();
        isFlashing = true;
            StartCoroutine(LightsFlicker(lights, maxIntensity));
            StartCoroutine(RestartFlashingAfterDuration(lights, minDelayRange, randomDuration, maxIntensity));
    }
    
    //Actual flicker occurs here
    IEnumerator LightsFlicker(Light2D light, float maxIntensity)
    {

        //while (true) causes it to loop.
        while (true)
        {
            // if flashing is true, flash, else if false, break the loop.
            if (isFlashing)
            {
                float random1 = Random.Range(maxIntensity / 4, maxIntensity);
                float random2 = Random.Range(maxIntensity / 4, maxIntensity);
                light.intensity = random1;
                yield return new WaitForSeconds(flickerDuration);

                light.intensity = random2;
                yield return new WaitForSeconds(flickerDuration);
            }
            else
            break;
        }
    }

    //stops  flickering and then starts it again
    IEnumerator RestartFlashingAfterDuration(Light2D lights, float Delay, float randomDuration, float maxIntensity)
    {
        //wait random duration time
        yield return new WaitForSeconds(randomDuration);

        //stop flashing
        isFlashing = false;
        //set lights to its original intensity
        lights.intensity = maxIntensity;
        //delay time
        yield return new WaitForSeconds(Delay);

        //after delay, restarts flashing
        FlickerAllLights(lights, maxIntensity);

    }

    //Function stops flashing of all lights
    public void StopFlashing()
    {
        isFlashing = false;
    }

}
