using UnityEngine;
using System.Collections;

public class MeshFunctionality : MonoBehaviour {

	public ObjectCreator objectCreatorRef;
	public bool meshEditMode = false;

	private MeshFilter meshFilter;
	private Mesh mesh;
	private Vector3[] storedObjPos, storedObjRot;

	public void ToggleMeshEditMode()
	{
		storedObjPos = new Vector3[objectCreatorRef.createdObjectRoot.transform.childCount];
		storedObjRot = new Vector3[objectCreatorRef.createdObjectRoot.transform.childCount];

		meshEditMode = !meshEditMode;
		for (int i = 0; i < objectCreatorRef.createdObjectRoot.transform.childCount; i++) 
		{
			if(meshEditMode)
			{
				objectCreatorRef.createdObjectRoot.transform.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
				storedObjPos[i] = objectCreatorRef.createdObjectRoot.transform.GetChild(i).transform.position;
				storedObjRot[i] = objectCreatorRef.createdObjectRoot.transform.GetChild(i).transform.eulerAngles;
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
			int childIndex = 0;
			for (int i = 0; i < objectCreatorRef.createdObjectRoot.transform.childCount; i++) 
			{
				if(coll.gameObject == objectCreatorRef.createdObjectRoot.transform.GetChild(i))
					childIndex = i;
			}

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

			closestVertex -= transform.InverseTransformDirection(coll.contacts [0].normal) * 0.02f;

			vertices [closestVertexNum] = closestVertex;
			mesh.vertices = vertices;
			meshFilter.gameObject.GetComponent<MeshCollider> ().sharedMesh = null;
			meshFilter.gameObject.GetComponent<MeshCollider> ().sharedMesh = mesh;

			coll.transform.eulerAngles = storedObjRot[childIndex];
			coll.transform.position = storedObjPos[childIndex];
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
