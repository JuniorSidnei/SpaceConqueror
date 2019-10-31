using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningMeteor : MeteorStatus
{

    //Particulas do meteoro
    public GameObject _meteorExplosion;

    //Colisão player meteoro de raio
    protected override void OnCollision(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Standard"))
        {
            _meteorLife -= obj.gameObject.GetComponent<StandardBullet>()._damage;
            var tempHit2 = Instantiate(_bulletHit, obj.contacts[0].point, Quaternion.identity);
        }
    }

    protected override void OnCollisionWithPlayer(ControlPlayer player)
    {
        //Efeito de raio
        player.AddEffect(new LightningEffect());

        //Meteor explosion
        GameObject tempHit = Instantiate(_meteorExplosion, transform.position, Quaternion.identity);
        //wave Explosion
        GameObject tempHit2 = Instantiate(_WaveExplosion, transform.position, Quaternion.Euler(-90, 0, 0));
    }

}
