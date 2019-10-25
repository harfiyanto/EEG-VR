using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    // private int score;
    private RobotController robot;
    private Text score;
    private string message;
    

    
    // Start is called before the first frame update
    void Start()
    {
        robot = GameObject.Find("Robot").gameObject.GetComponent<RobotController>();
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // message = "Score: " + robot.success + ". Fail: " + robot.fail;
        // score.text = message.ToString();
    }

    private GUIStyle guiStyle = new GUIStyle();
    // void OnGUI()
    // {
    //    guiStyle.fontSize = 30; //change the font size
    //    GUI.color = Color.yellow;
    //    GUI.Label(new Rect(Screen.swidth * 1/4, Screen.height * 4 / 8, 200, 20), message, guiStyle);
    // }
}
