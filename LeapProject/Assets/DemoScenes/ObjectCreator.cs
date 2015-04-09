using UnityEngine;
using System.Collections;

public class ObjectCreator : MonoBehaviour {

	public GameObject lastCreated;
	public GameObject createdObjectRoot;
	
	private Object newObj;
	public void CreateObject(string prefab)
	{

			newObj = Resources.Load (prefab);
			lastCreated = (GameObject)Instantiate(newObj, new Vector3(0,9,0), Quaternion.identity);

		lastCreated.transform.SetParent (createdObjectRoot.transform);
	}
}
