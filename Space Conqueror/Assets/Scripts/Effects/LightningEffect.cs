using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEffect : PlayerEffects
{
    
    public override void EnterEffect(ControlPlayer player)
    {
        Debug.Log("RAIO NO CU");
        _damage = 30;
        _speed = 0;

        player.GetComponent<ControlPlayer>().ApplyDamage(_damage);

    }

    public override void RunEffect(ControlPlayer player)
    {

        //Começar animação de raio
        player.GetComponent<SpriteRenderer>().color = Color.yellow;
        player.SetSpeed(_speed);

        //Contando o tempo
        _timer += Time.deltaTime;
        Debug.Log("tempo raio:" + _timer);

        //Terminar animação de raio
        if (_timer >= 1f)
        {
            _timer = 0;
            player.RemoveEffect(this);
        }
    }

    public override void ExitEffect(ControlPlayer player)
    {
        player.AddEffect(new NormalEffect());
        Debug.Log("CHEGA DE RAIO");
    }
}
