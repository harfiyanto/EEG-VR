using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESphereController : MonoBehaviour
{
    public float frequency = 1.0f;
    private int size = 0;
    public float startingTime = 5.0f;
    public float scaleValue1 = 0.4f;
    public float scaleValue2 = 0.4f;


    void Start ()
    {       
        InvokeRepeating("Gulliver", startingTime, 1/(2*frequency));
    }      

    void Gulliver() 
    {
        Vector3 scale = transform.localScale;
        if (size == 0) {
            scale.x = scaleValue1;
            scale.y = scaleValue1;
            scale.z = scaleValue1;
            size = 1;
        } else {
            scale.x = scaleValue2;
            scale.y = scaleValue2;
            scale.z = scaleValue2;
            size = 0;
        }
        transform.localScale = scale;
    }

}
