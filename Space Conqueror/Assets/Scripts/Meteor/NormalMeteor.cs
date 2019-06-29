using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMeteor : MeteorStatus
{
    public GameObject _meteorExplosion;
    public int m_damage = 300;

    protected override void OnCollisionWithPlayer(ControlPlayer player)
    {

        player.ApplyDamage(m_damage);
        //Meteor explosion
        GameObject tempHit = Instantiate(_meteorExplosion, transform.position, Quaternion.identity);
        Destroy(tempHit, 1f);
        //wave Explosion
        GameObject tempHit2 = Instantiate(_WaveExplosion, transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(tempHit2, 1f);
    }

}
