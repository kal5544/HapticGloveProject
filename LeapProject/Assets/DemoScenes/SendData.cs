using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class SendData : MonoBehaviour 
{
	//Setup parameters to connect to Arduino
	public static SerialPort sp = new SerialPort("COM6", 9600, Parity.None, 8, StopBits.One);
	public static string strIn;        

	private int motorPin = 2;
	private byte[] motorOn = new byte[]{2, 255};
	// Use this for initialization
	void Start () 
	{
		OpenConnection();
	}

	public void ToggleMotor()
	{
		Debug.Log ("Sent: " + motorOn);
		sp.Write(motorOn,0,2);
		if (motorOn[1] == (byte)255) 
		{
			motorOn[1] = (byte)0;
		} 
		else 
		{
			motorOn[1] = (byte)255;
		}

		if (motorOn [1] == (byte)255) 
		{
			motorOn [0] = (byte)(motorPin++);
			if (motorPin > (byte)6)
				motorPin = (byte)2;
		}

	}
	
	//Function connecting to Arduino
	public void OpenConnection() 
	{
		if (sp != null) 
		{
			if (sp.IsOpen) 
			{
				sp.Close();
				Debug.Log("Closing port, because it was already open!");
			}
			else 
			{
				sp.Open();  // opens the connection
				sp.ReadTimeout = 50;  // sets the timeout value before reporting error
				Debug.Log("Port Opened!");
			}
		}
		else 
		{
			if (sp.IsOpen)
			{
				Debug.Log("Port is already open");
			}
			else 
			{
				Debug.Log("Port == null");
			}
		}
	}
	
	void OnApplicationQuit() 
	{
		sp.Close();
	}
}
