using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class PrintCollisions : MonoBehaviour {

	//Setup parameters to connect to Arduino
	public static SerialPort sp = new SerialPort("COM6", 9600, Parity.None, 8, StopBits.One);
	public int motorPin;

	private string debugString = "";

	private byte[] motorOn = new byte[2];

	void Start()
	{
		Debug.Log (motorPin);
		motorOn = new byte[]{(byte)motorPin, 255};
		OpenConnection ();
	}

	void OnCollisionEnter(Collision coll)
	{
		//Debug.Log ("modifying mesh");
		//GameObject.Find ("HandControllerSandBox").GetComponent<MeshFunctionality> ().ModifyMesh (coll);
		motorOn [1] = (byte)255;
		Debug.Log ("Motor Pin: " + (int)motorOn[0] + ", Motor Magnitude: " + (int)motorOn [1]);
		sp.Write (motorOn, 0, 2);

	}

	void OnCollisionExit(Collision coll)
	{
		motorOn [1] = (byte)0;
		Debug.Log ("Motor Pin: " + (int)motorOn[0] + ", Motor Magnitude: " + (int)motorOn [1]);
		sp.Write (motorOn, 0, 2);

	}

	void OnCollisionStay(Collision coll)
	{
		debugString = gameObject.name + " in contact with " + coll.gameObject.name + " with force: " + Vector3.Dot (coll.contacts[0].normal, coll.relativeVelocity)*coll.rigidbody.mass;
		//Debug.Log (debugString);
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


}
