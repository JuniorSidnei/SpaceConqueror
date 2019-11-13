using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ArmoryManager : MonoBehaviour
{
    private RectTransform m_rect;
    public TextMeshProUGUI RecoveryAmount;

    public TextMeshProUGUI Recovery
    {
        get => RecoveryAmount;
        set => RecoveryAmount = value;
    }

    private void Start()
    {
        m_rect = GetComponent<RectTransform>();
    }

    public void Show()
    {
        AudioManager.PlaySound("MapShowUp");
        m_rect.DOAnchorPos(new Vector3(-350, -560, 0),1f).OnComplete(() =>
        {
            GameManager.Instance.ArmoryController++;
        });
    }

    public void Hide()
    {
        AudioManager.PlaySound("MapShowDown");
        m_rect.DOAnchorPos(new Vector3(-1248,-560,0), 1f).OnComplete(() =>
        {
            GameManager.Instance.ArmoryController--;
            HudManager.Instance.HandlePlaying();  
        });
    }
}
