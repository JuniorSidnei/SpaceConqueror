using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    private Camera m_camera;

    public Transform m_target;

    public float m_smoothTime;
    
    private Rigidbody2D m_playerRb;
    
    public static CameraController Instance;

    private void Start()
    {
        Instance = this;
        m_camera = GetComponent<Camera>();
        m_playerRb = FindObjectOfType<ControlPlayer>().GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 target = m_target.position + new Vector3(m_playerRb.velocity.x, m_playerRb.velocity.y, 0) * m_smoothTime;
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.x, target.y, transform.position.z), 0.2f);

    }

//    private void Update()
//    {
//        Vector3 viewPos = Camera.main.WorldToViewportPoint(m_target.position);
//
//        if(viewPos.x > 1f)
//            m_target.position = Vector3.Lerp(m_target.position, viewPos, 0.1f);
//        else if(viewPos.x < 0)
//        {
//            m_target.position = Vector3.Lerp(m_target.position, viewPos, 0.1f);
//        }
//        
//        if(viewPos.y > 1f)
//            m_target.position = Vector3.Lerp(m_target.position, viewPos, 0.1f);
//        else if(viewPos.y < 0)
//        {
//            m_target.position = Vector3.Lerp(m_target.position, viewPos, 0.1f);
//        }
//    }

    public void ScreenShake()
    {
        m_camera.transform.DOShakePosition(0.2f, new Vector3(1.5f, 1.5f, 0), 5, 45f);
    }
}
