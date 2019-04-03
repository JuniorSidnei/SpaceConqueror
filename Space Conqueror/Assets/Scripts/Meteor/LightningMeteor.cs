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
        player.AddEffect(new LightningEffect());

        GameObject tempHit = Instantiate(_meteorExplosion, transform.position, Quaternion.identity);
        Destroy(tempHit, 1f);
    }

}
