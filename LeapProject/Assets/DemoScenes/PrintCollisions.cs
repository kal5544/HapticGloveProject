using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class PrintCollisions : MonoBehaviour {

	//Setup parameters to connect to Arduino
	public static SerialPort sp = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);
	public int motorPin;

	private string debugString = "";

	//[0] is pin, [1] is data
	private byte[] motorOn = new byte[2];
	private bool meshEdited = false;

	private float forceMagnitude = 0;
	//Force to be sent to vibrating motor
	private int force_vib = 0;
	//Force to be sent to servo
	private int force_servo = 0;
	//(num range of motor)/(max force)
	const float forceCoeff = (155/100);

	void Start()
	{
		//Debug.Log (motorPin);
		motorOn = new byte[]{(byte)motorPin, 1};
		OpenConnection ();
		sp.ReadTimeout = 1;
	}

	void OnCollisionEnter(Collision coll)
	{
		//Debug.Log ("modifying mesh");
		meshEdited = GameObject.Find ("HandControllerSandBox").GetComponent<MeshFunctionality> ().ModifyMesh (coll);
		if(!meshEdited)
		{
			motorOn [1] = (byte)255;
			//Debug.Log ("Motor Pin: " + (int)motorOn [0] + ", Motor Magnitude: " + (int)motorOn [1]);
			sp.Write (motorOn, 0, 2);
		}

	}

	void OnCollisionExit(Collision coll)
	{
		if (!meshEdited) {
			motorOn [1] = (byte)0;
			//Debug.Log ("Motor Pin: " + (int)motorOn [0] + ", Motor Magnitude: " + (int)motorOn [1]);
			sp.Write (motorOn, 0, 2);
		}

	}

	void OnCollisionStay(Collision coll)
	{
		forceMagnitude = Vector3.Dot(coll.contacts[0].normal, coll.relativeVelocity)*coll.rigidbody.mass*10;
		Debug.Log ("force magnitude: " + forceMagnitude);
		forceMagnitude = Mathf.Abs ((int)forceMagnitude);
		if (forceMagnitude > 100) {
			forceMagnitude = 100;
		}
		force_vib = (int)Mathf.Floor ((forceCoeff * forceMagnitude)+100);
		debugString = gameObject.name + " in contact with " + coll.gameObject.name + " with force: " + forceMagnitude;
		Debug.Log (debugString);
		motorOn [1] = (byte)force_vib;
	}

	void OnDestroy()
	{
		motorOn [1] = (byte)0;
		sp.Write (motorOn, 0, 2);
	}

	//Function connecting to Arduino
	public void OpenConnection() 
	{
		if (!sp.IsOpen) {
			sp.Open ();
			//Debug.Log ("opening port");
		}
			/*
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
*/
	}


}
