﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamingEffect : PlayerEffects
{

    //Flaminmg state, quando atingido pelo meteoro de FUEGO!
    public override void EnterEffect(ControlPlayer player)
    {
        _damage = 25;
        player.GetComponent<ControlPlayer>().ApplyDamage(_damage);
    }

    public override void RunEffect(ControlPlayer player)
    {
       
        //Deixando a sprite vermelha por causa do fogo, deixar a animação de fogo começando aqui
        player.GetComponent<SpriteRenderer>().color = Color.red;
    

        //Contando o tempo
        _timer += Time.deltaTime;



        switch (_timer)
        {
            //Priemiro dano do dps
            case 0.5f:
                player.ApplyDamage(_flameDamage);
                break;
            //Segundo dano do dps
            case 1.0f:
                player.ApplyDamage(_flameDamage);
                break;
            //Terceiro dano do dps
            case 1.5f:
                player.ApplyDamage(_flameDamage);
                break;
            //Quarto dano do dps e término do efeito
            case 2.0f:
                _timer = 0;
                player.ApplyDamage(_flameDamage);
                player.RemoveEffect(this);
                break;
        }

    }
    public override void ExitEffect(ControlPlayer player)
    {
        player.AddEffect(new NormalEffect());
    }

}
