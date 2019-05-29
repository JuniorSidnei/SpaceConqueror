using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrasLosnas : MonoBehaviour
{
    private Animator _KrasAnim;

    public int _krasLife = 3000;

    public Transform _spawnShoot;

    public GameObject m_ptcShoot;

    public GameObject _projectile;

    private int _isOverHeatCount = 0;

    private bool _isOverHeat = false;

    private float _shootTimer = 0f;

    private float _overHeatTimer = 0f;

    private int m_bodyDamage = 30;

    public SpeechScriptable m_speechBoss;
    
    void Start()
    {
        _KrasAnim = GetComponent<Animator>();
    }


    void Update()
    {
        
        if (_isOverHeat)
        {
            _overHeatTimer += Time.deltaTime;

            if (_overHeatTimer >= 3f)
            {
                _isOverHeat = false;
                _KrasAnim.SetBool("Overheat", false);
                _overHeatTimer = 0;
            }
        }

        //Se a vida do boss for menor que metade e ele não estiver sobrecarregado, pode atirar
        if (_krasLife <= 1500 && _isOverHeat == false)
        {
          
            _KrasAnim.SetBool("BurstOn", true);
            _shootTimer += Time.deltaTime;

            if (_shootTimer >= 0.2f)
            {
                ShootAndOverHeat();
                _shootTimer = 0f;
            }
        }
        
    }


    void OnEnable()
    {
        MeteorBehavior.EndMeteorWave += CallBossDialogue;
    }

    void OnDisable()
    {
        MeteorBehavior.EndMeteorWave -= CallBossDialogue;
    }

    private void CallBossDialogue()
    {
        GameManager.Instance.SetMeteorOver(true);
        GameManager.Instance.CallDialogue(m_speechBoss);
    }

    
    void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.layer == 11)
        {
           
            _krasLife -= obj.gameObject.GetComponent<StandardBullet>()._damage;
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
            _KrasAnim.SetBool("Overheat", true);
   
        }
    }

    public int GetBodyDamage()  {  return m_bodyDamage;  }
}
