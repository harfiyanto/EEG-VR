using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    Text currMessage;
    
    // Start is called before the first frame update
    void Start()
    {
        currMessage = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update the message
    public void UpdateMessage(string message) 
    {   
        currMessage.text = "Message: " + message;
    }
}
