using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GridNavigationFiveController : MonoBehaviour
{
    private Renderer rend;
    public int xstart = 4;
    public int ystart = 4;
    private int xpos;
    private int ypos;
    private int xupperlimit = 8;
    private int xlowerlimit = 1;
    private int yupperlimit = 8;
    private int ylowerlimit = 1;
    private int currChild = 1;
    private int gridNumber;
    private Vector3 gridCoordinate;
    private int updated = 0;
    private RobotController robot;
    private BallController ball;
    private Color color;
    // public int mode = 1;
    public int udpCMD = 0;
    private int prevUDP;
    public int commandPending = 0;
    public Vector3 ballPos;
    private int ballCoordinate;
    public int randomize;
    public int ignore;
    private IndicatorController ic1; 	// Indicator 1 Controller Reference
	private IndicatorController ic2;	// Indicator 2 Controller Reference
	private IndicatorController ic3;	// Indicator 3 Controller Reference
	private IndicatorController ic4;	// Indicator 4 Controller Reference
    private IndicatorController ic5;	// Indicator 5 Controller Reference
    private CheckerController cc1;
    private CheckerController cc2;
    private CheckerController cc3;
    private CheckerController cc4;
    private CheckerController cc5;
    private TimingBarFiveController timingBar;
   
    void Start()
    {
        xpos = xstart;
        ypos = ystart;
        randomize = 0;
        prevUDP = 17;   // some out of range number
        gridNumber = xpos + (ypos - 1) * 10;
        robot = GameObject.Find("Robot").gameObject.GetComponent<RobotController>();
        ball = GameObject.Find("Ball 1").gameObject.GetComponent<BallController>();
        // Initialize the reference for other game objects (Implementation Specific)
		ic1 = GameObject.Find("Indicator 1").gameObject.GetComponent<IndicatorController>();
		ic2 = GameObject.Find("Indicator 2").gameObject.GetComponent<IndicatorController>();
		ic3 = GameObject.Find("Indicator 3").gameObject.GetComponent<IndicatorController>();
		ic4 = GameObject.Find("Indicator 4").gameObject.GetComponent<IndicatorController>();
        ic5 = GameObject.Find("Indicator 5").gameObject.GetComponent<IndicatorController>();
        cc1 = GameObject.Find("Checker 1").gameObject.GetComponent<CheckerController>();
        cc2 = GameObject.Find("Checker 2").gameObject.GetComponent<CheckerController>();
        cc3 = GameObject.Find("Checker 3").gameObject.GetComponent<CheckerController>();
        cc4 = GameObject.Find("Checker 4").gameObject.GetComponent<CheckerController>();
        cc5 = GameObject.Find("Checker 5").gameObject.GetComponent<CheckerController>();
        
        timingBar = GameObject.Find("Timing Bar").gameObject.GetComponent<TimingBarFiveController>();

        // Define grid color
        color.r = 0.5f;
        color.g = 0.5f;
        color.b = 0.5f;

        Debug.Log("Test: Ball " + randomize.ToString());
        ignore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (randomize == 0) {
            randomize = 1;
            // ballCoordinate = Random.Range(1, 64);
            ballCoordinate = 25;
            // Debug.Log("randomized ball coordinate: " + ballCoordinate + ", ball position: " + ball.ballPos);
            currChild = 1;
            foreach (Transform child in transform) {
                if (currChild == ballCoordinate) {
                    ballPos = child.position;
                }
                currChild++;
            }
            ball.ballPos = new Vector3 (ballPos.x, ball.ballPos.y, ballPos.z);
            ball.randomized = 1;
            randomize = 1;
            xpos = xstart;
            ypos = ystart;
            // Debug.Log("random position: " + ballCoordinate +", and vector 3: " + ballPos);
        }
        
        gridNumber = xpos + (ypos - 1) * 8;
        
        ic1.activate = 0;
		ic2.activate = 0;
		ic3.activate = 0;
		ic4.activate = 0;
        ic5.activate = 0;
        
        // timingBar.activate = 0;

        if (udpCMD == 1) {
            ic1.activate = 1;
        } else if (udpCMD == 2) {
            ic2.activate = 1;
        } else if (udpCMD == 3) {
            ic3.activate = 1;
        } else if (udpCMD == 4) {
            ic4.activate = 1;
        } else if (udpCMD == 5) {
            ic5.activate = 1;
        } 

        if (Input.GetKeyDown(KeyCode.UpArrow) || (udpCMD == 1 && commandPending == 1)){
            ActivateHUD();
            if (ignore == 1) {
                randomize = 0;
                ignore = 0;
            } else if (ypos > ylowerlimit) {
                ypos = ypos - 1;
                prevUDP = udpCMD;
            }
            commandPending = 0;
        } else if (Input.GetKeyDown(KeyCode.DownArrow) || (udpCMD == 3 && commandPending == 1)) {
            ActivateHUD();
            if (ignore == 1) {
                randomize = 0;
                ignore = 0;
            } else if (ypos < yupperlimit) {
                ypos = ypos + 1;
                prevUDP = udpCMD;
            }
            commandPending = 0;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) || (udpCMD == 4 && commandPending == 1)) {
            ActivateHUD();
            if (ignore == 1) {
                randomize = 0;
                ignore = 0;
            } else if (xpos > xlowerlimit) {
                xpos = xpos - 1;
                prevUDP = udpCMD;
            }
            commandPending = 0;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) ||(udpCMD == 2 && commandPending == 1)) {
            ActivateHUD();
            if (ignore == 1) {
                randomize = 0;
                ignore = 0;
            } else if (xpos < xupperlimit) {
                xpos = xpos + 1;
                prevUDP = udpCMD;
            }
            commandPending = 0;
        } else if (Input.GetKeyDown("p") || ((udpCMD == 5) && commandPending == 1)) {
            // Debug.Log("Grid selected: (" + xpos + "," + ypos + ").");
            ActivateHUD();
            if (ignore == 1) {
                randomize = 0;
                ignore = 0;
            } else {
                robot.gridPos = gridCoordinate;
                robot.activate = 1;
                ignore = 1;
                prevUDP = udpCMD;
            }
            commandPending = 0;
        } else if (udpCMD == 0 && commandPending == 1){ 
            ActivateHUD();
            commandPending = 0;
        }

        if (Input.GetKeyDown("r")){
            randomize = 0;
        }

        // Debug.Log("x: " + xpos + ", y: " + ypos + " , gridNumber: " + gridNumber);
        currChild = 1;
        foreach (Transform child in transform) {
            rend = child.GetComponent<Renderer>();
            if (currChild == gridNumber) {
                rend.material.color = Color.yellow;
                gridCoordinate = child.position;
            } else {
                rend.material.color = color;
            }
            currChild++;
        }



            // foreach (Transform child in transform){
            //     if (currChild+1 == selected) {
            //         currController = child.GetComponent<FourbyFourController>();
            //         currController.activate = 1;
            //     } else {
            //         currController = child.GetComponent<FourbyFourController>();
            //         currController.activate = 0;
            //     }
            //     currChild++;
            // }
    }
    
    void ActivateHUD() {
        cc1.activate = 1;
        cc2.activate = 1;
        cc3.activate = 1;
        cc4.activate = 1;
        cc5.activate = 1;
        // timingBar.activate = 1;
    }

    void DeactivateHUD() {
        cc1.activate = 0;
        cc2.activate = 0;
        cc3.activate = 0;
        cc4.activate = 0;
        cc5.activate = 0;
        // timingBar.activate = 0;
    }
}

