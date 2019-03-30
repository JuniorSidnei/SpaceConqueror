using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    //Nome de quem fala
    public string name;
    //Sentença de quem fala
    [TextArea(3,40)]
    public string[] sentences;

}
