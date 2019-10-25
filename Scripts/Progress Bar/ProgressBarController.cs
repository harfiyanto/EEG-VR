using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarController : MonoBehaviour
{
    public int activate = 0;
    public float interval = 1.0f;
    private Vector3 maxScale;
    private Vector3 currScale;
    private float timeLeft = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        maxScale = transform.localScale;
        timeLeft = interval;
    }

    // Update is called once per frame
    void Update()
    {
        // Activated, increases the size of progress bar
        if (activate == 1) {
            currScale = maxScale;
            if (timeLeft > 0) {
                currScale.x = maxScale.x * (1 - timeLeft/interval);
                transform.localScale = currScale;
                timeLeft -= Time.deltaTime; 
            } else {
                activate = 0;
                currScale.x = maxScale.x * (1 - timeLeft/interval);
                Debug.Log("Last scale value = " + currScale.x);
            }
        // Inactive
        } else {
            timeLeft = interval;
            currScale.x = 0;
            transform.localScale = currScale;
        }
    }
}
