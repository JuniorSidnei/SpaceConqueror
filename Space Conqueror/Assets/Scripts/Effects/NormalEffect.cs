using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NormalEffect : PlayerEffects
{

    public override void EnterEffect(ControlPlayer player, PlayerInfo playerInfo)
    {
        _effectSpeed = player.GetComponent<Rigidbody2D>().velocity;
    }


    public override void RunEffect(ControlPlayer player, PlayerInfo playerInfo)
    {
        player.GetComponentInChildren<SpriteRenderer>().DOColor(Color.white, 1.5f);
        player.GetComponentInChildren<SpriteRenderer>().DOFade(1, 0.5f);
    }

    public override void ExitEffect(ControlPlayer player, PlayerInfo playerInfo)
    {
       
    }
}
