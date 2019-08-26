using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBomb : MonoBehaviour
{
    private int m_damage = 10;


    public GameObject _WaveExplosion;
    public GameObject _Explosion;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ControlPlayer>().ApplyDamage(m_damage);
            SpaceBombExplosion(other);
            HudManager.Instance.HandleOnDamage();
            Destroy(gameObject);
        }

        if (other.gameObject.layer == 11)
        {
            CameraController.Instance.ScreenShake();
            SpaceBombExplosion(other);
        }
    }

    
    private void SpaceBombExplosion(Collision2D other)
    {
        GameObject tempWaveExplosion = Instantiate(_WaveExplosion, other.contacts[0].point, Quaternion.Euler(-90, 0, 0));
        GameObject tempExplosion = Instantiate(_Explosion, transform.position, Quaternion.identity);
        AudioManager.PlaySound("MeteorExplosion");
        Destroy(gameObject);
    }
}
