using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerEffects
{
    public int _effectDamage;
    protected float _effectTimer = 0f;
    protected float _effectSpeed;
    protected int _effectFlameDamage = 5;
    
    
    //Entrando no efeito
    public abstract void EnterEffect(ControlPlayer player, PlayerInfo playerInfo);

    //Dentro do efeito
    public abstract void RunEffect(ControlPlayer player, PlayerInfo playerInfo);

    //Saindo do efeito
    public abstract void ExitEffect(ControlPlayer player, PlayerInfo playerInfo);
}
