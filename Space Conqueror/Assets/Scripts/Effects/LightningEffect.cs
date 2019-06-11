using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightningEffect : PlayerEffects
{
    
    public override void EnterEffect(ControlPlayer player, PlayerInfo playerInfo)
    {
        
        _effectDamage = 30;
        _effectSpeed = 0;

        player.ApplyDamage(_effectDamage);

    }

    public override void RunEffect(ControlPlayer player, PlayerInfo playerInfo)
    {

        //Começar animação de raio
        player.GetComponentInChildren<SpriteRenderer>().DOColor(Color.yellow, 1.5f);
        player.GetComponentInChildren<SpriteRenderer>().DOFade(0.8f, 1.5f);
        playerInfo.Speed = _effectSpeed;

        //Contando o tempo
        _effectTimer += Time.deltaTime;
       

        //Terminar animação de raio
        if (_effectTimer >= 1f)
        {
            _effectTimer = 0;
            player.RemoveEffect(this);
        }
    }

    public override void ExitEffect(ControlPlayer player, PlayerInfo playerInfo)
    {
        player.AddEffect(new NormalEffect());
        
    }
}
