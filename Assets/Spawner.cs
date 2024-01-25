
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Spawner : ObjectHealth
//By Regan Ly!
{
    [SerializeField] private GameObject[] Spawners;
    [SerializeField] private GameObject[] GlowingVeins;
    [SerializeField] private GameObject[] lights;
    [SerializeField] private float glowspeed;
    [SerializeField] private float spawnTime;
    [SerializeField] private float animationLockTime = 3f;
    [SerializeField] private GameObject gasEffect;
    [SerializeField] private GameObject Alien;
    

    Animator animator;

    private float minScale = -0.05f;
    private float maxScale = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        GlowLights(lights);
        //start Glow
        StartGlowing(GlowingVeins);
        //Start pulse for each
        StartBreathing(Spawners);
        SetObjectDefaultHealth(2f);

    }

    public override void ObjectTakeVisualDamage()
    {
        StartCoroutine(ShakeSprite(transform));
        foreach (var spawner in Spawners)
        {
            SpriteRenderer spawnerRenderer = spawner.GetComponent<SpriteRenderer>();
            StartCoroutine(ObjectFlashDamage(spawnerRenderer));
        }

    }



    public void StartSpawningEggs()
    {
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
        gasEffect.transform.position = obj.transform.position;
        ParticleSystem gasEffectPS = gasEffect.GetComponent<ParticleSystem>();
        gasEffectPS.Play();
        //Play Animation
        StartCoroutine(HatchingAnimation(obj, FrontHatching));
        //Insitiate Alien

        GameObject newAlien = HatchedAlien(obj);
        GrowAlien(newAlien);
        
    }
   
    GameObject HatchedAlien(GameObject obj)
    {
        var newNewAlien =
        Instantiate(Alien, obj.transform.position, Quaternion.identity);
        
        return newNewAlien;
    }

    void GrowAlien(GameObject newAlien)
    {
        Vector3 newAlienOriginalScale = newAlien.transform.localScale;
        newAlien.transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(ScaleObject(newAlien.transform, newAlienOriginalScale, 0.5f));
    
    }
    IEnumerator ScaleObject(Transform objTransform, Vector3 targetScale, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (objTransform != null)
            objTransform.localScale = Vector3.Lerp(objTransform.localScale, targetScale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object's scale is exactly the target scale when the animation is complete
        if (objTransform != null)
            objTransform.localScale = targetScale;
    }

    void GlowLights(GameObject[] lights)
    {
        foreach (var light in lights)
        {
            Light2D lights2D = light.GetComponent<Light2D>();
            float orginalIntensity = lights2D.intensity;
            StartCoroutine(LightsGlow(lights2D, orginalIntensity));
        }
    }
    IEnumerator LightsGlow( Light2D lights, float orginalIntensity)
    {
        while (true)
        {

            if (lights != null)
            {
                // Set the light intensity
                lights.intensity = Mathf.PingPong(Time.time/8, orginalIntensity);
            }
            yield return null;
        }
    }

    private IEnumerator HatchingAnimation(GameObject obj,int animationHash)
    {
        animator = obj.GetComponent<Animator>();
        animator.Play(animationHash, 0,0);
        yield return new WaitForSeconds(animationLockTime);
    }

    private static readonly int FrontHatching = Animator.StringToHash("Hatching1");
    private static readonly int BackHatching = Animator.StringToHash("Hatching2");
}
