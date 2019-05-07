using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyVFX : MonoBehaviour
{
    
    void Start()
    {
        //Destruindo efetivamente a particula após o efeito
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration + GetComponent<ParticleSystem>().main.startLifetime.constantMax);
    }
}
