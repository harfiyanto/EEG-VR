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

public class UDPFiveController : MonoBehaviour
{

	// Local host IP addresss
	public string IP = "127.0.0.1";

	//	Set up the ports
	public int portLocal = 8002;	// Receiving Port
	public int portRemote = 8003;	// Sending Port

	public int invalidInterval = 0;
	private double message;
	private int counter = 0;
	public int validMessage = 1;
	// private TimingBarFiveController timingBar;
    private QuadrantFiveController grid;
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
	
	// start from Unity3d
	public void Start ()
	{
		init ();
		grid = GameObject.Find("8x8 Grid Step").gameObject.GetComponent<QuadrantFiveController>();
		// timingBar = GameObject.Find("Timing Bar").gameObject.GetComponent<TimingBarFiveController>();
		
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
		
		// Valid Message
		validMessage = 1;
		counter = 0;
		message = 17;

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


				// print (">> Data is " + convertedData[0]);
				// Case 1: Top cube is active
				if (validMessage == 1) {
					message = convertedData[0];
					Debug.Log("Valid Message: " + message);
					grid.udpCMD = (int) message;
					grid.commandPending = 1;
					counter = invalidInterval;
					// timingBar.activate = 1;
				}
				
				lastReceivedUDPPacket = convertedData[0].ToString ();

				if (counter == 0) {
					validMessage = 1;
					counter = invalidInterval;

				} else {
					validMessage = 0;
					counter--;
				}
				
				// Debug.Log("message count: " + counter);

				Debug.Log(lastReceivedUDPPacket);

			} catch (Exception err) {
				Debug.Log("exception detected");
				print (err.ToString());
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
