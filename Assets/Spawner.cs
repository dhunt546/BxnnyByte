using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject frontEgg;
    [SerializeField]
    private GameObject middleEgg;
    [SerializeField]
    private GameObject backEgg;


    private SpriteRenderer frontEggRenderer;
    private SpriteRenderer middleEggRenderer;
    private SpriteRenderer backEggRenderer;

    private Vector3 frontOrginalScale;
    [SerializeField]
    private float pulseSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {

        frontEggRenderer = frontEgg.GetComponent<SpriteRenderer>();


        frontOrginalScale = frontEgg.transform.localScale;
        StartCoroutine(PulseSprite());
    }

    IEnumerator PulseSprite()
    {
        while (true)
        {
            float pulseScale = 1f + Mathf.Sin(Time.time * pulseSpeed) * 0.1f; // Adjust the 0.1f factor for the magnitude of the pulse

            // Apply the pulsating scale to the GameObject
            frontEgg.transform.localScale = frontOrginalScale * pulseScale;

            yield return null;
        }
    }
}
