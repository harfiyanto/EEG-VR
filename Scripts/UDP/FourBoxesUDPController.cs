/*
UDP Controller for Four Boxes (Indicators)
*/
using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class FourBoxesUDPController : MonoBehaviour
{

	// Local host IP addresss
	public string IP = "127.0.0.1";

	//	Set up the ports
	public int portLocal = 8002;	// Receiving Port
	public int portRemote = 8003;	// Sending Port

	// Create necessary UdpClient objects
	UdpClient client;
	IPEndPoint remoteEndPoint;

	// Receiving Thread
	Thread receiveThread;
	// Message to be sent
	string strMessageSend = "";

	// Received Messsage
	public string lastReceivedUDPPacket = "";
	public string allReceivedUDPPackets = "";

	// Controller References (Implementation Specific)
	// Modify parts of the controller script on those objects
	private MessageController mc; // MessageBox Controller Reference
	private IndicatorController ic1; 	// Indicator 1 Controller Reference
	private IndicatorController ic2;	// Indicator 2 Controller Reference
	private IndicatorController ic3;	// Indicator 3 Controller Reference
	private IndicatorController ic4;	// Indicator 4 Controller Reference
	// private CursorController cursor; // Cursor Controller
	private TimingBarController timingBar;
	private EightbyEightController gridc; 
	
	// start from Unity3d
	public void Start ()
	{
		init ();
	}

	// OnGUI is called for rendering and handling GUI events. Can be called multiple times per frame
	void OnGUI ()
	{
		// Rect rectObj = new Rect (500, 0, 500, 400);
		// GUIStyle style = new GUIStyle ();
		// style.alignment = TextAnchor.UpperLeft;
		// GUI.Box (rectObj, "# UDP Object Receive\n127.0.0.1:" + portLocal + "\n"
		// + "\nLast Packet: \n" + lastReceivedUDPPacket
		// + "\n\nAll Messages: \n" + allReceivedUDPPackets
		// 	, style);

		// strMessageSend = GUI.TextField (new Rect (500, 420, 140, 20), strMessageSend);
		// if (GUI.Button (new Rect (500, 200, 40, 20), "send")) {
		// 	sendData (strMessageSend + "\n");
		// }  


	}

	// Initialization code
	private void init ()
	{
		// Initialize (seen in comments window)
		print ("UDP Object init()");

		// Initialize the reference for other game objects (Implementation Specific)
		ic1 = GameObject.Find("Indicator 1").gameObject.GetComponent<IndicatorController>();
		ic2 = GameObject.Find("Indicator 2").gameObject.GetComponent<IndicatorController>();
		ic3 = GameObject.Find("Indicator 3").gameObject.GetComponent<IndicatorController>();
		ic4 = GameObject.Find("Indicator 4").gameObject.GetComponent<IndicatorController>();
		timingBar = GameObject.Find("Timing Bar").gameObject.GetComponent<TimingBarController>();
		// cursor = GameObject.Find("Cursor").gameObject.GetComponent<CursorController>();
		gridc = GameObject.Find("8x8 Grid Quad").gameObject.GetComponent<EightbyEightController>();

		// Create remote endpoint (to Matlab) 
		remoteEndPoint = new IPEndPoint (IPAddress.Parse (IP), portRemote);

		// Create local client
		client = new UdpClient (portLocal);

		// Local endpoint define (where messages are received)
		// Create a new thread for reception of incoming messages
		// Thread runs in the background and does not interfere with the main application
		receiveThread = new Thread (
			new ThreadStart (ReceiveData));

		// Run the thread in the background
		receiveThread.IsBackground = true;
		receiveThread.Start ();

	}


	// Receive data, update packets received
	private  void ReceiveData ()
	{
		while (true) {
			try {
				IPEndPoint anyIP = new IPEndPoint (IPAddress.Any, 0);
				byte[] data = client.Receive (ref anyIP);
                // int command = 0;
				// string text = Encoding.UTF8.GetString (data);
				
				// lastReceivedUDPPacket = Encoding.UTF8.GetString(data[0]);
				// allReceivedUDPPackets = allReceivedUDPPackets + Encoding.UTF8.GetString(data[0]);

				double[] convertedData = new double[data.Length / 8];
                for(int ii = 0; ii < convertedData.Length; ii++)
                    convertedData[ii] = BitConverter.ToDouble(data, 8 * ii);


				print (">> Data is " + convertedData[0]);
				// Case 1: Top cube is active
				if ((int)convertedData[0] == 1) {
					ic1.activate = 1;
					ic2.activate = 0;
					ic3.activate = 0;
					ic4.activate = 0;
					timingBar.activate = 1;
					// cursor.x_udp = 1;
                    // cursor.y_udp = 0;
                    // cursor.z_udp = 0;
					gridc.udpCMD = 1;
				// Case 2: Right cube is active
				} else if ((int)convertedData[0] == 2) {
					ic1.activate = 0;
					ic2.activate = 1;
					ic3.activate = 0;
					ic4.activate = 0;
					timingBar.activate = 1;
					// cursor.x_udp = -1;
                    // cursor.y_udp = 0;
                    // cursor.z_udp = 0;
					gridc.udpCMD = 2;
				// Case 3: Bottom cube is active
				} else if ((int)convertedData[0] == 3) {
					ic1.activate = 0;
					ic2.activate = 0;
					ic3.activate = 1;
					ic4.activate = 0;
					timingBar.activate = 1;
					// cursor.x_udp = 0;
                    // cursor.y_udp = 1;
                    // cursor.z_udp = 0;
					gridc.udpCMD = 3;
				// Case 4: Left cube is active
				} else if ((int)convertedData[0] == 4) {
					ic1.activate = 0;
					ic2.activate = 0;
					ic3.activate = 0;
					ic4.activate = 1;
					timingBar.activate = 1;
					// cursor.x_udp = 0;
                    // cursor.y_udp = -1;
                    // cursor.z_udp = 0;
					gridc.udpCMD = 4;
				// Case 5: None of the cube is active
				} else if ((int)convertedData[0] == 0) {
					ic1.activate = 0;
					ic2.activate = 0;
					ic3.activate = 0;
					ic4.activate = 0;
					timingBar.activate = 1;
					// cursor.x_udp = 0;
                    // cursor.y_udp = 0;
                    // cursor.z_udp = 0;
					gridc.udpCMD = 0;
				}
				
				lastReceivedUDPPacket = convertedData[0].ToString ();
				Debug.Log(lastReceivedUDPPacket);

			} catch (Exception err) {
				print (err.ToString ());
			}
		}
	}

	// Send data
	private void sendData (string message)
	{
		try {
			byte[] data = Encoding.UTF8.GetBytes (message);
			client.Send (data, data.Length, remoteEndPoint);
			
		} catch (Exception err) {
			print (err.ToString ());
		}
	}

	// getLatestUDPPacket, clears all previous packets
	public string getLatestUDPPacket ()
	{
		allReceivedUDPPackets = "";
		return lastReceivedUDPPacket;
	}

	// Prevent crashes - close clients and threads properly!
	void OnDisable ()
	{ 
		if (receiveThread != null)
			receiveThread.Abort (); 

		client.Close ();
	}

}
