using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    private Color altColor = Color.black;
    private Renderer rend; 
    // private float frequency = 13.0f;
    public float frequency_1 = 13.0f;
    public float frequency_2 = 15.0f;
    public float frequency_3 = 17.0f;
    public float frequency_4 = 19.0f;

    public float[] frequency;
    //15, 19, 23 ;
    private int colour = 0;
    public int mode = 0;
    public static float startingTime = 5.0f;
    public static float interval = 3.0f;
    private float timeLeft = startingTime + interval;
    private int increment = 1;
    public float waitTime = 1.0f;

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

        // Assign frequency values
        frequency = new float[4];
        frequency[0] = frequency_1;
        frequency[1] = frequency_2;
        frequency[2] = frequency_3;
        frequency[3] = frequency_4;
        // print("Frequency 1: " + frequency[0]);
        // print("Frequency 2: " + frequency[1]);
        // print("Frequency 3: " + frequency[2]);
        // print("Frequency 4: " + frequency[3]);
        // print(frequency_2);
        
        //Invoke First Blink Repeatedly
        InvokeRepeating("Blink", startingTime, 1/(2*frequency[mode]));
        print("Frequency: " + frequency[mode]);
    }      
 
    void Update() 
    {
        timeLeft -= Time.deltaTime;
        if ( timeLeft < 0 )
        {
            CancelInvoke();
            ResetColour();
            ChangeMode();
            InvokeRepeating("Blink", waitTime, 1/(2*frequency[mode]));
            timeLeft = interval + waitTime;
            print("Frequency: " + frequency[mode]);
            print("Mode: " + mode);
            // GameOver();
        }
    }

    // Reset the colour to black
    void ResetColour() 
    {
        altColor.r = 0.0f;
        altColor.g = 0.0f;
        altColor.b = 0.0f;
        altColor.a = 0.0f;
        rend.material.color = altColor;
    }

    void Blink()
    {
        if (colour == 0) {
                altColor.r = 255.0f;
                altColor.g = 255.0f;
                altColor.b = 255.0f;
                altColor.a = 0f;
                colour = 1;
            } else {
                altColor.r = 0.0f;
                altColor.g = 0.0f;
                altColor.b = 0.0f;
                altColor.a = 0.0f;
                colour = 0;
            }
            rend.material.color = altColor;
    }

    void Sinusoid()
    {
        // Colour is Black
        if (colour == 0) {
            altColor.r = 25.5f * increment;
            altColor.g = 25.5f * increment;
            altColor.b = 25.5f * increment;
            altColor.a = 0f;
            increment++;
            // Colour is now Orange
            if (increment == 11) {
                // Debug.Log("Change Colour");
                colour = 1;
                increment = 1;
            }
        // Colour is Orange
        } else {
            altColor.r = 25.5f * (10 - increment);
            altColor.g = 25.5f * (10 - increment);
            altColor.b = 25.5f * (10 - increment);
            altColor.a = 0.0f;
            increment++;
            // Colour is now Black
            if (increment == 11) {
                colour = 0;
                increment = 1;
            }
        }
        rend.material.color = altColor;
    }

    void ChangeMode () {
        if (mode == 3) {
            mode = 0;
        } else if (mode == 0) {
            mode++;
        } else if (mode == 1) {
            mode++;
        } else if (mode == 2) {
            mode++;
        }
    }
}
