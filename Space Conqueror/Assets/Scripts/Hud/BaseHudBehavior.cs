using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHudBehavior : MonoBehaviour
{
    //Lista de objetos que devem dizer o que carregar na hora que forem chamados
    [SerializeField]
    private List<GameObject> m_conversationMode, m_playingMode;

    //HUD com vida, ataques do jogador e itens de cura
    public virtual void HandlePlaying()
    {
        ActiveList(m_conversationMode, false);
        ActiveList(m_playingMode, true);
    }

    //HUD para dialogos
    public virtual void HandleConversation()
    {
        ActiveList(m_playingMode, false);
        ActiveList(m_conversationMode, true);
    }


    public virtual void SetPlayerInfo(PlayerInfo playerInfo)
    { }
    
    
    //Ativar todos os objetos da lista para a HUD
    private void ActiveList(List<GameObject> list, bool active)
    {
        list.ForEach(f => f.SetActive(active));
    }
}
