using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] private string m_menuScene;
    [SerializeField] private string m_gameScene;
    
    public void Show() {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnclickResumeGame() {
        gameObject.SetActive(false);
        HudManager.Instance.HandlePlaying();
        Time.timeScale = 1f;
    }

    public void OnClickExitGame() {
        SceneManager.LoadScene(m_menuScene);
    }

    public void OmClickRestartGame() {
        SceneManager.LoadScene(m_gameScene);
    }
    
}
