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

    private bool m_burstOn;

    private Animator m_anima;

    private static bool m_alive = true;
    
    private void Start()
    {
        m_anima = GetComponent<Animator>();
    }

    void Update()
    {
        SetAnimations();

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
        if (m_life <= 3500 && _isOverHeat == false)
        {
            
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
            BossDiyng();
        }
    }

    private void  BossDiyng()
    {
        StartCoroutine(SlowEffect());
        Time.timeScale = 1f;
        EventHandler.Instance.CallDialogueAndEvent();
    }

    IEnumerator SlowEffect()
    {
        Time.timeScale = 0.2f;
        AudioManager.PlaySound("BossDie");
        Instantiate(m_ptcDie, transform.position, Quaternion.identity);
        Destroy(gameObject);
        m_alive = false;
        yield return new WaitForSeconds(2f);
    }

    public static bool isBossAlive => m_alive;
    
    //Colisão com tiro do jogador
    void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.layer == 11)
        {
            
            var tempSpt = gameObject.GetComponent<SpriteRenderer>().material;
            tempSpt.DOColor(Color.red, 0.5f);
            tempSpt.DOFade(1, 0.5f).OnComplete(()=> tempSpt.DOColor(Color.white, 0.1f));
           
            
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
            AudioManager.PlaySound("AlertBoss");
        }
    }

    public void ActiveBoss()
    {
        FindObjectOfType<KrasLosnas>().GetComponentInChildren<Animator>().SetTrigger("BossOn");
        MeteorBehavior.SetMeteorOver(false);
        AudioManager.FadeOut("MainTheme", 2f);
        AudioManager.PlaySound("KrasLonasTheme");
        CameraController.Instance.m_minX = 0;
    }

    private void SetAnimations()
    {
        m_anima.SetBool("Overheat", IsOverHeat);
        m_anima.SetBool("BurstOn", BurstOn);
    }
    
    public int GetBodyDamage { get; } = 30;

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
