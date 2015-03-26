using UnityEngine;
using System.Collections;

public class ObjectCreator : MonoBehaviour {

	public GameObject lastCreated;
	public Shader outlineShader;
	public GameObject createdObjectRoot;

	private Shader previousShader;
	private Object newObj;
	public void CreateObject(string prefab)
	{
		if (lastCreated != null) {
			//lastCreated.GetComponent<Renderer>().material.shader = previousShader;
			newObj = Resources.Load (prefab);
			lastCreated = (GameObject)Instantiate(newObj, new Vector3(0,9,0), Quaternion.identity);
		//	previousShader = lastCreated.GetComponent<Renderer>().material.shader;
		//	lastCreated.GetComponent<Renderer>().material.shader = outlineShader;
		} else {
			newObj = Resources.Load (prefab);
			lastCreated = (GameObject)Instantiate (newObj, new Vector3 (0, 9, 0), Quaternion.identity);
		//	previousShader = lastCreated.GetComponent<Renderer>().material.shader;
		//	lastCreated.GetComponent<Renderer>().material.shader = outlineShader;
		}

		lastCreated.transform.SetParent (createdObjectRoot.transform);
	}
}
