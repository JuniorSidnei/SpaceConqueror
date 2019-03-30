using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEffect : PlayerEffects
{

    public override void EnterEffect(ControlPlayer player)
    {
        Debug.Log("ATÉ AGORA TUDO TRANQUILO");
        _speed = 15f;
    }


    public override void RunEffect(ControlPlayer player)
    {
        player.GetComponent<SpriteRenderer>().color = Color.white;
        player.SetSpeed(_speed);
    }

    public override void ExitEffect(ControlPlayer player)
    {
        Debug.Log("FUDEU EFEITO VINDO");
    }
}
