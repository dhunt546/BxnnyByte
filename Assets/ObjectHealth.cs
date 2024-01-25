using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectHealth : MonoBehaviour, IDamageable
{
    //Breakable Objects
    SpriteRenderer objectSpriteRenderer;
    [SerializeField]private float currentObjectHealth;

    public void SetObjectDefaultHealth(float maxHealth)
    {
        currentObjectHealth = maxHealth;

    }
    void CheckHealth(float currentObjectHealth)
    {
        if (currentObjectHealth <= 0)
        {
            ObjectDestroyed();
        }
    }


    public void ObjectGetComponents()
    {
        objectSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void ObjectDestroyed()
    {
        Destroy(this.gameObject);
    }

    void IDamageable.Damage(float damageAmount)
    {
        
        ObjectTakeVisualDamage();
        currentObjectHealth -= damageAmount;
        CheckHealth(currentObjectHealth);
    }

    public virtual void ObjectTakeVisualDamage()
    {
        StartCoroutine(ShakeSprite(transform));
        StartCoroutine(ObjectFlashDamage(objectSpriteRenderer));
    }


    public IEnumerator ShakeSprite(Transform transform)
    {
        float shakeIntensity = 0.1f;
        float shakeDuration = 0.1f;
        float squishScale = 0.5f;
        float elapsedTime = 0f;
        Vector3 originalScale = transform.localScale;
        Vector3 originalPosition = transform.position;
        while (elapsedTime < shakeDuration)
        {
            Vector3 squishedScale = new Vector3(originalScale.x, originalScale.y * squishScale, originalScale.z);
            transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;
            transform.localScale = Vector3.Lerp(originalScale, squishedScale, elapsedTime / shakeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
        transform.position = originalPosition;


    }
    public IEnumerator ObjectFlashDamage(SpriteRenderer spriteRenderer)
    {

        float flashDuration = 0.2f;

        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;
            // Flash red
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;

        }
    }
}

