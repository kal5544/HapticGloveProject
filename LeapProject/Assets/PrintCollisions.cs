using UnityEngine;
using System.Collections;

public class PrintCollisions : MonoBehaviour {

	private string debugString = "";

	void OnCollisionEnter(Collision coll)
	{
		Debug.Log ("modifying mesh");
		GameObject.Find ("HandControllerSandBox").GetComponent<MeshFunctionality> ().ModifyMesh (coll);
	}

	void OnCollisionStay(Collision coll)
	{
		debugString = gameObject.name + " in contact with " + coll.gameObject.name + " with force: " + Vector3.Dot (coll.contacts[0].normal, coll.relativeVelocity)*coll.rigidbody.mass;
		//Debug.Log (debugString);
	}

}
