using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VisualAnimationController 
{

    public static IEnumerator Flash(SpriteRenderer spriteRenderer)
    {
        float flashDuration = 0.2f;

        if (spriteRenderer == null)
        {
            Color originalColor = spriteRenderer.color;
            // Flash red
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
            Debug.Log(spriteRenderer);

        }
    }

  //  public  static IEnumerator ShakeSprite(Transform transform)
  // {
  //     Debug.Log("Sprite Shook: " + ShakeSprite(transform));
  //     float shakeIntensity = 0.1f;
  //     float shakeDuration = 0.1f;
  //     float squishScale = 0.5f;
  //     float elapsedTime = 0f;
  //     Vector3 originalScale = transform.localScale;
  //     while (elapsedTime < shakeDuration)
  //     {
  //         Vector3 squishedScale = new Vector3(originalScale.x, originalScale.y * squishScale, originalScale.z);
  //         Vector3 originalPosition = transform.position;
  //         transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;
  //         transform.localScale = Vector3.Lerp(originalScale, squishedScale, elapsedTime / shakeDuration);
  //         elapsedTime += Time.deltaTime;
  //         yield return null;
  //     }
  //     transform.localScale = originalScale;
  // }
}
