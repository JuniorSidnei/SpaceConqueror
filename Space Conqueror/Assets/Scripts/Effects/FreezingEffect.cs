﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingEffect : PlayerEffects
{

    public override void EnterEffect(ControlPlayer player)
    {
        _effectSpeed = 400;
        _effectDamage = 50;

        player.ApplyDamage(_effectDamage);
    }

    public override void RunEffect(ControlPlayer player)
    {
       
        //Debuff visual e de velocidade, começar animação de gelo
        player.GetComponent<SpriteRenderer>().color = Color.blue;
        m_playerInfo.Speed = _effectSpeed;

        //Tirando o tempo de dois segundos
        _effectTimer += Time.deltaTime;
       

        //Enquanto for maior que o tempo vai ficar com debuff, terminar animação de gelo
        if (_effectTimer >= 2f)
        {
            _effectTimer = 0;
            player.RemoveEffect(this);
        }
    }

    public override void ExitEffect(ControlPlayer player)
    {
        player.AddEffect(new NormalEffect());
    }
}
