using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroll : MonoBehaviour
{
    private MeshRenderer _rend;
    
    public float _speedX;
    public float _speedY;

    public  ControlPlayer m_player;
   
    void Start()
    {
        _rend = GetComponent<MeshRenderer>();
    }

    
    void Update()
    {
        Vector2 offset = new Vector2(_speedX * Time.deltaTime, 0);
        
        offset.x += m_player._moveVelocity.normalized.magnitude;
        
        _rend.material.mainTextureOffset += offset;
    }
}
