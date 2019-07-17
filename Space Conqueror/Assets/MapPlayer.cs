using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayer : MapObject
{
    public Transform _playerPos;

    void Update()
    {
        transform.position = _playerPos.position;
    }
}
