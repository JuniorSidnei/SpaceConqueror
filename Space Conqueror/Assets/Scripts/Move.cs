using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float m_speed = 100f;
    
    
    void Update()
    {
        transform.position = new Vector3(m_speed * Time.deltaTime, 0,0);
    }
}
