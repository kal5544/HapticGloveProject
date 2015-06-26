using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class SendMotorData : MonoBehaviour {

	public bool bluetoothConnected;

	public static SerialPort sp = new SerialPort("COM8", 115200, Parity.None, 8, StopBits.One);
	public static byte[] magnitudesToSend = new byte[20];

	void Start()
	{
		if(bluetoothConnected)
			sp.Open ();
	}

	void OnApplicationQuit()
	{
		magnitudesToSend = new byte[20];
		if (bluetoothConnected) {
			sp.Write (magnitudesToSend, 0, 20);
			sp.Close ();
		}
	}

	void OnDestroy()
	{
		magnitudesToSend = new byte[20];
		if (bluetoothConnected) {
			sp.Write (magnitudesToSend, 0, 20);
			sp.Close ();
		}
	}

	// Update is called once per frame
	void LateUpdate () {
		string debugString = "Values: ";
		for (int i = 0; i < 20; i++)
			debugString += magnitudesToSend [i].ToString();
		Debug.Log (debugString);
		if(bluetoothConnected)
			sp.Write (magnitudesToSend, 0, 20);
	}
}
