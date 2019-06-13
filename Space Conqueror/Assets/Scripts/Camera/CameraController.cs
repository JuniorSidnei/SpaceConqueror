using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    private Camera m_camera;

    public Transform m_target;

    private Vector3 m_velocity = Vector3.zero;

    public float m_smoothTime;


    [Header("Camera bound Settings")]
    public float m_maxY;
    public bool m_maxYEnabled;
    public float m_minY;
    public bool m_minYEnabled;
    public float m_maxX;
    public bool m_maxXEnabled;
    public float m_minX;
    public bool m_minXEnabled;
    
    public static CameraController Instance;

    private void Start()
    {
        Instance = this;
        m_camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        var targertPos = m_target.position;


        if (m_minYEnabled && m_maxYEnabled)
            targertPos.y = Mathf.Clamp(m_target.position.y, m_minY, m_maxY);
        else if(m_minYEnabled)
            targertPos.y = Mathf.Clamp(m_target.position.y, m_minY, m_target.position.y);
        else if (m_maxYEnabled)
            targertPos.y = Mathf.Clamp(m_target.position.y, m_target.position.y, m_maxY);
        
        
        if (m_minXEnabled && m_maxXEnabled)
            targertPos.x = Mathf.Clamp(m_target.position.x, m_minX, m_maxX);
        else if (m_minXEnabled)
            targertPos.x = Mathf.Clamp(m_target.position.x, m_minX, m_target.position.x);
        else if (m_maxXEnabled)
            targertPos.x = Mathf.Clamp(m_target.position.x, m_target.position.x, m_maxX);
        
        
        targertPos.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position,targertPos, ref m_velocity, m_smoothTime);
    }

    private void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(m_target.position);

        if(viewPos.x > 1f)
            m_target.position = Vector3.Lerp(m_target.position, viewPos, 0.1f);
        else if(viewPos.x < 0)
        {
            m_target.position = Vector3.Lerp(m_target.position, viewPos, 0.1f);
        }
        
        if(viewPos.y > 1f)
            m_target.position = Vector3.Lerp(m_target.position, viewPos, 0.1f);
        else if(viewPos.y < 0)
        {
            m_target.position = Vector3.Lerp(m_target.position, viewPos, 0.1f);
        }
    }

    public void ScreenShake()
    {
        m_camera.transform.DOShakePosition(0.2f, new Vector3(1.5f, 1.5f, 0), 5, 45f);
    }
}
