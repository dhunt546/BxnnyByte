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
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalColor = spriteRenderer.color;
       // originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyVisualDamageTaken()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            StartCoroutine(EnemyFlash(spriteRenderer));
        }
        else
        {
            Debug.LogError("SpriteRenderer is null. Make sure the object has a SpriteRenderer component.");
        }
    }

    public IEnumerator EnemyFlash(SpriteRenderer spriteRenderer)
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


}
