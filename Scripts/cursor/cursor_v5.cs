using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//update 1.5v intends to fix the "moving robot in x and z(unity axis), makes the cursor goes offset bug"
//update: new boundary given via point in robot move V4 onwards./
//remove up and down
//deleted some unused stuff

public class cursor_v5 : MonoBehaviour
{
    float speed_int;
    private float speed;
    private int y_dir;
    private int x_dir;
    private int x_udp;
    private int y_udp;
    private float r_bound;
    private float l1, l2;
    private float x_circle, y_circle;
    private Transform Point1;
    private Transform Point2;
    private Transform Point3;
    private Transform Robot;
    private Vector3 dir;
    private Vector3 mag;

    void Start()
    {
        speed_int = 10;
        speed = speed_int* transform.parent.localScale.x; //scale the speed
        y_dir = 0;
        x_dir = 0;
        x_udp = 0;
        y_udp = 0;
        Point1 = gameObject.transform.parent.Find("Position reference/Point1");
        Point2 = gameObject.transform.parent.Find("Position reference/Point2");
        Point3 = gameObject.transform.parent.Find("Position reference/Point3");
        l1 = Point2.position.y - Point1.position.y; //calcuate the arm length
        l2 = Point3.position.y - Point2.position.y; //calcuate the arm length
        r_bound = l1 + l2;

        //find x in a cicle of radius r_bound given y of point1.y
        y_circle = (transform.parent.localScale.x); //plus 1 up, via point 2
        x_circle = Mathf.Sqrt(r_bound * r_bound - y_circle * y_circle); //x^2 +y^2 = r^2, transpose to x
        r_bound = x_circle;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            y_dir = 1;
        }
        else if (Input.GetKey("s"))
        {
            y_dir = -1;
        }
        else
        {
            y_dir = 0;
        }

        if (Input.GetKey("d"))
        {
            x_dir = 1;
        }
        else if (Input.GetKey("a"))
        {
            x_dir = -1;
        }
        else
        {
            x_dir = 0;
        }

        dir = new Vector3(x_dir + x_udp, 0, y_dir + y_udp);

        //calculate the vector from point1(robot tower height) to cursor(robot end effector)
        mag = transform.position - ((Point1.position.y- transform.parent.position.y) * Vector3.up + transform.parent.position) + (dir * speed * Time.deltaTime);
        if (mag.magnitude < r_bound) //up to here z contraints is not working, in unity z is y
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }
        else if (dir != Vector3.zero)
        {
            Debug.Log("max reach"); //convert this to screen output
        }


    }
}
