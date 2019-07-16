using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameMeteor : MeteorStatus
{
    //Particulas do meteoro
    public GameObject _meteorExplosion;


    //Colisão player meteoro de fogo
    protected override void OnCollision(Collision2D obj)
    {
        var tempHit2 = Instantiate(_bulletHit, obj.contacts[0].point, Quaternion.identity);
    }

    protected override void OnCollisionWithPlayer(ControlPlayer player)
    {
        player.AddEffect(new FlamingEffect());

        //Meteor explosion
        GameObject tempHit = Instantiate(_meteorExplosion, transform.position, Quaternion.identity);
        //wave Explosion
        GameObject tempHit2 = Instantiate(_WaveExplosion, transform.position, Quaternion.Euler(-90, 0, 0));
    }

}
