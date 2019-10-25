using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSquareWaveController : MonoBehaviour
{
    private Color altColor = Color.black;
    private Color currColor = Color.black;
    private Renderer rend; 
    public float frequency_1 = 1.0f;
    public float frequency_2 = 2.0f;
    private int color = 0;
    private int color1 = 0;
    private int color2 = 0;
    public float startingTime = 3.0f;
    private int increment = 1;
    public float waitTime = 1.0f;
    public float colorOneR = 0.0f; 
    public float colorOneG = 0.0f;
    public float colorOneB = 0.0f;
    public float colorOneA = 1.0f;
    public float colorTwoR = 1.0f;
    public float colorTwoG = 1.0f;
    public float colorTwoB = 1.0f;
    public float colorTwoA = 1.0f;
    public float colorDiffR = 1.0f;
    public float colorDiffG = 1.0f;
    public float colorDiffB = 1.0f;
    public float colorDiffA = 1.0f;
    private float i = 0.0f;

    void Start ()
    {       
        //Get the renderer of the object so we can access the color
        rend = GetComponent<Renderer>();
        //Set the initial color (0f,0f,0f,0f)
        altColor.r = colorOneR;
        altColor.g = colorOneG;
        altColor.b = colorOneB;
        altColor.a = colorOneA;
        rend.material.color = altColor;
        colorDiffR = colorTwoR - colorOneR;
        colorDiffG = colorTwoG - colorOneG;
        colorDiffB = colorTwoB - colorOneB;
        colorDiffA = colorTwoA - colorOneA;

        //Invoke First Blink Repeatedly
        // InvokeRepeating("Blink", startingTime, 1/(2*frequency_1));
        // InvokeRepeating("AnotherBlink", startingTime, 1/(2*frequency_2));
        // print("Frequency: " + frequency);
        InvokeRepeating("AddNumber", startingTime, 4);
        InvokeRepeating("AnotherAddNumber", startingTime, 1);
        // Debug.Log("Color Diff: " + colorDiffR);
    }

    // Reset the color to black
    void Resetcolor() 
    {
        altColor.r = colorOneR;
        altColor.g = colorOneG;
        altColor.b = colorOneB;
        altColor.a = colorOneA;
        rend.material.color = altColor;
    }

    void AddNumber() 
    {
        if (color1 == 0) {
            i++;
            color1 = 1;
            
        } else {
            i--;
            color1 = 0;
        }
        Debug.Log("Current Number: " + i);
    }

    void AnotherAddNumber() 
    {
        if (color2 == 0) {
            i++;
            color2 = 1;
            
        } else {
            i--;
            color2 = 0;
        }
        Debug.Log("Current Number: " + i);
    }
    
    void Blink()
    {
        currColor = rend.material.GetColor("_Color");
        if (color1 == 0) {
            altColor.r = currColor.r + colorDiffR/2;
            altColor.g = currColor.g + colorDiffG/2;
            altColor.b = currColor.b + colorDiffB/2;
            altColor.a = currColor.a + colorDiffA/2;
            color1 = 1;
            Debug.Log("Current color (1): " + currColor.r);
            
        } else {
            altColor.r = currColor.r - colorDiffR/2;
            altColor.g = currColor.g - colorDiffG/2;
            altColor.b = currColor.b - colorDiffB/2;
            altColor.a = currColor.a - colorDiffA/2;
            color1 = 0;
            Debug.Log("Current color (0): " + currColor.r);
        }
        rend.material.color = altColor;
    }

    void AnotherBlink()
    {
        currColor = rend.material.GetColor("_Color");
        if (color2 == 0) {
            altColor.r = currColor.r + colorDiffR/2;
            altColor.g = currColor.g + colorDiffG/2;
            altColor.b = currColor.b + colorDiffB/2;
            altColor.a = currColor.a + colorDiffA/2;
            color2 = 1;
            Debug.Log("Current color (1): " + currColor.r);
            
        } else {
            altColor.r = currColor.r - colorDiffR/2;
            altColor.g = currColor.g - colorDiffG/2;
            altColor.b = currColor.b - colorDiffB/2;
            altColor.a = currColor.a - colorDiffA/2;
            color2 = 0;
            Debug.Log("Current color (0): " + currColor.r);
        }
        rend.material.color = altColor;
    }

}
