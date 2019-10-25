using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingBarFiveController : MonoBehaviour
{
    private Color altColor = Color.grey;
    private Renderer rend; 
    public int activate = 0;
    private int activated = 0;
    public float interval = 1.0f;
    public float delay = 0.9f;
    private int delayActive = 0;
    private float delayTimer = 0.0f;
    private Vector3 maxScale;
    private Vector3 currScale;
    private float timeLeft = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        maxScale = transform.localScale;
        currScale = maxScale;
        // currScale.x = 0;
        transform.localScale = currScale;
        rend.material.color = Color.grey;
        timeLeft = interval;
        delayActive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (activate == 1 && activated == 0) {
            rend.material.color = Color.green;
            activate = 0;
            activated = 1;
            timeLeft = 0.0f;
        }
        
        // Activated, increases the size of progress bar
        if (activated == 1) {
            Debug.Log("Timing Bar Activated");
            if (timeLeft > interval){
                Debug.Log("Timing Bar Deactivated");
                activated = 0;
                rend.material.color = Color.grey;
                currScale.x = maxScale.x;
            } else {
                currScale.x = maxScale.x * (1 - timeLeft/interval);
            }
            timeLeft += Time.deltaTime;
            transform.localScale = currScale;
        }
            // if (timeLeft > 0 && delayActive == 0) {
            //     currScale.x = maxScale.x * (1 - timeLeft/interval);
            //     transform.localScale = currScale;
            //     timeLeft -= Time.deltaTime; 
            // } else if (timeLeft <= 0  && delayActive == 0){
            //     delayTimer = delay;
            //     delayActive = 1;
            //     rend.material.color = Color.grey;
            //     currScale.x = 0.0f;
            //     transform.localScale = currScale;
            // } else if (delayTimer > 0  && delayActive == 1){
            //     currScale.x = maxScale.x * (delayTimer/delay);
            //     transform.localScale = currScale;
            //     delayTimer -= Time.deltaTime;
            // } else if (delayTimer <= 0  && delayActive == 1){
            //     currScale.x = 0.0f;
            //     transform.localScale = currScale;
            //     delayActive = 0;
            //     rend.material.color = Color.green;
            //     activate = 0;
            //     timeLeft = interval;
            //     // timeLeft = interval;
            // }
            // Debug.Log("")
        // Inactive
    }
    
    void Activate() {
        activate = 1;
    }
}
