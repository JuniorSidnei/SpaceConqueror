using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerEffects
{
    public float _damage;
    public float _timer = 0f;
    public float _speed;
    public int _flameDamage = 5;

    //Entrando no efeito
    public abstract void EnterEffect(ControlPlayer player);

    //Dentro do efeito
    public abstract void RunEffect(ControlPlayer player);

    //Saindo do efeito
    public abstract void ExitEffect(ControlPlayer player);
}
