using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MiningMeteor : MonoBehaviour
{

    public float m_mineralsAmount;

    public ParticleSystem m_effect;
 

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player") && m_mineralsAmount > 0)
        {
            m_mineralsAmount --;
            //obj.GetComponent<ControlPlayer>().GetMinerals();
            
            if(m_mineralsAmount <= 0)
            {
                m_mineralsAmount = 0;
                var emissionModule = m_effect.emission;
                emissionModule.rateOverTime = 5;
            }
        }
    }
}
