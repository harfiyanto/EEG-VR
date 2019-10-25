using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class QuadrantController : MonoBehaviour
{
    private Renderer rend;
    private int mode = 3;
    private int modeChanged = 0;
    private int firstSelect = 0;
    private int secondSelect = 0;
    private int thirdSelect = 0;
    private int selected = 0;
    private int prevSelected = 0;
    private FourbyFourController currController;
    // Start is called before the first frame update
    void Start()
    {
        mode = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("q")){
            selected = 1;
            prevSelected = 1;
        } else if (Input.GetKey("w")) {
            selected = 2;
            prevSelected = 2;
        } else if (Input.GetKey("s")) {
            selected = 3;
            prevSelected = 3;
        } else if (Input.GetKey("a")) {
            selected = 4;
            prevSelected = 4;
        } else if (Input.GetKey(KeyCode.Return)) {
            if (modeChanged == 0) {
                modeChanged = 1;
                if (mode == 1) {
                    firstSelect = prevSelected;
                } else if (mode == 2) {
                    secondSelect = prevSelected;
                } else if (mode == 3) {
                    thirdSelect = prevSelected;
                }
                mode++;
                selected = 0;
            }
        } else {
            modeChanged = 0;
            selected = 0;
        }

        if (mode == 1) {
            Debug.Log("Mode 1. Selected: " + selected);
            int currChild = 0;
            foreach (Transform child in transform){
                if (currChild+1 == selected) {
                    currController = child.GetComponent<FourbyFourController>();
                    currController.activate = 1;
                } else {
                    currController = child.GetComponent<FourbyFourController>();
                    currController.activate = 0;
                }
                currChild++;
            }
        } else if (mode == 2) {
            Debug.Log("Mode 2. Selected: " + selected + ". First Select: " + firstSelect);
            int currChild = 0;
            foreach (Transform child in transform){
                if (currChild+1 == firstSelect) {
                    currController = child.GetComponent<FourbyFourController>();
                    currController.activate = 1;
                    currController.selected = selected;
                    currController.mode = 2;
                } else {
                    currController = child.GetComponent<FourbyFourController>();
                    currController.activate = 0;
                    currController.selected = 0;
                    currController.mode = 2;
                }
                currChild++;
            }
        } else if (mode == 3) {
            Debug.Log("Mode 3. Selected: " + selected + ". Second Select: " + secondSelect);
            int currChild = 0;
            foreach (Transform child in transform){
                if (currChild+1 == firstSelect) {
                    currController = child.GetComponent<FourbyFourController>();
                    currController.activate = 1;
                    currController.selected = secondSelect;
                    currController.nextSelect = selected;
                    currController.mode = 3;
                } else {
                    currController = child.GetComponent<FourbyFourController>();
                    currController.activate = 0;
                    currController.selected = 0;
                    currController.nextSelect = 0;
                }
                currChild++;
            }
        } else if (mode == 4) {
            Debug.Log("Mode 4 Reached. Square (" + firstSelect + "," + secondSelect + "," + prevSelected + ") selected.");
            int currChild = 0;
            foreach (Transform child in transform){
                if (currChild+1 == firstSelect) {
                    currController = child.GetComponent<FourbyFourController>();
                    currController.activate = 1;
                    currController.selected = secondSelect;
                    currController.nextSelect = selected;
                    currController.mode = 4;
                } else {
                    currController = child.GetComponent<FourbyFourController>();
                    currController.activate = 0;
                }
                currChild++;
            }
        } 
    }
}
