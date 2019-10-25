using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrequencyTextController : MonoBehaviour
{
    private float time = 0.0f;
    public float startingTime = 5.0f;
    public float interval = 3.0f;
    public int repetition = 15;
    private int count = 1;
    public float waitTime = 1.0f;
    public GameObject sphereObject;
    private SphereController sc;
    public int test;

 
    Text ttext;
 
    void Awake ()
    {
        ttext = GetComponent<Text> ();
    }
    
    void Start() 
    {
        // sphereObject = GameObject.Find("Sphere");
        print("Object is:" + sphereObject);
        sc = sphereObject.GetComponent<SphereController>();
    }

    void Update()
    {
        ttext.text = "Frequency: " + sc.frequency[sc.mode];
        if (count <= repetition) {
            time += Time.deltaTime;
            if (time > startingTime + 4*(interval + waitTime)) {
                time = 0.0f;
                startingTime = 0.0f;
                count++;
            } else {
                ttext.text = ttext.text + " Trial: " + count;
            }
        } else {
            ttext.text = "Experiment is done!";
        }
        
    }

//     GameObject go = GameObject.Find("mainCharacter");
//  controllerScript cs = go.GetComponent<controllerScript>();
//  float thisObjectMove = cs.move;
}
