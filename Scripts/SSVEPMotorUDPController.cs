using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSVEPMotorUDPController : MonoBehaviour
{
    private UDPController udp1;
    // private UDPController udp2;
    private string message1;
    private string prevmessage1;
    private string message2;
    private string prevmessage2;
    private MessageController mc; // MessageBox Controller Reference
	private IndicatorController ic1; 	// Indicator 1 Controller Reference
	private IndicatorController ic2;	// Indicator 2 Controller Reference
	private IndicatorController ic3;	// Indicator 3 Controller Reference
	private IndicatorController ic4;	// Indicator 4 Controller Reference
    private IndicatorController ic5;	// Indicator 4 Controller Reference
    private IndicatorController ic6;	// Indicator 4 Controller Reference

	// private CursorController cursor; // Cursor Controller
	private TimingBarController timingBar;
	// private EightbyEightController gridc; 
    
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the reference for other game objects (Implementation Specific)
		ic1 = GameObject.Find("Indicator 1").gameObject.GetComponent<IndicatorController>();
		ic2 = GameObject.Find("Indicator 2").gameObject.GetComponent<IndicatorController>();
		ic3 = GameObject.Find("Indicator 3").gameObject.GetComponent<IndicatorController>();
		ic4 = GameObject.Find("Indicator 4").gameObject.GetComponent<IndicatorController>();
        ic5 = GameObject.Find("Indicator 5").gameObject.GetComponent<IndicatorController>();
		ic6 = GameObject.Find("Indicator 6").gameObject.GetComponent<IndicatorController>();

		timingBar = GameObject.Find("Timing Bar").gameObject.GetComponent<TimingBarController>();
		// cursor = GameObject.Find("Cursor").gameObject.GetComponent<CursorController>();
		// gridc = GameObject.Find("8x8 Grid Quad").gameObject.GetComponent<EightbyEightController>();
        
        udp1 = GameObject.Find("UDP 1").gameObject.GetComponent<UDPController>();
        // udp2 = GameObject.Find("UDP 2").gameObject.GetComponent<UDPController>();
        prevmessage1 = udp1.lastReceivedUDPPacket;
        // prevmessage2 = udp2.lastReceivedUDPPacket;
    }

    // Update is called once per frame
    void Update()
    {
        message1 = udp1.lastReceivedUDPPacket;
        // message2 = udp2.lastReceivedUDPPacket;
        // if (message1 == prevmessage1 && message2 == prevmessage2) {
            
        // } else {
        //     Debug.Log("Message 1: " + message1 + ", Message 2: " + message2);
        //     prevmessage1 = message1;
        //     prevmessage2 = message2;
        // }
		if (udp1.validMessage == 1) {
			// Debug.Log("Valid Message: " + message1);
		}

		ic1.activate = 0;
		ic2.activate = 0;
		ic3.activate = 0;
		ic4.activate = 0;
        ic5.activate = 0;
        ic6.activate = 0;

        if (udp1.message == 1 && udp1.validMessage == 1) {
			ic1.activate = 1;
            ic5.activate = 1;
			timingBar.activate = 1;
		} else if (udp1.message == 2 && udp1.validMessage == 1) {
			ic2.activate = 1;
            ic5.activate = 1;
			timingBar.activate = 1;
		} else if (udp1.message == 3 && udp1.validMessage == 1) {
			ic3.activate = 1;
            ic5.activate = 1;
			timingBar.activate = 1;
		} else if (udp1.message == 4 && udp1.validMessage == 1) {
			ic4.activate = 1;
            ic5.activate = 1;
			timingBar.activate = 1;
		} else if (udp1.message == 5 && udp1.validMessage == 1) {
            ic6.activate = 1;
			timingBar.activate = 1;
		} else if (udp1.message == 6 && udp1.validMessage == 1) {
			ic1.activate = 1;
            ic6.activate = 1;
			timingBar.activate = 1;
		} else if (udp1.message == 7 && udp1.validMessage == 1) {
			ic2.activate = 1;
            ic6.activate = 1;
			timingBar.activate = 1;
		} else if (udp1.message == 8 && udp1.validMessage == 1) {
			ic3.activate = 1;
            ic6.activate = 1;
			timingBar.activate = 1;
		} else if (udp1.message == 9 && udp1.validMessage == 1) {
			ic4.activate = 1;
            ic6.activate = 1;
			timingBar.activate = 1;
		} else if (udp1.message ==  0 && udp1.validMessage == 1) {
            ic5.activate = 1;
			timingBar.activate = 1;
		}
    }
}
