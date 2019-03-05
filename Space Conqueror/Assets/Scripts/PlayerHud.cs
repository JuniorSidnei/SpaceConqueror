using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHud : MonoBehaviour
{

    public ControlPlayer _player;

    //Barra de vida do jogador
    public Image barLife;
    public TextMeshProUGUI _lifeText;

    void Start()
    {
        _lifeText.text = ("" + _player._life);
        _player.DamageEvent += OnDamage;
        
    }

    
    private void OnDisable()
    {

        _player.DamageEvent -= OnDamage;
    }


    public void OnDamage(float life)
    {
        
        _lifeText.text = ("" + _player._life);
        barLife.fillAmount = life;
    }
}
