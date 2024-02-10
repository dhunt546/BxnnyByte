using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoomSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    public float camDefaultSize = 8.0f;
    public float camNewSizeValue;
    public float zoomSpeed = 4.0f;

    bool isZoomingIn = true;
    bool isZoomingActive;

    void CamZoomHandler()
    {
        if (!isZoomingActive) return;

        float targetSize = isZoomingIn ? camNewSizeValue : camDefaultSize;
        cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(
            cinemachineVirtualCamera.m_Lens.OrthographicSize, targetSize, zoomSpeed * Time.deltaTime);

        if (Mathf.Abs(cinemachineVirtualCamera.m_Lens.OrthographicSize - targetSize) < 0.01f)
        {
            cinemachineVirtualCamera.m_Lens.OrthographicSize = targetSize;
            isZoomingIn = !isZoomingIn;
            isZoomingActive = false;
        }

        //Debug.Log("Cam current size = " + cinemachineVirtualCamera.m_Lens.OrthographicSize);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isZoomingActive = true;
        }
    }

    private void Update()
    {
        CamZoomHandler();
    }
}
