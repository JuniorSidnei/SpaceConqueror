using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEffect : PlayerEffects
{
    
    public override void EnterEffect(ControlPlayer player)
    {
        
        _effectDamage = 30;
        _effectSpeed = 0;

        player.ApplyDamage(_effectDamage);

    }

    public override void RunEffect(ControlPlayer player)
    {

        //Começar animação de raio
        player.GetComponent<SpriteRenderer>().color = Color.yellow;
        m_playerInfo.Speed = _effectSpeed;

        //Contando o tempo
        _effectTimer += Time.deltaTime;
       

        //Terminar animação de raio
        if (_effectTimer >= 1f)
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
