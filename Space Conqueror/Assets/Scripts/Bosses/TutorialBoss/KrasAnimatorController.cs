using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrasAnimatorController : MonoBehaviour
{
    private KrasLosnas m_KrasLosnas;

    private Animator m_anima;
    
    void Start()
    {
        m_KrasLosnas = GetComponentInParent<KrasLosnas>();
        m_anima = GetComponent<Animator>();
    }

    
    void Update()
    {
        m_anima.SetFloat("Iddle", m_KrasLosnas.m_distance.sqrMagnitude);
        m_anima.SetBool("Overheat", m_KrasLosnas.IsOverHeat);
        m_anima.SetBool("BurstOn", m_KrasLosnas.BurstOn);
    }
}
