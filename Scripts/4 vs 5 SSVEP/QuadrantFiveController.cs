using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class QuadrantFiveController : MonoBehaviour
{
    private Renderer rend;
    public int xstart = 4;
    public int ystart = 4;
    private int xpos;
    private int ypos;
    private int xupperlimit = 0;
    private int xlowerlimit = 0;
    private int yupperlimit = 0;
    private int ylowerlimit = 0;
    private int xUpper = 0;
    private int xLower = 0;
    private int yUpper = 0;
    private int yLower = 0;
    private int xGrid = 0;
    private int yGrid = 0;
    private int first = 0;
    private int second = 0;
    private int third = 0;
    private int currChild = 1;
    private int gridNumber;
    private Vector3 gridCoordinate;
    private int updated = 0;
    private RobotController robot;
    private BallController ball;
    private Color color;
    public int mode = 1;
    public int udpCMD = 0;
    private int prevUDP;
    private int prevCMD;
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
    private int[] gridLimits;
   
    void Start()
    {
        gridLimits = new int[4];
        mode = 1;
        xpos = xstart;
        ypos = ystart;
        xUpper = 0;
        yUpper = 0;
        xLower = 0;
        yLower = 0;
        randomize = 0;
        prevUDP = 17;   // some out of range number
        prevCMD = 0;
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
        ignore = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (randomize == 0) {
            randomize = 1;
            ballCoordinate = Random.Range(1, 64);
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

        if (Input.GetKeyDown("a") || (udpCMD == 1 && commandPending == 1)){
            ActivateHUD();
            prevCMD = 1;
            if (ignore == 1) {
                randomize = 0;
                ignore = 0;
            } else {
                gridLimits = calculateLimits(mode, 1);
                xLower = gridLimits[0];
                xUpper = gridLimits[1];
                yLower = gridLimits[2];
                yUpper = gridLimits[3];
                if (mode == 1) {
                    Debug.Log("Mode: " + mode);
                    // second = prevUDP;
                    first = prevCMD;
                    mode++;
                } else if (mode == 2) {
                    Debug.Log("Mode: " + mode);
                    // second = prevUDP;
                    second = prevCMD;
                    mode++;
                } else if (mode == 3) {
                    if (prevUDP == udpCMD) {
                        // third = prevUDP;
                        third = prevCMD;
                        robot.gridPos = calculateFinalPos(xLower, yLower);
                        robot.activate = 1;
                        ignore = 1;
                        mode = 1; // reset mode
                        Debug.Log("Reset mode to " + mode);
                    }
                }
                prevUDP = udpCMD;
            }
            commandPending = 0;
        } else if (Input.GetKeyDown("x") || (udpCMD == 3 && commandPending == 1)) {
            ActivateHUD();
            prevCMD = 3;
            if (ignore == 1) {
                randomize = 0;
                ignore = 0;
            } else {
                gridLimits = calculateLimits(mode, 3);
                xLower = gridLimits[0];
                xUpper = gridLimits[1];
                yLower = gridLimits[2];
                yUpper = gridLimits[3];
                if (mode == 1) {
                    Debug.Log("Mode: " + mode);
                    // second = prevUDP;
                    first = prevCMD;
                    mode++;
                } else if (mode == 2) {
                    Debug.Log("Mode: " + mode);
                    // second = prevUDP;
                    second = prevCMD;
                    mode++;
                } else if (mode == 3) {
                    if (prevUDP == udpCMD) {
                        // third = prevUDP;
                        third = prevCMD;
                        robot.gridPos = calculateFinalPos(xLower, yLower);
                        robot.activate = 1;
                        ignore = 1;
                        mode = 1; // reset mode
                        Debug.Log("Reset mode to " + mode);
                    }
                }
                prevUDP = udpCMD;
            }
            commandPending = 0;
        } else if (Input.GetKeyDown("z") || (udpCMD == 4 && commandPending == 1)) {
            ActivateHUD();
            prevCMD = 4;
            if (ignore == 1) {
                randomize = 0;
                ignore = 0;
            } else {
                gridLimits = calculateLimits(mode, 4);
                xLower = gridLimits[0];
                xUpper = gridLimits[1];
                yLower = gridLimits[2];
                yUpper = gridLimits[3];
                if (mode == 1) {
                    Debug.Log("Mode: " + mode);
                    // second = prevUDP;
                    first = prevCMD;
                    mode++;
                } else if (mode == 2) {
                    Debug.Log("Mode: " + mode);
                    // second = prevUDP;
                    second = prevCMD;
                    mode++;
                } else if (mode == 3) {
                    if (prevUDP == udpCMD) {
                        // third = prevUDP;
                        third = prevCMD;
                        robot.gridPos = calculateFinalPos(xLower, yLower);
                        robot.activate = 1;
                        ignore = 1;
                        mode = 1; // reset mode
                        Debug.Log("Reset mode to " + mode);
                    }
                }
                prevUDP = udpCMD;
            }
            commandPending = 0;
        } else if (Input.GetKeyDown("s") ||(udpCMD == 2 && commandPending == 1)) {
            ActivateHUD();
            prevCMD = 2;
            if (ignore == 1) {
                randomize = 0;
                ignore = 0;
            } else {
                gridLimits = calculateLimits(mode, 2);
                xLower = gridLimits[0];
                xUpper = gridLimits[1];
                yLower = gridLimits[2];
                yUpper = gridLimits[3];
                if (mode == 1) {
                    Debug.Log("Mode: " + mode);
                    // second = prevUDP;
                    first = prevCMD;
                    prevCMD = 0;
                    mode++;
                } else if (mode == 2) {
                    Debug.Log("Mode: " + mode);
                    // second = prevUDP;
                    second = prevCMD;
                    prevCMD = 0;
                    mode++;
                } else if (mode == 3) {
                    if (prevUDP == udpCMD) {
                        // third = prevUDP;
                        third = prevCMD;
                        Debug.Log("final pos: (" + xLower + "," + yLower + ").");
                        robot.gridPos = calculateFinalPos(xLower, yLower);
                        robot.activate = 1;
                        ignore = 1;
                        mode = 1; // reset mode
                        Debug.Log("Reset mode to " + mode);
                    }                }
                prevUDP = udpCMD;
            }
            commandPending = 0;
            
        } else if (Input.GetKeyDown("p") || ((udpCMD == 5) && commandPending == 1)) {
            // Debug.Log("Grid selected: (" + xpos + "," + ypos + ").");
            ActivateHUD();
            prevCMD = 5;
            if (ignore == 1) {
                randomize = 0;
                ignore = 0;
            } else {
                if (mode > 1) {
                    Debug.Log("mode before: " + mode);
                    mode--;
                    Debug.Log("mode after: " + mode);
                } else {
                    mode = 1;
                }
                
                // if (mode == 1){
                //     Debug.Log("Mode: " + mode);
                //     // first = prevUDP;
                //     first = prevCMD;
                //     mode++;
                //     // gridLimits = calculateLimits(mode, prevUDP);
                //     // xLower = gridLimits[0];
                //     // xUpper = gridLimits[1];
                //     // yLower = gridLimits[2];
                //     // yUpper = gridLimits[3];
                // } else if (mode == 2) {
                //     Debug.Log("Mode: " + mode);
                //     // second = prevUDP;
                //     second = prevCMD;
                //     mode++;
                //     Debug.Log("Mode after 2: " + mode);
                //     // gridLimits = calculateLimits(mode, prevUDP);
                //     // xLower = gridLimits[0];
                //     // xUpper = gridLimits[1];
                //     // yLower = gridLimits[2];
                //     // yUpper = gridLimits[3];
                // } else if (mode == 3) {
                //     Debug.Log("Mode: " + mode);
                //     // third = prevUDP;
                //     third = prevCMD;
                //     robot.gridPos = calculateFinalPos(xLower, yLower);
                //     robot.activate = 1;
                //     ignore = 1;
                //     mode = 1; // reset mode
                //     Debug.Log("Reset mode to " + mode);
                // }
                prevUDP = udpCMD;
            }
            commandPending = 0;
            gridLimits = calculateLimits(mode, prevCMD);
            xLower = gridLimits[0];
            xUpper = gridLimits[1];
            yLower = gridLimits[2];
            yUpper = gridLimits[3];
        } else if (udpCMD == 0 && commandPending == 1){ 
            ActivateHUD();
            commandPending = 0;
            prevCMD = 0;
        } 

        if (Input.GetKeyDown("r")){
            randomize = 0;
        }

        // Debug.Log("x: " + xpos + ", y: " + ypos + " , gridNumber: " + gridNumber);
        currChild = 1;
        foreach (Transform child in transform) {
            xGrid = currChild % 8;
            yGrid = currChild / 8 + 1;
            if (xGrid == 0) {
                xGrid = 8;
                yGrid -= 1;
            }
            
            rend = child.GetComponent<Renderer>();
            if ((xGrid <= xUpper) && (xGrid >= xLower) && (yGrid <= yUpper) && (yGrid >= yLower)) {
                rend.material.color = Color.yellow;
                gridCoordinate = child.position;
            } else {
                rend.material.color = color;
            }
            currChild++;
        }
    }

    Vector3 calculateFinalPos(int x, int y) {
        int currC = 1;
        foreach (Transform child in transform) {
            int currX = currC % 8;
            int currY = currC / 8 + 1;
            if (currX == 0) {
                currX = 8;
                currY -= 1;
            }
            // Debug.Log("x: " + currX + ", y: " + currY);
            if ((currX == x) && (currY == y)) {
                Debug.Log("square found");
                return child.position;
            }
            currC++;
        }
        Debug.Log("Danger!");
        return new Vector3(0,0,0);
    }


    int[] calculateLimits(int mode, int command) {
        int[] limits = new int[4];
        int incrementX = 0;
        int incrementY = 0;

        // Calculate increment for mode 2 (and mode 3 partially)
        if (mode > 1) {
            if (first == 1) {
            } else if (first == 2) {
                incrementX = 4;
            } else if (first == 3) {
                incrementX = 4;
                incrementY = 4;
            } else if (first == 4) {
                incrementY = 4;
            }
        }

        // Calculate increment for mode 3
        if (mode > 2) {
            if (second == 1) {
            } else if (second == 2) {
                incrementX += 2;
            } else if (second == 3) {
                incrementX += 2;
                incrementY += 2;
            } else if (second == 4) {
                incrementY += 2;
            }
            // Debug.Log("calculating for mode 3");
            // Debug.Log("xinc: " + incrementX);
            // Debug.Log("yinc: " + incrementY);
            // Debug.Log("first: " + first);
            // Debug.Log("second: " + second);
        }
        // Debug.Log("Increment x: " + incrementX);
        // Debug.Log("Increment y: " + incrementY);

        if (mode == 1) {
            if (command == 1) {
                limits[0] = 1;
                limits[1] = 4;
                limits[2] = 1;
                limits[3] = 4;
            } else if (command == 2){
                limits[0] = 5;
                limits[1] = 8;
                limits[2] = 1;
                limits[3] = 4;
            } else if (command == 3){
                limits[0] = 5;
                limits[1] = 8;
                limits[2] = 5;
                limits[3] = 8;
            } else if (command == 4){
                limits[0] = 1;
                limits[1] = 4;
                limits[2] = 5;
                limits[3] = 8;
            }
            return limits;
        } else if (mode == 2){
            if (command == 1) {
                limits[0] = 1 + incrementX;
                limits[1] = 2 + incrementX;
                limits[2] = 1 + incrementY;
                limits[3] = 2 + incrementY;
            } else if (command == 2){
                limits[0] = 3 + incrementX;
                limits[1] = 4 + incrementX;
                limits[2] = 1 + incrementY;
                limits[3] = 2 + incrementY;
            } else if (command == 3){
                limits[0] = 3 + incrementX;
                limits[1] = 4 + incrementX;
                limits[2] = 3 + incrementY;
                limits[3] = 4 + incrementY;
            } else if (command == 4){
                limits[0] = 1 + incrementX;
                limits[1] = 2 + incrementX;
                limits[2] = 3 + incrementY;
                limits[3] = 4 + incrementY;
            }
            return limits;
        } else if (mode == 3){
            if (command == 1) {
                limits[0] = 1 + incrementX;
                limits[1] = 1 + incrementX;
                limits[2] = 1 + incrementY;
                limits[3] = 1 + incrementY;
            } else if (command == 2){
                limits[0] = 2 + incrementX;
                limits[1] = 2 + incrementX;
                limits[2] = 1 + incrementY;
                limits[3] = 1 + incrementY;
            } else if (command == 3){
                limits[0] = 2 + incrementX;
                limits[1] = 2 + incrementX;
                limits[2] = 2 + incrementY;
                limits[3] = 2 + incrementY;
            } else if (command == 4){
                limits[0] = 1 + incrementX;
                limits[1] = 1 + incrementX;
                limits[2] = 2 + incrementY;
                limits[3] = 2 + incrementY;
            }
            return limits;
        }
        return limits;
        // else if (mode == 2) {
        //     return limits;
        // } else if (mode == 3) {
        //     return limits;
        // }
    }

    void ActivateHUD() {
        cc1.activate = 1;
        cc2.activate = 1;
        cc3.activate = 1;
        cc4.activate = 1;
        cc5.activate = 1;
        timingBar.activate = 1;
    }

    void DeactivateHUD() {
        cc1.activate = 0;
        cc2.activate = 0;
        cc3.activate = 0;
        cc4.activate = 0;
        cc5.activate = 0;
        timingBar.activate = 0;
    }
}

