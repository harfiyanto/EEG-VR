using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerController : MonoBehaviour
{
    private Color altColor = Color.black;
    private Renderer rend; 
    public float frequency = 1.0f;
    public int activate = 0;
    private int activated = 0;
    private int colour = 0;
    private int size = 0;
    public int delayNumber = 0;
    public float phaseDelay = 0.0f;
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
    public float scale1 = 1.0f;
    public float scale2 = 1.0f;
    private int currChild = 0;
    private float timer = 0.0f;
    public float interval = 3.0f;
    public int mode = 1;
    void Start ()
    {       
        //Invoke First Blink Repeatedly
        // InvokeRepeating("Gulliver", startingTime, 1/(2*frequency));
        // print("Frequency: " + frequency);
        phaseDelay = delayNumber / (4 * frequency);
        // Debug.Log(phaseDelay);
    }      
 
    void Update() 
    {   
        if (activate == 1 && activated == 0) {
            StartInvoke();
            activate = 0;
            activated = 1;
            timer = 0.0f;
        }

        if (activated == 1) {
            if (timer >= interval) {
                activated = 0;
                StopInvoke();
                // if (timer >= interval + waitTime) {
                //     StartInvoke();
                //     timer = 0.0f;
                // }
            }
            timer += Time.deltaTime;
        }
        
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
    }

    void Gulliver() 
    {
        Vector3 scale = transform.localScale;
        if (size == 0) {
            scale.x = scale1;
            scale.y = scale1;
            scale.z = scale1;
            size = 1;
        } else {
            scale.x = scale2;
            scale.y = scale2;
            scale.z = scale2;
            size = 0;
        }
        transform.localScale = scale;
    }

    void Checker() 
    {
        currChild = 1;
        foreach (Transform child in transform){
            rend = child.GetComponent<Renderer>();
            if (currChild % 2 == 0) {
                if (colour == 0) {
                    altColor.r = colourTwoR;
                    altColor.g = colourTwoG;
                    altColor.b = colourTwoB;
                    altColor.a = colourTwoA;
                } else {
                    altColor.r = colourOneR;
                    altColor.g = colourOneG;
                    altColor.b = colourOneB;
                    altColor.a = colourOneA;
                }
            } else {
                if (colour == 0) {
                    altColor.r = colourOneR;
                    altColor.g = colourOneG;
                    altColor.b = colourOneB;
                    altColor.a = colourOneA;
                } else {
                    altColor.r = colourTwoR;
                    altColor.g = colourTwoG;
                    altColor.b = colourTwoB;
                    altColor.a = colourTwoA;
                }
            }
            rend.material.color = altColor;
            currChild++;
        }

        if (colour == 0) {
            colour = 1;
        } else {
            colour = 0;
        }

    }

    void Flicker() 
    {
        currChild = 1;
        foreach (Transform child in transform){
            rend = child.GetComponent<Renderer>();
            if (colour == 0) {
                altColor.r = colourTwoR;
                altColor.g = colourTwoG;
                altColor.b = colourTwoB;
                altColor.a = colourTwoA;
            } else {
                altColor.r = colourOneR;
                altColor.g = colourOneG;
                altColor.b = colourOneB;
                altColor.a = colourOneA;
            }
            rend.material.color = altColor;
            currChild++;
        }

        if (colour == 0) {
            colour = 1;
        } else {
            colour = 0;
        }

    }

    void StartInvoke(){
        // Checker
        if (mode == 1) {
            InvokeRepeating("Checker", phaseDelay, 1/(2*frequency));
        } else if (mode == 2) {
            InvokeRepeating("Flicker", phaseDelay, 1/(2*frequency));
        } else if (mode == 3) {
            InvokeRepeating("Gulliver", phaseDelay, 1/(2*frequency));
        }
    }

    void StopInvoke(){
        // Checker
        if (mode == 1) {
            CancelInvoke("Checker");
        } else if (mode == 2) {
            CancelInvoke("Flicker");
        } else if (mode == 3) {
            Vector3 scale = transform.localScale;
            scale.x = scale2;
            scale.y = scale2;
            scale.z = scale2;
            transform.localScale = scale;
            CancelInvoke("Gulliver");
        }
    }
}
