using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCreator : MonoBehaviour
{
    private RectTransform m_rect;

    private float m_hw;
    private float m_hh;

    private List<GameObject> m_mapObjects;
    
    private void Awake()
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
        var drawObjectsMap = FindObjectsOfType<MapObject>();
        
        m_mapObjects = new List<GameObject>();
        
        foreach (MapObject obj in drawObjectsMap)
        {
            DrawOnMap(obj);
        }
    }

    [ContextMenu("ClearMap")]
    private void ClearObjects()
    {
        foreach (GameObject obj in m_mapObjects)
        {
            DestroyImmediate(obj);
        }
    }


    //Just to draw
    private void DrawOnMap(MapObject mapObj)
    {
        //cria um objeto com o mesmo nome que o objeto que ele representa, e deixa ele como filho do objeto desse script
        var obj = new GameObject(mapObj.gameObject.name);
        obj.transform.SetParent(transform);
        obj.transform.localScale = new Vector3(mapObj.sizeSprite.x, mapObj.sizeSprite.y, mapObj.sizeSprite.z);

        obj.AddComponent<Image>().sprite = mapObj.mapSprite;
        
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
        
//      //Adiciona o objeto na lista
        
        m_mapObjects.Add(obj);
        Debug.Log("Quantos na lista? " + m_mapObjects.Count);
    }

    
    
    //Just to move
    private void MoveOnMap()
    {
        for (int i = 0; i < m_mapObjects.Count; i++)
        {
            if (m_mapObjects[i].name != "ControlPlayer") continue;
            
            //coordenadas da imagem para a imagem principal
            Vector2 MapPos = MapUtilitys.ToMap(m_mapObjects[i].transform.position,
                new Vector2(-MapUtilitys.mapSize, -MapUtilitys.mapSize),
                new Vector2(MapUtilitys.mapSize, MapUtilitys.mapSize),
                new Vector2(-m_hw, -m_hh),
                new Vector2(m_hw, m_hh));



            m_mapObjects[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(MapPos.x, MapPos.y);
        }
    }
}