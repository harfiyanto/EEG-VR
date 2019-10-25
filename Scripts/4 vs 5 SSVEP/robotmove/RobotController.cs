using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Albert Tien 27/09/2019 capstone VR/BCI

//update: (27th sep 2019)
//fixed cursor offset bug
//back and forth from bin to cursor
//has via points
//can pick up
//fix scale issue
//clean up a bit more
//better comments
//more functions



public class RobotController : MonoBehaviour
{
    private Transform Cursor, Ball, Destination;
    private Transform Point1, Point2, Point3;
    private Transform Segment_1, Segment_2, Segment_3, Segment_4;
    private Transform Hinge_1, Hinge_2, Hinge_3, Hinge_4;
    private Transform Grip_pos, Grip_L, Grip_R;
    private Vector3 target;
    private Vector3 grip_L_pos, grip_R_pos;
    private Vector3 temp1_vec, temp2_vec, temp3_vec;
    private Vector3 via1, via2, via3, via4;
    public Vector3 gridPos;
    public int activate;
    private float q1, q2, q3, q4;
    private float old_q1, old_q2, old_q3, old_q4;
    private float l1, l2;
    private float z, x, y, r, y_new, z_offset;
    private float r_bound;
    public int success = 0;
    public int fail = 0;
    private float speed, int_speed;
    private int pickup, forward, attached, at_dest;
    private const float speed_multiplier = 1.15f;
    private const float epsilon = 0.000001f; //need to make scale able?
    private GUIStyle guiStyle = new GUIStyle();
    private string message;
    private int timerStarted = 0;
    private GridNavigationController grid;

    void Start()
    {
        //define objects
        Ball        = gameObject.transform.Find("Ball 1");
        Cursor      = gameObject.transform.Find("Cursor");
        Grip_pos    = gameObject.transform.Find("Grip_pos");
        Destination = gameObject.transform.Find("Destination");
        Point1      = gameObject.transform.Find("Position reference/Point1");
        Point2      = gameObject.transform.Find("Position reference/Point2");
        Point3      = gameObject.transform.Find("Position reference/Point3");
        Segment_1   = gameObject.transform.Find("Segment 1");
        Segment_2   = gameObject.transform.Find("Segment 1/Segment 2");
        Segment_3   = gameObject.transform.Find("Segment 1/Segment 2/Segment 3");
        Segment_4   = gameObject.transform.Find("Segment 1/Segment 2/Segment 3/Segment 4");
        Hinge_1     = gameObject.transform.Find("Segment 1/Hinge 1");
        Hinge_2     = gameObject.transform.Find("Segment 1/Segment 2/Hinge 2");
        Hinge_3     = gameObject.transform.Find("Segment 1/Segment 2/Segment 3/Hinge 3");
        Hinge_4     = gameObject.transform.Find("Segment 1/Segment 2/Segment 3/Segment 4/Hinge 4");
        Grip_L      = gameObject.transform.Find("Segment 1/Segment 2/Segment 3/Segment 4/L2_grip_L");
        Grip_R      = gameObject.transform.Find("Segment 1/Segment 2/Segment 3/Segment 4/L2_grip_R");

        //scaling
        //epsilon = 0.000001f * transform.localScale.x;
        int_speed = 0.1f;
        int_speed = int_speed * transform.localScale.x;//scale speed with robot size

        //calculate parameters
        l1 = Point2.position.y - Point1.position.y; //calcuate the arm length
        l2 = Point3.position.y - Point2.position.y; //calcuate the arm length
        r_bound = l1 + l2;  //calculate max reach and set boundary value

        //set initial condition
        pickup = 0;
        forward = 0; //start with the first trajectory
        attached = 0;//ball is not picked up by robot end effector;
        at_dest = 0;
        speed = int_speed; //starting speed
        activate = 0;

        grid = GameObject.Find("8x8 Grid Step").gameObject.GetComponent<GridNavigationController>();

        //move robot to the starting position
        Grip_pos.position = Destination.position; ; //start from bin
        move_robot();

    }


    void FixedUpdate()
    {

        // press p to pick up.
        if (activate == 1 && pickup == 0) //set start position when p is pressed
        {
            pickup = 1; //is now picked up
            activate = 0;
            target = Grid_pos(gridPos);  //set location here, grid center!!!!!!!!!!!!!!!!!!!!!

            //set via points positions
            // via1 = Destination.position;
            // via2 = target + Vector3.up * transform.localScale.x*1; //plus 1 up
            // via3 = target;
            via1 = Destination.position;
            via2 = transform.position + Vector3.right * (-1)*transform.localScale.x + Vector3.up * 4 * transform.localScale.x;// up to here
            via3 = target + Vector3.up * transform.localScale.x; //plus 1 up
            via4 = target;
            
        }
        
        // Debug.Log("Timer: " + timer);
        // if (timer > 0) {
        //     timer -= Time.deltaTime;
        //     if (timerStarted == 0) {
        //         timerStarted = 1;
        //     }
        // } else if (timer <= 0 && timerStarted == 1) {
        //     Debug.Log("robot says randomize!");
        //     grid.randomize = 0;
        //     activate = 0;
        //     timer = 0;
        //     timerStarted = 0;
        // }

        if (pickup == 1)
        {
            // if (forward == 0)//moving towards cursor from bin
            // {
            //     trajectory(via1, via2, 0);
            // }
            // else if (forward == 1)
            // {
            //     trajectory(via2, via3, 0);
            // }
            // else if (forward == 2)
            // {
            //     trajectory(via3, via4, 1);
            // }
            // else if (forward == 3)
            // {
            //     pickup_ball();
            //     grip();
            //     forward = forward+1;
            // }
            // else if (forward == 4)
            // {
            //     trajectory(via4, via3, 0);
            //     move_ball();
            // }
            // else if (forward == 5)
            // {
            //     trajectory(via3, via2, 0);
            //     move_ball();
            // }
            // else if (forward == 6) //moving away from cursor to bin
            // {
            //     trajectory(via2, via1, 1);
            //     move_ball();
            // }
            // else if(forward == 7)
            // {
            //     pickup = 0;
            //     forward = 0;
            //     if (attached == 1)
            //     {
            //         at_dest = 1;
            //         timer = 3.0f;
            //         success++;
            //     } else {
            //         fail++;
            //     }
            //     attached = 0;
            //     ungrip();
            // }
            activate = 0;
            if (forward == 0)//moving towards cursor from bin
            {
                trajectory(via1, via3, 0);
            }
            else if (forward == 1)
            {
                trajectory(via3, via4, 1);
            }
            else if (forward == 2)
            {
                pickup_ball();
                grip();
                forward = forward+1;
            }
            else if (forward == 3)
            {
                trajectory(via4, via3, 0);
                move_ball();
            }
            else if (forward == 4)
            {
                trajectory(via3, via1, 1);
                move_ball();
            }
            else if(forward == 5)
            {
                pickup = 0;
                forward = 0;
                if (attached == 1)
                {
                    success++;
                } else {
                    fail++;
                }
                attached = 0;
                ungrip();
            }

            move_robot();
        }
        message = "Score: " + success + ". Fail: " + fail;
    }


    // void OnGUI()
    // {
    //    guiStyle.fontSize = 30; //change the font size
    //    GUI.color = Color.yellow;
    //    GUI.Label(new Rect(Screen.width * 1/8, Screen.height * 1 / 8, 200, 20), message, guiStyle);
    // }

    void Q4()//calcualte the q4 angle and rotate joint, always pointing down
    {
        q4 = 180 - q2 - q3;
        Segment_4.transform.RotateAround(Hinge_4.position, Hinge_4.right, q4 - old_q4);
        old_q4 = q4;
    }

    void Q3(float y, float z, float l1, float l2)//calculate the q3 angle and rotate joint
    {
        q3 = Mathf.Rad2Deg * Mathf.Acos(((y * y + z * z) - (l1 * l1 + l2 * l2)) / (2 * l1 * l2));
        Segment_3.transform.RotateAround(Hinge_3.position, Hinge_3.right, q3 - old_q3);
        old_q3 = q3;
    }

    void Q2(float y, float z, float l1, float l2)//calcualte the q2 angle and rotate joint
    {
        q2 = Mathf.Atan2(y, z) - Mathf.Asin(l2 * Mathf.Sin(Mathf.PI - q3 * Mathf.Deg2Rad) / (Mathf.Sqrt(y * y + z * z)));
        q2 = q2 * Mathf.Rad2Deg; //convert to degrees
        Segment_2.transform.RotateAround(Hinge_2.position, Hinge_2.right, q2 - old_q2);
        old_q2 = q2;
    }

    void Q1()//calcualte the q1 angle and rotate joint
    {
        q1 = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
        Segment_1.transform.RotateAround(Segment_1.position, Segment_1.up, q1 - old_q1);
        old_q1 = q1;
    }

    void move_robot()
    {
        z = Grip_pos.position.y; //y corrsponds to z
        y = Grip_pos.position.z - transform.position.z; //
        x = Grip_pos.position.x - transform.position.x; //

        z_offset = z - Point1.position.y; //everything after this line should be in z_offset frame
        y_new = Mathf.Sqrt(x * x + y * y);//calculating the y out of the (y,z) for the 2D side view IK 

        //calcualte current reach length
        r = Mathf.Sqrt(x * x + y * y + z_offset * z_offset);   //current mag of end effeotor pos to sphere center

        //rotate robot joint
        if (r <= r_bound)// is in bound
        {
            Q1();
            Q3(y_new, z_offset, l1, l2);
            Q2(y_new, z_offset, l1, l2);
            Q4();
        }
    }

    void pickup_ball()
    {
        
        if (Mathf.Abs(Ball.position.x - Grip_pos.position.x) <= (0.8f * transform.localScale.x) && //check x aligns
            Mathf.Abs(Ball.position.z - Grip_pos.position.z) <= (0.8f * transform.localScale.x)//check z aligns
            ) //check it's not at destination
        {
            attached = 1;
        } else {
            Debug.Log("Not Attached!");
            Debug.Log("Ball Position x: " + Ball.position.x + ", Grip Position x: " + Grip_pos.position.x);
            Debug.Log("Ball Position z: " + Ball.position.z + ", Grip Position z: " + Grip_pos.position.z);
        }
    }

    void move_ball()
    {
        if (attached == 1)
        {
            Ball.position = Vector3.MoveTowards(Ball.position,
                                                Grip_pos.position - 1.0f * transform.localScale.x * Vector3.up,
                                                100 / transform.localScale.x * Time.fixedDeltaTime); 
            //scale the speed by divding because it is inverse proportional
        }
    }

    // void drop_ball() //drop and ball and generate a new ball
    // {
    //     if (at_dest == 1)
    //     {
    //         //drop ball

    //         //ball disappers or stay
    //         //generate a new ball
    //     }
    // }

    void grip()//grasp ball
    {
        Grip_L.localPosition = Grip_L.localPosition + 0.15f * Vector3.right;
        Grip_R.localPosition = Grip_R.localPosition + 0.15f * Vector3.left;
    }

    void ungrip()//ungripped
    {
        Grip_L.localPosition = Grip_L.localPosition - 0.15f * Vector3.right;
        Grip_R.localPosition = Grip_R.localPosition - 0.15f * Vector3.left;
    }

    Vector3 Grid_pos(Vector3 pos)//write a function that takes in x and y of a position and makes robot pick up at that spot
    {
        return new Vector3(pos.x, (Point1.position.y), pos.z); //point 1 level: horizontal plane 
    }

    void trajectory(Vector3 start, Vector3 end, int reset_speed)
    {
        if ((Grip_pos.position - start).magnitude < 0.5f * (end - start).magnitude - epsilon)
        {
            Grip_pos.position = Vector3.MoveTowards(Grip_pos.position, 0.5f * (end - start) + start, speed * Time.fixedDeltaTime);
            speed = speed * speed_multiplier;
        }
        else if ((Grip_pos.position - start).magnitude < (end - start).magnitude - epsilon)
        {
            Grip_pos.position = Vector3.MoveTowards(Grip_pos.position, end, speed * Time.fixedDeltaTime);
            speed = speed / speed_multiplier;
        }
        else
        {
            forward = forward +1;   
            if (reset_speed == 1)
            {
                speed = int_speed;
            }
            
        }
    }

    //if ball hits ground after dropping, can pick up again
    //if ball is near ground, pos.y = transform.pos+0.5f, change location
    //make the trajectory a function, and combien the back and forth

}


//moving robot arm along trajectory
        // if (pickup == 1)
        // {
        //     if (forward == 0)//moving towards cursor from bin
        //     {
        //         //set start and end point of each trajectory 1
        //         temp2_vec = via1; //starting
        //         temp1_vec = via2; //ending

        //         if ((Grip_pos.position - temp2_vec).magnitude < 0.5f * (temp1_vec - temp2_vec).magnitude - epsilon)
        //         {
        //             Grip_pos.position = Vector3.MoveTowards(Grip_pos.position, 0.5f * (temp1_vec - temp2_vec) + temp2_vec, speed * Time.fixedDeltaTime);
        //             speed = speed * speed_multiplier;
        //         }
        //         else if ((Grip_pos.position - temp2_vec).magnitude < (temp1_vec - temp2_vec).magnitude)
        //         {
        //             Grip_pos.position = Vector3.MoveTowards(Grip_pos.position, temp1_vec, speed * Time.fixedDeltaTime);
        //             speed = speed / speed_multiplier;
        //         }
        //         else
        //         {
        //             forward = 1;
        //             speed = int_speed;//reset speed once moved to cursor
        //         }
        //     }
        //     else if (forward == 1)
        //     {
        //         temp2_vec = via2; //starting
        //         temp1_vec = via3; //ending

        //         if ((Grip_pos.position - temp2_vec).magnitude < 0.5f * (temp1_vec - temp2_vec).magnitude - epsilon)
        //         {
        //             Grip_pos.position = Vector3.MoveTowards(Grip_pos.position, 0.5f * (temp1_vec - temp2_vec) + temp2_vec, speed * Time.fixedDeltaTime);
        //             speed = speed * speed_multiplier;
        //         }
        //         else if ((Grip_pos.position - temp2_vec).magnitude < (temp1_vec - temp2_vec).magnitude)
        //         {
        //             Grip_pos.position = Vector3.MoveTowards(Grip_pos.position, temp1_vec, speed * Time.fixedDeltaTime);
        //             speed = speed / speed_multiplier;
        //         }
        //         else
        //         {
        //             forward = 2;
        //             speed = int_speed; //reset speed once moved to cursor

        //             //pick up object here
        //             pickup_ball();
        //             grip();
        //         }
        //     }
        //     else if (forward == 2)
        //     {
        //         temp1_vec = via3; //starting
        //         temp2_vec = via2; //ending
        //         if ((Grip_pos.position - temp1_vec).magnitude < 0.5f * (temp2_vec - temp1_vec).magnitude - epsilon)
        //         {
        //             Grip_pos.position = Vector3.MoveTowards(Grip_pos.position, 0.5f * (temp2_vec - temp1_vec) + temp1_vec, speed * Time.fixedDeltaTime);
        //             speed = speed * speed_multiplier;
        //         }
        //         else if ((Grip_pos.position - temp1_vec).magnitude < (temp2_vec - temp1_vec).magnitude)
        //         {
        //             Grip_pos.position = Vector3.MoveTowards(Grip_pos.position, temp2_vec, speed * Time.fixedDeltaTime);
        //             speed = speed / speed_multiplier;
        //         }
        //         else //moved back to cursor
        //         {
        //             forward = 3;
        //             speed = int_speed;

        //         }
        //         move_ball();
        //     }
        //     else if (forward == 3) //moving away from cursor to bin
        //     {
        //         temp1_vec = via2; //starting
        //         temp2_vec = via1; //ending

        //         if ((Grip_pos.position - temp1_vec).magnitude < 0.5f * (temp2_vec - temp1_vec).magnitude - epsilon)
        //         {
        //             Grip_pos.position = Vector3.MoveTowards(Grip_pos.position, 0.5f * (temp2_vec - temp1_vec) + temp1_vec, speed * Time.fixedDeltaTime);
        //             speed = speed * speed_multiplier;
        //         }
        //         else if ((Grip_pos.position - temp1_vec).magnitude < (temp2_vec - temp1_vec).magnitude)
        //         {
        //             Grip_pos.position = Vector3.MoveTowards(Grip_pos.position, temp2_vec, speed * Time.fixedDeltaTime);
        //             speed = speed / speed_multiplier;
        //         }
        //         else //moved back to cursor
        //         {
        //             pickup = 0;
        //             forward = 0;
        //             speed = int_speed;
        //             // if (attached == 1)
        //             // {
        //             //     at_dest = 1;
        //             // }
        //             attached = 0;

        //             ungrip();
        //         }
        //         move_ball();
        //     }