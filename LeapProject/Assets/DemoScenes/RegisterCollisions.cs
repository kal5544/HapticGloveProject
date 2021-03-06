﻿using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class RegisterCollisions : MonoBehaviour {

	//Setup parameters to connect to bluetooth chip
	public int motorPin = 4;

	private string debugString = "";

	//[0] is pin, [1] is data
	private byte[] motorOn = new byte[2];
	private bool meshEdited = false;

	private float forceMagnitude = 0;
	//Force to be sent to vibrating motor
	private int force_vib = 0;
	//Force to be sent to servo
	private int force_servo = 0;
	// maximum force from collisions
	private int maxForce = 2;
	//(num range of motor)/(max force)
	private float forceCoeff;

	void Start()
	{
		forceCoeff = 155 / maxForce;
		//Debug.Log (motorPin);
		motorOn = new byte[]{(byte)motorPin, 1};
	}

	void OnCollisionEnter(Collision coll)
	{

		SendMotorData.magnitudesToSend [motorPin] = (byte)1;
		//Debug.Log ("modifying mesh");
		meshEdited = GameObject.Find ("SandBox").GetComponent<MeshFunctionality> ().ModifyMesh (coll);
		//if(!meshEdited)
		//{
			//motorOn [1] = (byte)255;
			//Debug.Log ("Motor Pin: " + (int)motorOn [0] + ", Motor Magnitude: " + (int)motorOn [1]);
			//sp.Write (motorOn, 0, 2);
		//}

	}

	void OnCollisionExit(Collision coll)
	{
		SendMotorData.magnitudesToSend [motorPin] = (byte)0;
	}

	void OnCollisionStay(Collision coll)
	{
		/*
		if (coll.rigidbody != null) {
			forceMagnitude = Vector3.Dot (coll.contacts [0].normal, coll.relativeVelocity) * coll.rigidbody.mass;
	
			//Debug.Log ("raw force magnitude: " + forceMagnitude);
			forceMagnitude = Mathf.Abs (forceMagnitude);
			if (forceMagnitude > maxForce) {
				forceMagnitude = maxForce;
			}
			force_vib = (int)Mathf.Floor ((forceCoeff * forceMagnitude) + 100);
			//debugString = gameObject.name + " in contact with " + coll.gameObject.name + " with force: " + forceMagnitude;
			//Debug.Log (debugString);
			//Debug.Log("force vibrating motor: " + force_vib);
			motorOn [1] = (byte)force_vib;
			//sp.Write (motorOn, 0, 2);
			//Debug.Log("motor pin: " + motorOn[0] + " magnitude: " + motorOn[1]);
		}
		*/
	}
	
	void OnApplicationQuit()
	{
		SendMotorData.magnitudesToSend [motorPin] = (byte)0;
		motorOn [1] = (byte)0;
		//sp.Write (motorOn, 0, 2);
	}

	void OnDestroy()
	{
		SendMotorData.magnitudesToSend [motorPin] = (byte)0;
		motorOn [1] = (byte)0;
		//sp.Write (motorOn, 0, 2);
	}
}
