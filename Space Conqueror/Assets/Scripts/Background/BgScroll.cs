using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BgScroll : MonoBehaviour
{
    private MeshRenderer _rend;
    
    public float _speedX;
    public float _speedY;

    private  ControlPlayer m_player;
   
    void Start()
    {
        _rend = GetComponent<MeshRenderer>();
        m_player = FindObjectOfType<ControlPlayer>();
    }

    
    void Update()
    {
        Vector2 offset = new Vector2(_speedX * Time.deltaTime, _speedY * Time.deltaTime);
        
        offset += m_player.GetComponent<Rigidbody2D>().velocity;

        _rend.material.mainTextureOffset += offset;
    }
}
