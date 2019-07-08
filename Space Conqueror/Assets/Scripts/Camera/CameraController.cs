using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    private Camera m_camera;

    public Transform m_target;

    public float m_smoothTime;

    public float m_zoomOut;

    public float m_zoomIn;

    public float m_smoothZoomTime;
    
    private Rigidbody2D m_playerRb;
    
    [FormerlySerializedAs("m_timeToMaxSpeed")] [SerializeField]
    private float m_timeToMaxZoom;

    private float m_timeZooming;

    public AnimationCurve m_ZoomCurve;
    
    private Vector3 velocity = Vector3.zero;
    
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
        //transform.position = Vector3.Lerp(transform.position, new Vector3(target.x, target.y, transform.position.z), 0.2f);
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.x, target.y, transform.position.z), ref velocity, 0.2f);
    }

    public void ZoomOut(bool accelerating)
    {
        if (accelerating)
        {
            m_timeZooming += Time.deltaTime;

            float acce = m_ZoomCurve.Evaluate(m_timeZooming / m_timeToMaxZoom);

            m_camera.orthographicSize = Mathf.Lerp(m_camera.orthographicSize, m_zoomOut, 0.05f * acce);
            m_smoothTime = m_smoothZoomTime;
        }
        else
        {
            m_camera.orthographicSize = Mathf.Lerp(m_camera.orthographicSize, m_zoomIn, 0.05f);
            m_timeZooming = 0;
        }
    }
    
    public void ScreenShake()
    {
        m_camera.transform.DOShakePosition(0.2f, new Vector3(1.5f, 1.5f, 0), 5, 45f);
    }
}
