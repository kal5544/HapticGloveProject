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

	public bool ModifyMesh(Collision coll)
	{
		if (meshEditMode) 
		{
			Vector3 collisionPoint = coll.contacts[0].otherCollider.transform.InverseTransformPoint(coll.contacts[0].point);

			meshFilter = coll.gameObject.GetComponent<MeshFilter> ();
			mesh = meshFilter.mesh;

			float minDistanceSqr = Mathf.Infinity;
			Vector3 closestVertex = Vector3.zero;
			int closestVertexNum = 0;
			Vector3[] vertices = mesh.vertices;

			Debug.Log("collision point " + collisionPoint);
			// scan all vertices to find nearest
			for (int i = 0; i < vertices.Length; i++) 
			{

				Vector3 diff = collisionPoint - vertices [i];
				float distSqr = diff.sqrMagnitude;
			
				if (distSqr < minDistanceSqr) 
				{
					minDistanceSqr = distSqr;
					closestVertex = vertices [i];
					closestVertexNum = i;
				}
			}

			closestVertex -= transform.InverseTransformDirection(coll.contacts [0].normal) * 0.01f;

			vertices [closestVertexNum] = closestVertex;
			mesh.vertices = vertices;
			meshFilter.gameObject.GetComponent<MeshCollider> ().sharedMesh = null;
			meshFilter.gameObject.GetComponent<MeshCollider> ().sharedMesh = mesh;
			coll.transform.eulerAngles = Vector3.zero;
			coll.transform.position = new Vector3(0,3.3f,0);
		}
		return meshEditMode;
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
