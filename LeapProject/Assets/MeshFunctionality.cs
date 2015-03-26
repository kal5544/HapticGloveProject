using UnityEngine;
using System.Collections;

public class MeshFunctionality : MonoBehaviour {

	public ObjectCreator objectCreatorRef;
	public bool meshEditMode = false;

	private MeshFilter meshFilter;
	private Mesh mesh;

	public void ToggleMeshEditMode()
	{
		meshEditMode = !meshEditMode;
		for (int i = 0; i < objectCreatorRef.createdObjectRoot.transform.childCount; i++) 
		{
			if(meshEditMode)
			{
				objectCreatorRef.createdObjectRoot.transform.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			}
			else
			{
				objectCreatorRef.createdObjectRoot.transform.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			}
		}
	}

	public void ModifyMesh(Collision coll)
	{
		if (meshEditMode) 
		{
			meshFilter = coll.gameObject.GetComponent<MeshFilter> ();
			mesh = meshFilter.mesh;

			float minDistanceSqr = Mathf.Infinity;
			Vector3 closestVertex = Vector3.zero;
			int closestVertexNum = 0;
			Vector3[] vertices = mesh.vertices;

			// scan all vertices to find nearest
			for (int i =0; i < vertices.Length; i++) 
			{
				Vector3 diff = coll.contacts [0].point - vertices [i];
				float distSqr = diff.sqrMagnitude;
			
				if (distSqr < minDistanceSqr) 
				{
					minDistanceSqr = distSqr;
					closestVertex = vertices [i];
					closestVertexNum = i;
				}
			}

			//Debug.Log ("old vertex point: " + closestVertex);
			//Debug.Log ("collision normal: " + coll.contacts [0].normal);
			closestVertex -= coll.contacts [0].normal * 0.1f;

			//closestVertex = 2 * closestVertex;
			//Debug.Log ("new vertex point: " + closestVertex); 
			vertices [closestVertexNum] = closestVertex;
			mesh.vertices = vertices;
			meshFilter.gameObject.GetComponent<MeshCollider> ().sharedMesh = null;
			meshFilter.gameObject.GetComponent<MeshCollider> ().sharedMesh = mesh;
		}
	}

	public void GrowMesh()
	{
		meshFilter = objectCreatorRef.lastCreated.GetComponent<MeshFilter> ();
		mesh = meshFilter.mesh;
		Vector3[] vertices = mesh.vertices;
		for(int i = 0; i < vertices.Length; i++) {
			vertices[i] += mesh.normals[i];
		}
		mesh.vertices = vertices;
		meshFilter.gameObject.GetComponent<MeshCollider> ().sharedMesh = null;
		meshFilter.gameObject.GetComponent<MeshCollider> ().sharedMesh = mesh;
	}
}
