using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMeteor : MeteorStatus
{
    public GameObject _meteorExplosion;
    public int m_damage = 300;

    protected override void OnCollision(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("IceShoot"))
        {
            _meteorLife -= obj.gameObject.GetComponent<StandardBullet>()._damage;
            var tempHit2 = Instantiate(_bulletHit, obj.contacts[0].point, Quaternion.identity);
        }
    }

    protected override void OnCollisionWithPlayer(ControlPlayer player)
    {
        player.ApplyDamage(m_damage);
        
        //Meteor explosion
        GameObject tempHit = Instantiate(_meteorExplosion, transform.position, Quaternion.identity);
        //wave Explosion
        GameObject tempHit2 = Instantiate(_WaveExplosion, transform.position, Quaternion.Euler(-90, 0, 0));
    }

}
