using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimateMapObject : MonoBehaviour
{
    
    void Start()
    {
        //MapObjectRotate();
        var size = FindObjectOfType<MapObject>().sizeSprite;
        
       //MapObjectScale(size);
    }
    
    private void MapObjectScale(Vector3 size)
    {
        //Tamanho
        gameObject.transform.DOScale(size - new Vector3(0.01f, 0.01f, 0), 1f)
            .OnComplete(() =>
        {
            gameObject.transform.DOScale(size + new Vector3(0.01f, 0.01f, 0), 1f).OnComplete(()=>
            {
                
                MapObjectScale(size);
            });
        });   

    }

//    private void MapObjectRotate()
//    {
//        //Rotação
//        gameObject.transform.DOLocalRotate(new Vector3(0, 0, rand), 1f)
//            .OnComplete(() =>
//        {
//            gameObject.transform.DOLocalRotate(new Vector3(0, 0, Random.Range()), 1f).OnComplete(MapObjectRotate);    
//        });    
//    }
}
