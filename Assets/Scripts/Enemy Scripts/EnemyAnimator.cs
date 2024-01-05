using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private float flashDuration = 0.2f;
   // private float shakeIntensity = 0.1f;
   // private float shakeDuration = 0.1f;
   // private float squishScale = 0.5f;

    private Color originalColor;
  //  private Vector3 originalPosition;
  //  private Vector3 originalScale;
    // Start is called before the first frame update
    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        originalColor = spriteRenderer.color;
       // originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyVisualDamageTaken()
    {
        StartCoroutine(FlashAndShake());
    }

    private IEnumerator FlashAndShake()
    {
        // Flash red
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
        /*squish 

         Vector3 squishedScale = new Vector3(originalScale.x, originalScale.y * squishScale, originalScale.z);

        // Shake and Squish
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            originalPosition = transform.position;
            transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;
            transform.localScale = Vector3.Lerp(originalScale, squishedScale, elapsedTime / shakeDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Yield inside the shaking loop
        }
        transform.localScale = originalScale;
        */
    }
}
