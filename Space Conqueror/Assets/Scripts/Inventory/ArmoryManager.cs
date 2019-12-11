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
        gameObject.SetActive(false);
    }

    public void Show()
    {
        AudioManager.PlaySound("MapShowUp");
        gameObject.SetActive(true);
        m_rect.DOAnchorPos(new Vector3(400, -45, 0),1f).SetEase(Ease.OutBack);
    }

    public void Hide()
    {
        AudioManager.PlaySound("MapShowDown");
        m_rect.DOAnchorPos(new Vector3(-256,-110,0), 1f).OnComplete(() =>
        {
            HudManager.Instance.HandlePlaying();
            gameObject.SetActive(false);
        });
    }
}
