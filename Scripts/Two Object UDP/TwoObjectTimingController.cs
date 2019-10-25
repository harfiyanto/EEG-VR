using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoObjectTimingController : MonoBehaviour
{
    private Color altColor = Color.grey;
    private Renderer rend; 
    private CheckerController cc1;
    private CheckerController cc2;
    public int activate = 0;
    public float interval = 1.0f;
    public float delay = 1.0f;
    private int delayActive = 0;
    private float delayTimer = 0.0f;
    private Vector3 maxScale;
    private Vector3 currScale;
    private float timeLeft = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        maxScale = transform.localScale;
        timeLeft = interval;
        cc1 = GameObject.Find("Checker 1").gameObject.GetComponent<CheckerController>();
        cc2 = GameObject.Find("Checker 2").gameObject.GetComponent<CheckerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Activated, increases the size of progress bar
        if (activate == 1) {
            cc1.activate = 1;
            cc2.activate = 1;
            currScale = maxScale;
            rend = GetComponent<Renderer>();
            if (timeLeft > 0 && delayActive == 0) {
                currScale.x = maxScale.x * (1 - timeLeft/interval);
                transform.localScale = currScale;
                timeLeft -= Time.deltaTime; 
            } else if (timeLeft <= 0  && delayActive == 0){
                delayTimer = delay;
                delayActive = 1;
                rend.material.color = Color.grey;
                currScale.x = 0.0f;
                transform.localScale = currScale;
            } else if (delayTimer <= 0  && delayActive == 1){
                transform.localScale = currScale;
                delayTimer = delay;
                delayActive = 0;
                rend.material.color = Color.green;
                timeLeft = interval;
            } else if (delayTimer > 0  && delayActive == 1){
                currScale.x = maxScale.x * (delayTimer/delay);
                transform.localScale = currScale;
                delayTimer -= Time.deltaTime;
            } 
        // Inactive
        } else {
            timeLeft = interval;
            currScale.x = 0;
            transform.localScale = currScale;
        }
    }

    void Activate() {
        activate = 1;
    }
}
