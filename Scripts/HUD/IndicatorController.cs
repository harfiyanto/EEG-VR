using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    private Renderer rend;
    private Color altColor = Color.black;
    public int activate = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the renderer of the object so we can access the color
        rend = GetComponent<Renderer>();

        // Set the initial color (0f,0f,0f,0f)
        altColor.r = 0f;
        altColor.g = 0f;         
        altColor.b = 0f;         
        altColor.a = 0f;
        rend.material.color = altColor;
    }

    // Update is called once per frame
    void Update()
    {
        // Activated, change the colour to green
        if (activate == 1) {
            altColor.r = 0f;
            altColor.g = 0.8f;         
            altColor.b = 0f;         
            altColor.a = 1.0f;
            rend.material.color = altColor;
        } else {
            altColor.r = 0f;
            altColor.g = 0f;         
            altColor.b = 0f;         
            altColor.a = 0f;
            rend.material.color = altColor;
        }
    }

}
