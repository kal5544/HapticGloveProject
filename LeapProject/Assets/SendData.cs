using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class SendData : MonoBehaviour 
{
	//Setup parameters to connect to Arduino
	public static SerialPort sp = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
	public static string strIn;        
	
	// Use this for initialization
	void Start () 
	{
		OpenConnection();
	}
	
	void Update()
	{
		//Read incoming data

		strIn = sp.ReadLine ();
		Debug.Log (strIn);
		//You can also send data like this
		//sp.Write("1");
		
		
	}

	public void SendHello()
	{
		sp.Write ("Hello");
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
