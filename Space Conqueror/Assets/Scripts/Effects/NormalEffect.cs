using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEffect : PlayerEffects
{

    public override void EnterEffect(ControlPlayer player)
    {
        _effectSpeed = 700;
    }


    public override void RunEffect(ControlPlayer player)
    {
        player.GetComponent<SpriteRenderer>().color = Color.white;
        m_playerInfo.Speed = _effectSpeed;
    }

    public override void ExitEffect(ControlPlayer player)
    {
       
    }
}
