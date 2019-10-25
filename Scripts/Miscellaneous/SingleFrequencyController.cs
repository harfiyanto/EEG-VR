using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFrequencyController : MonoBehaviour
{
    private Renderer rend;                  // Renderer for colour
    private Color altColor = Color.black;   // Default colour
    public float frequency = 1.0f;          // Default trequency
    public float startingTime = 3.0f;       // Defaut starting time
    public int increment = 20;              // Number of increments in a period (How smooth the blinking is)
    private int colour = 0;                 // Variable to indicate the current colour
    private int i = 1;                      // Variable to help looping increments
    public float colourOneR = 0.0f; 
    public float colourOneG = 0.0f;
    public float colourOneB = 0.0f;
    public float colourOneA = 1.0f;
    public float colourTwoR = 1.0f;
    public float colourTwoG = 1.0f;
    public float colourTwoB = 1.0f;
    public float colourTwoA = 1.0f;


    // This function is ran at the start
    void Start ()
    {       
        // Get the renderer of the object so we can access the color
        rend = GetComponent<Renderer>();

        // Set the initial color (0f,0f,0f,0f)
        altColor.r = 0f;
        altColor.g = 0f;         
        altColor.b = 0f;         
        altColor.a = 0f;
        rend.material.color = altColor;

        // Invoke sinusoidal blinking
        InvokeRepeating("Sinusoid", startingTime, 1/(2*increment*frequency));
    }      

    // Function to alternate (blink) the colour of the sphere - Square wave
    void Blink()
    {
        // Colour is black
        if (colour == 0) {
                altColor.r = 255.0f;
                altColor.g = 255.0f;
                altColor.b = 255.0f;
                altColor.a = 0f;
                colour = 1;
        // Colour is white
        
        } else {
                altColor.r = 0.0f;
                altColor.g = 0.0f;
                altColor.b = 0.0f;
                altColor.a = 0.0f;
                colour = 0;
            }
        rend.material.color = altColor;
    }

    // Function to alternate the colour sinusoidically - Sinusoid
    void Sinusoid()
    {
        // Colour 1 
        // Orange 0.2F, 0.3F, 0.4F
        if (colour == 0) {
            altColor.r = colourOneR + (colourTwoR - colourOneR) * ((float)i/increment);
            altColor.g = colourOneG + (colourTwoG - colourOneG) * ((float)i/increment);
            altColor.b = colourOneB + (colourTwoB - colourOneB) * ((float)i/increment);
            // altColor.a = colourOneA + (colourTwoA - colourOneA) * (i/increment);
            altColor.a = 0.5f;
            i++;
            // After some increments, now the colour is now 2
            if (i > increment) {
                colour = 1;
                i = 1;
            }
        // Colour 2
        // Yellow 1.0F. 0.92F, 0.016F
        } else {
            altColor.r = colourTwoR - (colourTwoR - colourOneR) * ((float)i/increment);
            altColor.g = colourTwoG - (colourTwoG - colourOneG) * ((float)i/increment);
            altColor.b = colourTwoB - (colourTwoB - colourOneB) * ((float)i/increment);
            altColor.a = colourTwoA - (colourTwoA - colourOneA) * ((float)i/increment);
            // altColor.a = colourOneA + (colourTwoA - colourOneA) * (i/increment);
            altColor.a = 0.5f;
            i++;
            // After some increments, now the colour is now 1
            if (i > increment) {
                colour = 0;
                i = 1;
            }
        }

        // Debug.Log("Red is: " + altColor.r.ToString ());
        // Debug.Log("i/increment: " + ((float)i/increment));

        // Set the material color
        rend.material.color = altColor;
    }

}
