using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwobyTwoController : MonoBehaviour
{
    public int activate = 0;
    public int selected = 0;
    public int firstSelect = 0;
    public int secondSelect = 0;
    public int thirdSelect = 0;
    public int mode = 0;
    private int currChild = 0;
    private Renderer rend;
    
    // Start is called before the first frame update
    void Start()
    {
        activate = 0;
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 1 || mode == 2) {
            foreach (Transform child in transform){
                if (activate == 1) {
                    rend = child.GetComponent<Renderer>();
                    rend.material.color = Color.yellow;
                } else {
                    rend = child.GetComponent<Renderer>();
                    rend.material.color = Color.blue;
                }
            }
        } else if (mode == 3) {
            currChild = 0;
            foreach (Transform child in transform){
                if (currChild+1 == thirdSelect && activate == 1) {
                    rend = child.GetComponent<Renderer>();
                    rend.material.color = Color.yellow;
                } else {
                    rend = child.GetComponent<Renderer>();
                    rend.material.color = Color.blue;
                }
                currChild++;
            }
        } else if (mode == 4) {
            Debug.Log("Square Selected: (" + firstSelect + "," + secondSelect + "," + thirdSelect + ").");
        }
    }

    void ChangeColor(int i, Transform child) 
    {
        if (i == 1) {
            rend = child.GetComponent<Renderer>();
            rend.material.color = Color.yellow;
        } else if (i == 0) {
            rend = child.GetComponent<Renderer>();
            rend.material.color = Color.blue;
        }
        
    }

}


// int currChild = 0;
//         if (activate == 1) {
//             if (selected != 0 && selected != 5) {
//                 currChild = 0;
//                 foreach (Transform child in transform){
//                     if (currChild+1 == selected) {
//                         ChangeColor(1, child);
//                     } else {
//                         ChangeColor(0, child);
//                     }
//                     currChild++;
//                 }
//             } else {
//                 currChild = 0;
//                 foreach (Transform child in transform){
//                     ChangeColor(1, child);
//                     currChild++;
//                 }
//             }
//         } else {
//             currChild = 0;
//             foreach (Transform child in transform){
//                 ChangeColor(0, child);
//                 currChild++;
//             }
//         }