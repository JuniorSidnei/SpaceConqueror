using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region variables
    public PlayerInfo m_playerInfo;
    
    #endregion
    
    #region methods
    void Start()
    {
        HudManager.Show(()=>
        { 
            HudManager.Instance.InitializeHudPlayingInfo(m_playerInfo);
        });
    }

   
    void Update()
    {
        
    }
    #endregion
}
