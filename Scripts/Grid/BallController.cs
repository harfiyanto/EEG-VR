using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Vector3 ballPos;
    public int randomized = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        ballPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (randomized == 1) {
            Debug.Log ("Randomize!");
            transform.position = ballPos;
            randomized = 0;
        }
    }
}
