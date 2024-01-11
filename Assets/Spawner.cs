using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject frontEgg;
    [SerializeField] private GameObject middleEgg;
    [SerializeField] private GameObject backEgg;
    [SerializeField] private GameObject glow1;
    [SerializeField] private GameObject glow2;
    private SpriteRenderer glow1Renderer;
    private SpriteRenderer glow2Renderer;
    [SerializeField] private float glowspeed;
    
    private float minScale = 0.95f;
    private float maxScale = 1.05f;
    // Start is called before the first frame update
    void Start()
    {
        glow1Renderer = glow1.GetComponent<SpriteRenderer>();
        glow2Renderer = glow2.GetComponent<SpriteRenderer>();
        //start Glow
        StartCoroutine(GlowViens(glow2));
        StartCoroutine(GlowViens(glow1));
        //Start pulse for each
        StartCoroutine(PulseSprite(frontEgg));
        StartCoroutine(PulseSprite(middleEgg));
        StartCoroutine(PulseSprite(backEgg));
    }

    IEnumerator PulseSprite(GameObject egg)
    {
        float randomSquishSpeed = Random.Range(0.5f, 1.0f);
        while (true)
        {

            float lerpTime = Mathf.PingPong(Time.time * randomSquishSpeed, 1);
            float scale = Mathf.Lerp(minScale, maxScale, lerpTime);

            // Apply the pulsating scale to the GameObject
            egg.transform.localScale = new Vector3(1, scale, 1);

            yield return null;
        }
    }

    IEnumerator GlowViens(GameObject glow)
    {
        SpriteRenderer renderer = glow.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
        while (true)
        {
            float targetAlpha = Random.Range(0.2f, 0.8f);

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
}
