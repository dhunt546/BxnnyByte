using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Spawners;
    [SerializeField] private GameObject[] GlowingVeins;
    [SerializeField] private float glowspeed;
    [SerializeField] private float spawnTime = 5f;
    [SerializeField] private float animationLockTime = 3f;
    
    Animator animator;

    private float minScale = -0.05f;
    private float maxScale = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
        //start Glow
        StartGlowing(GlowingVeins);
        //Start pulse for each
        StartBreathing(Spawners);

        StartCoroutine(SpawnTimer());
    }

    void StartBreathing(GameObject[] objects)
    {
        foreach (var obj in objects)
        {
            StartCoroutine(PulseSprite(obj));
        }
    }
    void StartGlowing(GameObject[] objects)
    {
        foreach (var obj in objects)
        {
            StartCoroutine(GlowViens(obj));
        }
    }
    IEnumerator PulseSprite(GameObject obj)
    {
        float randomSquishSpeed = UnityEngine.Random.Range(0.5f, 1.0f);
        Vector3 originalScale = obj.transform.localScale;
        while (true)
        {
            minScale = -0.05f;
            maxScale = 0.05f;
            float lerpTime = Mathf.PingPong(Time.time * randomSquishSpeed, 1);
            float scale = Mathf.Lerp(minScale, maxScale, lerpTime);

            // Apply the pulsating scale to the GameObject

            obj.transform.localScale = new Vector3(originalScale.x, originalScale.y + scale, originalScale.z);

            yield return null;
        }
    }

    IEnumerator GlowViens(GameObject glow)
    {
        SpriteRenderer renderer = glow.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
        while (true)
        {
            float targetAlpha = UnityEngine.Random.Range(0.2f, 0.8f);

            float elapsedTime = 0f;
            float duration = 1f;

            Color currentColor = renderer.color;
            float startAlpha = currentColor.a;
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;

                // Lerp the alpha value
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);

                // Update the color with the new alpha value
                renderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);

                // Increment the elapsed time
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    IEnumerator SpawnTimer()
    {
        while (true)
        {
            // Iterate through each spawner in the array
            foreach (var spawner in Spawners)
            {
                // Call your AlienHatch function (replace with your actual logic)
                AlienHatch(spawner);

                // Wait for the specified spawn time before hatching the next alien egg
                yield return new WaitForSeconds(spawnTime);
            }
        }
    }
    void AlienHatch(GameObject obj)
    {
        //Play Partile effect
        //Play Animation
        StartCoroutine(HatchingAnimation(obj, FrontHatching));
        //Insitiate Alien
    }

    private IEnumerator HatchingAnimation(GameObject obj,int animationHash)
    {
        animator = obj.GetComponent<Animator>();
        animator.Play(animationHash, 0,0);
        yield return new WaitForSeconds(animationLockTime);
        animator.StopPlayback();
    }

    private static readonly int FrontHatching = Animator.StringToHash("Hatching1");
    private static readonly int BackHatching = Animator.StringToHash("Hatching2");
}
