using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEffect : PlayerEffects
{

    public override void EnterEffect(ControlPlayer player, PlayerInfo playerInfo)
    {
        _effectSpeed = 700;
    }


    public override void RunEffect(ControlPlayer player, PlayerInfo playerInfo)
    {
        player.GetComponent<SpriteRenderer>().color = Color.white;
        playerInfo.Speed = _effectSpeed;
    }

    public override void ExitEffect(ControlPlayer player, PlayerInfo playerInfo)
    {
       
    }
}
