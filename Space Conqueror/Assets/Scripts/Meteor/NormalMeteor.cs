using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMeteor : MeteorStatus
{
    public GameObject _meteorExplosion;

    protected override void OnCollisionWithPlayer(ControlPlayer player)
    { 
        GameObject tempHit = Instantiate(_meteorExplosion, transform.position, Quaternion.identity);
        Destroy(tempHit, 1f);
    }

}
