using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamingEffect : PlayerEffects
{

    //Flaminmg state, quando atingido pelo meteoro de FUEGO!
    public override void EnterEffect(ControlPlayer player)
    {
        _damage = 0.05f;
        player.ApplyDamage(_damage);
    }

    public override void RunEffect(ControlPlayer player)
    {
       
        //Deixando a sprite vermelha por causa do fogo, deixar a animação de fogo começando aqui
        player.GetComponent<SpriteRenderer>().color = Color.red;
    

        //Contando o tempo
        _timer += Time.deltaTime;

        if (_timer >= 0.5f && _timer <= 0.8f)
            player.ApplyDamage(_flameDamage);
        else if(_timer >= 1f)
            player.RemoveEffect(this);
       

    }
    public override void ExitEffect(ControlPlayer player)
    {
        player.AddEffect(new NormalEffect());
    }


}
