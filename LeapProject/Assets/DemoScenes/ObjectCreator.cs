using UnityEngine;
using System.Collections;

public class ObjectCreator : MonoBehaviour {

	public GameObject lastCreated;
	public GameObject createdObjectRoot;
	
	private Object newObj;

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Q)) {
			CreateObject ("Cube");
		} else if (Input.GetKeyDown (KeyCode.W)) {
			CreateObject ("Sphere");
		} else if (Input.GetKeyDown (KeyCode.E)) {
			CreateObject("Cylinder");
		}
	}

	public void CreateObject(string prefab)
	{

		newObj = Resources.Load (prefab);
		lastCreated = (GameObject)Instantiate(newObj, new Vector3(0,0,.5f), Quaternion.identity);

		lastCreated.transform.SetParent (createdObjectRoot.transform);
	}
}
