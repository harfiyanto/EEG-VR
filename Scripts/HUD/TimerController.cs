using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    float time = 0.0f;
 
    Text ttext;
 
    void Awake ()
    {
        ttext = GetComponent<Text> ();
    }
     
    void Update()
    {
        time += Time.deltaTime;
        ttext.text = "Time: " + time;
    }
}
