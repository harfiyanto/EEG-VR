using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cursor : MonoBehaviour
{
    float speed;
    int y_dir;
    int x_dir;
    int z_dir;
    float z, y, x;
    public int x_udp;
    public int y_udp;
    public int z_udp;
    public float r_bound;
    public float l1, l2;
    public Transform Point1;
    public Transform Point2;
    public Transform Point3;
    public Transform Robot;
    Vector3 dir;
    Vector3 mag;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
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
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey("w")){
            y_dir = 1;
        }
        else if(Input.GetKey("s"))
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

        if (Input.GetKey("q"))
        {
            z_dir = 1;
        }
        else if (Input.GetKey("e"))
        {
            z_dir = -1;
        }
        else
        {
            z_dir = 0;
        }

        //set a constraint space with sphere function and correct the z,y,x, find closest point on the sphere funcition.

        dir = new Vector3(x_dir + x_udp, z_dir + z_udp, y_dir + y_udp);
        //transform.Translate(dir * speed * Time.deltaTime);
        //print(dir);

        
        mag = transform.position + (dir * speed * Time.deltaTime) - Point1.position.y * Vector3.up;
        if (mag.magnitude < r_bound && mag.y >= 0) //up to here z contraints is not working, in unity z is y
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }

    }
}
