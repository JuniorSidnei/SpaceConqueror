using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHud : MonoBehaviour
{

//    public ControlPlayer _player;
//
//    //Barra de vida do jogador
//    public Image barLife;
//    public TextMeshProUGUI _lifeText;
//    public Button _repairKit;
//
//
//    void Start()
//    {
//        _lifeText.text = ("" + _player._life + "/ " + _player._maxLife);
//        _player.DamageEvent += OnDamage;
//       
//    }
//
//    void Update ()
//    {
//        if (_player._repairUsed == false)
//            OnRepairKit();
//    }
//    
//
//    private void OnDisable()
//    {
//
//        _player.DamageEvent -= OnDamage;
//        
//    }
//
//
//    public void OnDamage(float life)
//    {
//        //Atualizando a vida com os valores e a vida maxima do lado
//        _lifeText.text = ("" + _player._life + "/ " + _player._maxLife);
//        barLife.fillAmount = life;
//    }
//
//    public void OnRepairKit()
//    {
//        //Desativando botão de reparo depois de usado
//        _repairKit.enabled = false;
//      
//    }
}
