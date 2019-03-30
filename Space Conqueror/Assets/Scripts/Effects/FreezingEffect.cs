using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingEffect : PlayerEffects
{

    public override void EnterEffect(ControlPlayer player)
    {

        Debug.Log("GELINHO NO CU RAPÁ!");
        _speed = 5f;
    }

    public override void RunEffect(ControlPlayer player)
    {
       
        //Debuff visual e de velocidade, começar animação de gelo
        player.GetComponent<SpriteRenderer>().color = Color.blue;
        player.SetSpeed(_speed);

        //Tirando o tempo de dois segundos
        _timer += Time.deltaTime;

        //Enquanto for maior que o tempo vai ficar com debuff, terminar animação de gelo
        if (_timer >= 2f)
        {
            _timer = 0;
            player.RemoveEffect(this);
        }
    }

    public override void ExitEffect(ControlPlayer player)
    {
        Debug.Log("VIROU ÁGUA");
    }
}
