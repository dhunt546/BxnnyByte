using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoomSwitcher : MonoBehaviour
{
    public float camSizeValue;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    void CamZoomHandler()
    {
        float zoomSpeed = 0.5f;
        
        cinemachineVirtualCamera.m_Lens.OrthographicSize = 
            Mathf.Lerp(cinemachineVirtualCamera.m_Lens.OrthographicSize, camSizeValue, zoomSpeed + Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log(other.name);
            CamZoomHandler();
        }
    }
}
