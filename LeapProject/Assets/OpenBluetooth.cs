using UnityEngine;
using System.Collections;

public class OpenBluetooth : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PrintCollisions.OpenConnection ();
	}

	void OnApplicationQuit()
	{

		PrintCollisions.CloseConnection ();
	}

	void OnDestroy()
	{
		PrintCollisions.CloseConnection ();
	}
}
