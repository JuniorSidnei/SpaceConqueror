using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BubbleButton : Button
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        AudioManager.PlaySound("ShowHideInteractables");
    }


    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        transform.DOComplete();
        transform.DOPunchScale(Vector3.one * .01f, .25f).SetUpdate(true);
        AudioManager.PlaySound("ShowHideInteractables");
    }
}