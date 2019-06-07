using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private ControlPlayer m_player; 
    private Animator m_animator;
    
    void Awake()
    {
        m_animator  = GetComponent<Animator>();  
    }
    
    void Start()
    {
        m_player = GetComponentInParent<ControlPlayer>();
    }

    
    void Update()
    {
        m_animator.SetFloat("Iddle", m_player._moveVelocity.sqrMagnitude);
    }
}
