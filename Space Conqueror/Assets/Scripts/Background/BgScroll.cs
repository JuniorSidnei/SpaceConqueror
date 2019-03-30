using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroll : MonoBehaviour
{
    private Renderer _rend;
    public float _speed;


   
    void Start()
    {
        _rend = GetComponent<Renderer>();
    }

    
    void Update()
    {
        Vector2 offset = new Vector2(_speed * Time.deltaTime, 0);

        _rend.material.mainTextureOffset += offset;
    }
}
