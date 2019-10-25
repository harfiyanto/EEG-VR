using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourbyFourController : MonoBehaviour
{
    public int activate = 0;
    public int firstSelect = 0;
    public int secondSelect = 0;
    public int thirdSelect = 0;
    public int selected = 0;
    public int nextSelect = 0;
    public int mode = 1;
    private int currChild = 0;
    private TwobyTwoController currController;
    // Start is called before the first frame update
    void Start()
    {
        activate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 1) {
            foreach (Transform child in transform){
                currController = child.GetComponent<TwobyTwoController>();
                currController.activate = activate;
                currController.firstSelect = firstSelect;
                currController.secondSelect = secondSelect;
                currController.thirdSelect = thirdSelect;
                currController.mode = mode;
            }
        } else {
            currChild = 0;
            foreach (Transform child in transform){
                if (currChild+1 == secondSelect) {
                    currController = child.GetComponent<TwobyTwoController>();
                    currController.activate = activate;
                    currController.firstSelect = firstSelect;
                    currController.secondSelect = secondSelect;
                    currController.thirdSelect = thirdSelect;
                    currController.mode = mode;
                } else {
                    currController = child.GetComponent<TwobyTwoController>();
                    currController.activate = 0;
                }
                currChild++;
            }
        }
    }
}


// if (selected != 5 && selected != 0) {
//                 currController = child.GetComponent<TwobyTwoController>();
//                 if (currChild+1 == selected) {
//                     if (mode == 3) {
//                         currController.selected = nextSelect;
//                     }
//                     currController.activate = activate;
//                 } else {
//                     currController.activate = 0;
//                 }
//             } else if (mode == 2){
//                 currController = child.GetComponent<TwobyTwoController>();
//                 currController.activate = 0;
//             } else {
//                 currController = child.GetComponent<TwobyTwoController>();
//                 currController.activate = activate;
//             }
//             currChild++;
