using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateObject : MonoBehaviour
{
    public float Speed;
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Speed * Time.deltaTime));
    }
}
