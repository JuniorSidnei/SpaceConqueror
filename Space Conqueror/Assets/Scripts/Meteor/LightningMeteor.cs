using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningMeteor : MeteorStatus
{

    //Particulas do meteoro
    public GameObject _meteorExplosion;

    //Colisão player meteoro de raio
    protected override void OnCollisionWithPlayer(ControlPlayer player)
    {
        
        //Efeito de raio
        player.AddEffect(new LightningEffect());

        //Meteor explosion
        GameObject tempHit = Instantiate(_meteorExplosion, transform.position, Quaternion.identity);
        Destroy(tempHit, 1f);
        //wave Explosion
        GameObject tempHit2 = Instantiate(_WaveExplosion, transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(tempHit2, 1f);
    }

}
