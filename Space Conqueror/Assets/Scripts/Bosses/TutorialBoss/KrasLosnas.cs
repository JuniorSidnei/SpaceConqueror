using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class KrasLosnas : MonoBehaviour
{
    [Header("KrasLosnas Status")]
    [FormerlySerializedAs("_krasLife")] public int m_life;

    [Header("Shoot settings")]
    public Transform _spawnShoot;

    public GameObject m_ptcShoot;

    public GameObject _projectile;

    public GameObject m_ptcDie;

    
    private int _isOverHeatCount = 0;

    private bool _isOverHeat = false;

    private float _shootTimer = 0f;

    private float _overHeatTimer = 0f;

    private int m_bodyDamage = 30;

    private bool m_burstOn;

    

    private static bool m_alive = true;

    [Header("Distance and who to follow")]
    public Transform m_target;
    [HideInInspector]
    public Vector3 m_distance;
    public float m_maxDistance;
  
    void Update()
    {
        
//        m_distance = transform.position - m_target.position;
//        m_distance.y = 0;
//        transform.position = m_target.position + m_distance.normalized * m_maxDistance;
        
        if (_isOverHeat)
        {
            _overHeatTimer += Time.deltaTime;

            if (_overHeatTimer >= 3f)
            {
                _isOverHeat = false;
                _overHeatTimer = 0;
            }
        }

        //Se a vida do boss for menor que metade e ele não estiver sobrecarregado, pode atirar
        if (m_life <= 1500 && _isOverHeat == false)
        {
            //m_anim.SetBool("BurstOn", true);
            m_burstOn = true;
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
            StartCoroutine(BossDiyng());
        }
    }

    IEnumerator BossDiyng()
    {
        Time.timeScale = 0.2f;
        AudioManager.PlaySound("BossDie");
        Instantiate(m_ptcDie, transform.position, Quaternion.identity);
        Destroy(gameObject);
        m_alive = false;
        yield return new WaitForSeconds(2f);
        EventHandler.Instance.CallDialogueAndEvent();
        Time.timeScale = 0.2f;
    }

    public static bool isBossAlive => m_alive;
    
    void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.layer == 11)
        {
           AudioManager.PlaySound("BulletBossCollision");
           m_life -= obj.gameObject.GetComponent<StandardBullet>()._damage;
           Destroy(obj.gameObject);
        }
    }

    public void ShootAndOverHeat()
    {
       
        AudioManager.PlaySound("BossShoot");
        GameObject tempExp = Instantiate(m_ptcShoot, _spawnShoot.position, Quaternion.identity, transform);
        
        GameObject tempprojectile = Instantiate(_projectile, _spawnShoot.position, Quaternion.identity);
        tempprojectile.transform.right = Vector3.right;
        Destroy(tempprojectile, 4f);

        _isOverHeatCount++;
        
        if (_isOverHeatCount >= 40)
        {
            _isOverHeat = true;
            _isOverHeatCount = 0;
           // m_anim.SetBool("Overheat", true);
            AudioManager.PlaySound("AlertBoss");
        }
    }

    public void ActiveBoss()
    {
        FindObjectOfType<KrasLosnas>().GetComponentInChildren<Animator>().SetTrigger("BossOn");
        MeteorBehavior.SetMeteorOver(false);
        AudioManager.FadeOut("MainTheme", 2f);
        AudioManager.PlaySound("KrasLonasTheme");
    }

    public int GetBodyDamage
    {
        get => m_bodyDamage;
    }

    public int GetLife
    {
        get => m_life;
    }

    public int IsOverHeatCount
    {
        get => _isOverHeatCount;
        set => _isOverHeatCount = value;
    }

    public bool IsOverHeat
    {
        get => _isOverHeat;
        set => _isOverHeat = value;
    }

    public float OverHeatTimer
    {
        get => _overHeatTimer;
        set => _overHeatTimer = value;
    }
    
    public bool BurstOn
    {
        get => m_burstOn;
        set => m_burstOn = value;
    }
}
