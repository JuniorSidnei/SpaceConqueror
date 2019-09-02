﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlamingEffect : PlayerEffects
{

    //Flaminmg state, quando atingido pelo meteoro de FUEGO!
    public override void EnterEffect(ControlPlayer player, PlayerInfo playerInfo)
    {
        _effectDamage = 10;
        player.ApplyDamage(_effectDamage);
    }

    public override void RunEffect(ControlPlayer player, PlayerInfo playerInfo)
    {
       
        //Deixando a sprite vermelha por causa do fogo, deixar a animação de fogo começando aqui
        player.GetComponentInChildren<SpriteRenderer>().DOColor(Color.red, 1.5f);
        player.GetComponentInChildren<SpriteRenderer>().DOFade(0.8f, 1.5f);

        //Contando o tempo
        _effectTimer += Time.deltaTime;

        if (_effectTimer >= 0.5f && _effectTimer <= 0.8f)
            player.ApplyDamage(_effectFlameDamage);
        else if(_effectTimer >= 1f)
            player.RemoveEffect(this);
       

    }
    public override void ExitEffect(ControlPlayer player, PlayerInfo playerInfo)
    {
        player.AddEffect(new NormalEffect());
    }


}
