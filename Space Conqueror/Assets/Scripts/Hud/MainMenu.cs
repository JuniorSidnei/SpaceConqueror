using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject m_menuObj;
    [SerializeField] private GameObject m_helpObj;
    [SerializeField] private Image m_transition;
    private CanvasGroup m_menuCanvasGroup;
    private CanvasGroup m_menuHelpCanvasGroup;
    
    private void Start() {
        
        AudioManager.FadeIn("Menu", 1, .5f);
        m_menuCanvasGroup = m_menuObj.GetComponent<CanvasGroup>();
        m_menuHelpCanvasGroup = m_helpObj.GetComponent<CanvasGroup>();
        
         m_menuCanvasGroup.DOFade(1, 1f);
    }

    public void OnClickStart() {
        AudioManager.PlaySound("Start");
        m_transition.DOFade(1, 2f).OnComplete(() => { SceneManager.LoadScene("Game"); });
    }

    public void OnClickClose() {
        AudioManager.PlaySound("Quit/Back");
        Application.Quit();
    }

    public void OnClickHelp() {
        AudioManager.PlaySound("Help");
        ((RectTransform)m_menuObj.transform).DOLocalMoveX(-820, 1f);
        ((RectTransform)m_helpObj.transform).DOLocalMoveX(0, 1f);
    }

    public void OnClickBackHelp() {
        AudioManager.PlaySound("Quit/Back");
        ((RectTransform)m_menuObj.transform).DOLocalMoveX(0, 1f);
        ((RectTransform)m_helpObj.transform).DOLocalMoveX(850, 1f);
    }
}
