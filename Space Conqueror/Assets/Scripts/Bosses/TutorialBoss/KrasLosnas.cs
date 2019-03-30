using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrasLosnas : MonoBehaviour
{
    private Animator _anim;

    public int _krasLife = 1000;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //_timerDash += Time.deltaTime;

        //if (_timerDash >= 10f)
        //{

        //    _anim.SetBool("GoDash", true);
        //    _timerDash = 0;
        //}
       
    }
}
