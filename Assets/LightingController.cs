using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingController : MonoBehaviour
{
    [SerializeField] private GameObject[] LightObjects;
    private bool isFlashing;


    [SerializeField] private float minIntensity = 0.5f;
    [SerializeField] private float maxIntensity = 1.0f;

    float flickerDuration = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        StartFlashing(LightObjects);
    }

    void StartFlashing(GameObject[] Lights)
    {
        if (!isFlashing)
        {
            isFlashing = true;

            StartCoroutine(FlickerAllLights(Lights));        
        }
    }

    IEnumerator FlickerAllLights(GameObject[] lightlist)
    {
        foreach (GameObject obj in lightlist)
        {
            Light2D lights = obj.GetComponent<Light2D>();
            float randomRange = Random.Range(0.5f,2f);
            float randomDuration = Random.Range(0.5f,1.5f);
            
            StartCoroutine(LightsFlicker(lights));
            StartCoroutine(StopFlashingAfterDuration(lights,randomRange, randomDuration));
            yield return new WaitForSeconds(2f);
        }

    }
    
    IEnumerator LightsFlicker(Light2D light)
    {
        while (isFlashing)
        {
            float off = 0f;
            float on = Random.Range(minIntensity, maxIntensity);

            if (isFlashing)
            {
                light.intensity = on;


                yield return
                    new WaitForSeconds(flickerDuration);
                light.intensity = off;
            }
            light.intensity = on;
        }

    }

    IEnumerator StopFlashingAfterDuration(Light2D lights, float randomRange, float randomDuration)
    {
        yield return new WaitForSeconds(randomDuration);
        
        StopFlashing();
        StartCoroutine(RestartFlashingAfterDuration(randomRange));
    }

    void StopFlashing()
    {
        isFlashing = false;
    }

    IEnumerator RestartFlashingAfterDuration(float minRange)
    {
        float lightsOnDelay = Random.Range(minRange, 5f);

        yield return new WaitForSeconds(lightsOnDelay);
        StartFlashing(LightObjects);
    }
}
