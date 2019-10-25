using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public float damping = 2.0f;
    private float rotate;
    public int targetX = 0;
    public int targetY = 0;
    public int targetZ = 0;
    public Quaternion targetRotate;
    private Vector3 angles;
    
    // Start is called before the first frame update
    void Start()
    {
        angles = new Vector3(targetX,targetY,targetZ);
        targetRotate = Quaternion.Euler(angles);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotate, damping * Time.deltaTime);
    }
}
