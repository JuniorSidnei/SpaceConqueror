using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    private Camera m_camera;

    public static CameraController Instance;
    
    
    private void Start()
    {
        Instance = this;
        m_camera = GetComponent<Camera>();
    }

    public void ScreenShake()
    {
        m_camera.transform.DOShakePosition(0.2f, new Vector3(1.5f, 1.5f, 0), 5, 45f);
    }
}
