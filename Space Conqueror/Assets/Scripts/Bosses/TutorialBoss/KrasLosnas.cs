using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class KrasLosnas : MonoBehaviour
{
    public delegate void BossEvent();

    //public static event BossEvent BossIsDead;


    private Animator m_anim;

    [FormerlySerializedAs("_krasLife")] public int m_life;

    public Transform _spawnShoot;

    public GameObject m_ptcShoot;

    public GameObject _projectile;

    public GameObject m_ptcDie;

    private int _isOverHeatCount = 0;

    private bool _isOverHeat = false;

    private float _shootTimer = 0f;

    private float _overHeatTimer = 0f;

    private int m_bodyDamage = 30;

    //public SpeechScriptable m_speechBoss;

    private static bool m_alive = true;
    
    void Start()
    {
        m_anim = GetComponent<Animator>();
    }


    void Update()
    {
        
        if (_isOverHeat)
        {
            _overHeatTimer += Time.deltaTime;

            if (_overHeatTimer >= 3f)
            {
                _isOverHeat = false;
                m_anim.SetBool("Overheat", false);
                _overHeatTimer = 0;
            }
        }

        //Se a vida do boss for menor que metade e ele não estiver sobrecarregado, pode atirar
        if (m_life <= 1500 && _isOverHeat == false)
        {
          
            m_anim.SetBool("BurstOn", true);
            _shootTimer += Time.deltaTime;

            if (_shootTimer >= 0.2f)
            {
                ShootAndOverHeat();
                _shootTimer = 0f;
            }
        }
        
        //Se morrer ativa animação de morte e particula de explosão
        if (m_life <= 0)
        {
            
            Instantiate(m_ptcDie, transform.position, Quaternion.identity);
            Destroy(gameObject);
            //BossIsDead?.Invoke();
            m_alive = false;
        }
    }

    public static bool isBossAlive => m_alive;


    void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.layer == 11)
        {
           
            m_life -= obj.gameObject.GetComponent<StandardBullet>()._damage;
            Destroy(obj.gameObject);
        }
    }

    public void ShootAndOverHeat()
    {
       
        GameObject tempExp = Instantiate(m_ptcShoot, _spawnShoot.position, Quaternion.identity, transform);
        
        GameObject tempprojectile = Instantiate(_projectile, _spawnShoot.position, Quaternion.identity, transform);
        tempprojectile.transform.right = Vector3.right;
        Destroy(tempprojectile, 4f);

        _isOverHeatCount++;
        
        if (_isOverHeatCount >= 40)
        {
              _isOverHeat = true;
            _isOverHeatCount = 0;
            m_anim.SetBool("Overheat", true);
   
        }
    }

    public int GetBodyDamage()  {  return m_bodyDamage;  }
}
