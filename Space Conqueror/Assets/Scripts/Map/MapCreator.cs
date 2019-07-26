using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapCreator : MonoBehaviour
{
    private RectTransform m_rect;

    private float m_hw;
    private float m_hh;

    private List<MapObject> m_mapObjects;

    private List<GameObject> m_mapPlayer;

    public Transform m_positiontToFollow;
    private void Start()
    {
        GenerateObjects();
    }

    //For player position
    private void LateUpdate()
    {
        MoveOnMap();
    }

    
    
    [ContextMenu("CreateMap")]
    private void GenerateObjects()
    {
        m_mapPlayer = new List<GameObject>();
        m_mapObjects = FindObjectsOfType<MapObject>().ToList();

        foreach (var obj in m_mapObjects)
        {
            DrawOnMap(obj);
        }
    }

    [ContextMenu("ClearMap")]
    private void ClearObjects()
    {
        foreach (MapObject t in m_mapObjects)
        {
#if UNITY_EDITOR
            if (Application.isEditor)
                DestroyImmediate(t);
            else
                Destroy(t);
#endif
        }
        m_mapObjects.Clear();
    }


    //Just to draw
    private void DrawOnMap(MapObject mapObj)
    {
        //cria um objeto com o mesmo nome que o objeto que ele representa, e deixa ele como filho do objeto desse script
        var obj = new GameObject(mapObj.gameObject.name);
        obj.transform.SetParent(transform);
        obj.transform.localScale = new Vector3(mapObj.sizeSprite.x, mapObj.sizeSprite.y, mapObj.sizeSprite.z);
        obj.AddComponent<Image>().sprite = mapObj.mapSprite;
        
        m_mapPlayer.Add(obj);
        
        
        m_rect = GetComponent<RectTransform>();
        m_hw = m_rect.rect.width / 2;
        m_hh = m_rect.rect.height / 2;
        
        //coordenadas da imagem para a imagem principal
        Vector2 MapPos = MapUtilitys.ToMap(mapObj.transform.position,
            new Vector2(-MapUtilitys.mapSize, -MapUtilitys.mapSize),
            new Vector2(MapUtilitys.mapSize, MapUtilitys.mapSize),
            new Vector2(-m_hw, -m_hh),
            new Vector2(m_hw,m_hh));
        
        
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(MapPos.x, MapPos.y);
        
    }
    
    
    //Just to move
    private void MoveOnMap()
    {
        m_rect = GetComponent<RectTransform>();
        m_hw = m_rect.rect.width / 2;
        m_hh = m_rect.rect.height / 2;
        
        foreach (var t in m_mapPlayer)
        {
            if (t.name == "ControlPlayer")
            {
                //coordenadas da imagem para a imagem principal
                Vector2 MapPos = MapUtilitys.ToMap(m_positiontToFollow.position,
                    new Vector2(-MapUtilitys.mapSize, -MapUtilitys.mapSize),
                    new Vector2(MapUtilitys.mapSize, MapUtilitys.mapSize),
                    new Vector2(-m_hw, -m_hh),
                    new Vector2(m_hw, m_hh));
            
                t.GetComponent<RectTransform>().anchoredPosition = new Vector2(MapPos.x, MapPos.y);
            }
        }
        
       
//        //coordenadas da imagem para a imagem principal
//        Vector2 MapPos = MapUtilitys.ToMap(m_mapPlayer.transform.position,
//            new Vector2(-MapUtilitys.mapSize, -MapUtilitys.mapSize),
//            new Vector2(MapUtilitys.mapSize, MapUtilitys.mapSize),
//            new Vector2(-m_hw, -m_hh),
//            new Vector2(m_hw, m_hh));
//            
//        m_mapPlayer.GetComponent<RectTransform>().anchoredPosition = new Vector2(MapPos.x, MapPos.y);
    }
}