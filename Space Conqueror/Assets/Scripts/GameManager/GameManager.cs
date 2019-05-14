using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerInfo m_playerInfo;
    
    void Start()
    {
        HudManager.Show(()=> {
            HudManager.Instance.InitializeHudInfo(m_playerInfo);
        });
    }

   
    void Update()
    {
        
    }
}
