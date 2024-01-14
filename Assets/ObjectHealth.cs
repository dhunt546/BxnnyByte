using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectHealth : MonoBehaviour,IDamageable
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float maxHealth;
     private float currentHealth;

    private void Start()
    {

        currentHealth = maxHealth;
    } 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(currentHealth);
        }

        if (currentHealth == 0)
        {
            ObjectDestroyed();
        }
    }

    void ObjectDestroyed()
    {
        Destroy(this.gameObject);
    }

        void IDamageable.Damage(float damageAmount)
    {
        //EnemyFlash(spriteRenderer);
        StartCoroutine(ShakeSprite(transform));
        currentHealth -= damageAmount;
    }


    public IEnumerator ShakeSprite(Transform transform)
    {
        Debug.Log("Sprite Shook: " + ShakeSprite(transform));
        float shakeIntensity = 0.1f;
        float shakeDuration = 0.1f;
        float squishScale = 0.5f;
        float elapsedTime = 0f;
        Vector3 originalScale = transform.localScale;
        while (elapsedTime < shakeDuration)
        {
            Vector3 squishedScale = new Vector3(originalScale.x, originalScale.y * squishScale, originalScale.z);
            Vector3 originalPosition = transform.position;
            transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;
            transform.localScale = Vector3.Lerp(originalScale, squishedScale, elapsedTime / shakeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
    }
}

