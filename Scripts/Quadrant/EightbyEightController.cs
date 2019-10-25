using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EightbyEightController : MonoBehaviour
{
    private Renderer rend;
    private int mode = 1;
    private int modeChanged = 0;
    private int firstSelect = 0;
    private int secondSelect = 0;
    private int thirdSelect = 0;
    private int selected = 0;
    private int prevSelected = 0;
    private int currChild = 0;
    public int udpCMD = 0;
    private FourbyFourController currController;
    // Start is called before the first frame update
    private IndicatorController ic1; 	// Indicator 1 Controller Reference
	private IndicatorController ic2;	// Indicator 2 Controller Reference
	private IndicatorController ic3;	// Indicator 3 Controller Reference
	private IndicatorController ic4;	// Indicator 4 Controller Reference
    // public int udpCMD = 0;
    public int commandPending = 0;
    public Vector3 ballPos;
    private int ballCoordinate;
    public int randomize;
    private int updated = 0;
    private RobotController robot;
    private BallController ball;
    void Start()
    {
        mode = 1;
        randomize = 0;
        robot = GameObject.Find("Robot").gameObject.GetComponent<RobotController>();
        ball = GameObject.Find("Ball 1").gameObject.GetComponent<BallController>();
        ic1 = GameObject.Find("Indicator 1").gameObject.GetComponent<IndicatorController>();
		ic2 = GameObject.Find("Indicator 2").gameObject.GetComponent<IndicatorController>();
		ic3 = GameObject.Find("Indicator 3").gameObject.GetComponent<IndicatorController>();
		ic4 = GameObject.Find("Indicator 4").gameObject.GetComponent<IndicatorController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("q") || udpCMD == 1){
            selected = 1;
            prevSelected = 1;
        } else if (Input.GetKey("w") || udpCMD == 2) {
            selected = 2;
            prevSelected = 2;
        } else if (Input.GetKey("s") || udpCMD == 3) {
            selected = 3;
            prevSelected = 3;
        } else if (Input.GetKey("a") || udpCMD == 4) {
            selected = 4;
            prevSelected = 4;
        }
        

        if (Input.GetKey(KeyCode.Return)) {
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
        } else if (Input.GetKey(KeyCode.Backspace)) {
            if (modeChanged == 0) {
                modeChanged = 1;
                if (mode > 1) {
                    mode--;
                }
                selected = 0;
            }
        } else {
            modeChanged = 0;
            selected = 0;
        }

        if (mode == 1) {
            firstSelect = prevSelected;
        }
        if (mode == 2) {
            secondSelect = prevSelected;
        }
        if (mode == 3) {
            thirdSelect = prevSelected;
        }

        currChild = 0;
        foreach (Transform child in transform){
            // Debug.Log("prevSelect: " + prevSelected);
            if (currChild+1 == firstSelect) {
                currController = child.GetComponent<FourbyFourController>();
                currController.activate = 1;
                currController.firstSelect = firstSelect;
                currController.secondSelect = secondSelect;
                currController.thirdSelect = thirdSelect;
                currController.mode = mode;
            } else {
                currController = child.GetComponent<FourbyFourController>();
                currController.activate = 0;
            }
            currChild++;
        }
        // Debug.Log("Mode: " + mode + ". FirstSelect: " + firstSelect + ". SecondSelect: " + secondSelect + ". ThirdSelect: " + thirdSelect);
    }
}


        // if (mode == 1) {
        //     Debug.Log("Mode 1. Selected: " + selected);
        //     int currChild = 0;
        //     foreach (Transform child in transform){
        //         if (currChild+1 == selected) {
        //             currController = child.GetComponent<FourbyFourController>();
        //             currController.activate = 1;
        //         } else {
        //             currController = child.GetComponent<FourbyFourController>();
        //             currController.activate = 0;
        //         }
        //         currChild++;
        //     }
        // } else if (mode == 2) {
        //     Debug.Log("Mode 2. Selected: " + selected + ". First Select: " + firstSelect);
        //     int currChild = 0;
        //     foreach (Transform child in transform){
        //         if (currChild+1 == firstSelect) {
        //             currController = child.GetComponent<FourbyFourController>();
        //             currController.activate = 1;
        //             currController.selected = selected;
        //             currController.mode = 2;
        //         } else {
        //             currController = child.GetComponent<FourbyFourController>();
        //             currController.activate = 0;
        //             currController.selected = 0;
        //             currController.mode = 2;
        //         }
        //         currChild++;
        //     }
        // } else if (mode == 3) {
        //     Debug.Log("Mode 3. Selected: " + selected + ". Second Select: " + secondSelect);
        //     int currChild = 0;
        //     foreach (Transform child in transform){
        //         if (currChild+1 == firstSelect) {
        //             currController = child.GetComponent<FourbyFourController>();
        //             currController.activate = 1;
        //             currController.selected = secondSelect;
        //             currController.nextSelect = selected;
        //             currController.mode = 3;
        //         } else {
        //             currController = child.GetComponent<FourbyFourController>();
        //             currController.activate = 0;
        //             currController.selected = 0;
        //             currController.nextSelect = 0;
        //         }
        //         currChild++;
        //     }
        // } else if (mode == 4) {
        //     Debug.Log("Mode 4 Reached. Square (" + firstSelect + "," + secondSelect + "," + prevSelected + ") selected.");
        //     int currChild = 0;
        //     foreach (Transform child in transform){
        //         if (currChild+1 == firstSelect) {
        //             currController = child.GetComponent<FourbyFourController>();
        //             currController.activate = 1;
        //             currController.selected = secondSelect;
        //             currController.nextSelect = selected;
        //             currController.mode = 4;
        //         } else {
        //             currController = child.GetComponent<FourbyFourController>();
        //             currController.activate = 0;
        //         }
        //         currChild++;
        //     }
        // } 