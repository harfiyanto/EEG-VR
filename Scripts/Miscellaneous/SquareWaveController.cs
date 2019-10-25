using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareWaveController : MonoBehaviour
{
    private Color altColor = Color.black;
    private Renderer rend; 
    public float frequency = 1.0f;
    private int colour = 0;
    public float startingTime = 5.0f;
    private int increment = 1;
    public float waitTime = 1.0f;
    public float colourOneR = 0.0f; 
    public float colourOneG = 0.0f;
    public float colourOneB = 0.0f;
    public float colourOneA = 1.0f;
    public float colourTwoR = 1.0f;
    public float colourTwoG = 1.0f;
    public float colourTwoB = 1.0f;
    public float colourTwoA = 1.0f;

    void Start ()
    {       
        //Get the renderer of the object so we can access the color
        rend = GetComponent<Renderer>();
        //Set the initial color (0f,0f,0f,0f)
        altColor.g = 0f;         
        altColor.r = 0f;        
        altColor.b = 0f;         
        altColor.a = 0f;
        rend.material.color = altColor;

        //Invoke First Blink Repeatedly
        InvokeRepeating("Blink", startingTime, 1/(2*frequency));
        // print("Frequency: " + frequency);
    }      
 
    void Update() 
    {
        
    }

    // Reset the colour to black
    void ResetColour() 
    {
        altColor.r = colourOneR;
        altColor.g = colourOneG;
        altColor.b = colourOneB;
        altColor.a = colourOneA;
        rend.material.color = altColor;
    }

    void Blink()
    {
        if (colour == 0) {
                altColor.r = colourTwoR;
                altColor.g = colourTwoG;
                altColor.b = colourTwoB;
                altColor.a = colourTwoA;
                colour = 1;
            } else {
                altColor.r = 0.0f;
                altColor.g = 0.0f;
                altColor.b = 0.0f;
                altColor.a = 0.0f;
                colour = 0;
            }
            rend.material.color = altColor;
        // Debug.Log("Color R: " + altColor.r);
    }

}
