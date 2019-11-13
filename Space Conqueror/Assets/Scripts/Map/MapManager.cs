using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MapManager : MonoBehaviour
{
  //Show Map
  public void Show()
  {
    AudioManager.PlaySound("MapShowUp");
    gameObject.SetActive(true);
    gameObject.gameObject.transform.DOPunchScale(new Vector3(0.2f,0.2f,0),1f,1,0).OnComplete(() =>
    {
      GameManager.Instance.MapController++;
    });
  }

  //Hide Map
  public void Hide()
  {
    AudioManager.PlaySound("MapShowDown");
    gameObject.gameObject.transform.DOScale(new Vector3(0, 0, 0), 1f).OnComplete(() =>
    {
      gameObject.SetActive(false);
      GameManager.Instance.MapController--;
      HudManager.Instance.HandlePlaying();
      gameObject.gameObject.transform.DOScale(new Vector3(37.92f, 37.92f, 0), 0.1f);
    });
  }
}
