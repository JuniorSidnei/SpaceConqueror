using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMeteor : MeteorStatus
{
    //Particulas do meteoro
    public GameObject _meteorExplosion;

    //Colisão player meteoro de chelo
    protected override void OnCollisionWithPlayer(ControlPlayer player)
    {
        player.AddEffect(new FreezingEffect());
       
        GameObject tempHit = Instantiate(_meteorExplosion, transform.position, Quaternion.identity);
        Destroy(tempHit, 1f);
    }

}
