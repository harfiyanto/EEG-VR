using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//update 1.5v intends to fix the "moving robot in x and z(unity axis), makes the cursor goes offset bug"
//update: new boundary given via point in robot move V4 onwards./
//remove up and down

public class cursor_v4 : MonoBehaviour
{
    float speed;
    private int y_dir;
    private int x_dir;
    private int z_dir;
    private float z, y, x;
    private int x_udp;
    private int y_udp;
    private int z_udp;
    private float r_bound;
    private float l1, l2;
    private float x_circle, y_circle;
    private Transform Point1;
    private Transform Point2;
    private Transform Point3;
    private Transform Robot;
    private Vector3 dir;
    private Vector3 mag;
    private Vector3 point1_to_cursor_new;
    private Vector3 point1_y_new;
    // Start is called before the first frame update

    //void OnGUI()
    //{
    //    GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height * 7 / 8, 200, 20), "This is a cool robot. Sep 21");
    //}

    void Start()
    {
        speed = 1;
        y_dir = 0;
        x_dir = 0;
        z_dir = 0;
        x_udp = 0;
        y_udp = 0;
        z_udp = 0;
        Point1 = gameObject.transform.parent.Find("Position reference/Point1");
        Point2 = gameObject.transform.parent.Find("Position reference/Point2");
        Point3 = gameObject.transform.parent.Find("Position reference/Point3");
        l1 = Point2.position.y - Point1.position.y; //calcuate the arm length
        l2 = Point3.position.y - Point2.position.y; //calcuate the arm length
        r_bound = l1 + l2;
        Debug.Log("Rbound before: " + r_bound);
        //find x in a cicle of radius r_bound given y of point1.y
        // y = (Point1.position.y - transform.parent.position.y) * 0.5f;
        // x = Mathf.Sqrt(r_bound * r_bound - y * y); //x^2 +y^2 = r^2, transpose to x
        // //print(x);
        // r_bound = x;
        // Debug.Log("Rbound after: " + r_bound + ". Point1.y: " + Point1.position.y + " transform parent y: " + transform.parent.position.y);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            y_dir = 1;
            // Debug.Log("W detected"); 
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

        //set a constraint space with sphere function and correct the z,y,x, find closest point on the sphere funcition.

        dir = new Vector3(x_dir + x_udp, z_dir + z_udp, y_dir + y_udp);


        //calculate the vector from point1(robot tower height) to cursor(robot end effector)
        point1_to_cursor_new = new Vector3(transform.position.x - transform.parent.position.x, transform.position.z - transform.parent.position.z, transform.position.y);
        point1_y_new = new Vector3(-transform.parent.position.x, -transform.parent.position.z, Point1.position.y);//Point1.position.y * Vector3.up

        mag = transform.position - (Point1.position.y * Vector3.up + transform.parent.position) + (dir * speed * Time.deltaTime);
        if (mag.magnitude < r_bound && mag.y >= transform.parent.position.y) //up to here z contraints is not working, in unity z is y
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }
        else if (dir != Vector3.zero)
        {
            Debug.Log("mag magnitude: " + mag.magnitude + " mag y: " + mag.y);
            Debug.Log("r_bound: " + r_bound + " parent pos z: " + transform.parent.position.z);
            // Debug.Log("max reach"); //convert this to screen output
        }


    }
}
