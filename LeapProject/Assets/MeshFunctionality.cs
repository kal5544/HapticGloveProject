using UnityEngine;
using System.Collections;

public class MeshFunctionality : MonoBehaviour {

	public ObjectCreator objectCreatorRef;

	private MeshFilter meshFilter;
	private Mesh mesh;
	private Color[] vertexColors;
	private Color previousVertexColor;
	private Color highlightVertexColor = Color.red;

	void Update()
	{
		if (objectCreatorRef.lastCreated != null && meshFilter != objectCreatorRef.lastCreated.GetComponent<MeshFilter> ()) 
		{
			meshFilter = objectCreatorRef.lastCreated.GetComponent<MeshFilter> ();
			mesh = meshFilter.mesh;
			vertexColors = mesh.colors;
			Debug.Log("vertex color before change: " + vertexColors[0]);
			for(int i = 0; i < (int)(vertexColors.Length / 2); ++i)
			{
				vertexColors[i] = highlightVertexColor;
			}
			mesh.colors = vertexColors;
			Debug.Log("vertex color after change: " + vertexColors[0]);
		}

/*
		if (highlightedObject == null && objectCreatorRef.lastCreated != null) 
		{
			highlightedObject = objectCreatorRef.lastCreated;
			highlightedObjectMesh = highlightedObject.GetComponent<MeshFilter>().mesh;
			previousVertexColor = highlightedObjectMesh.colors[0];
			vertexColors = highlightedObjectMesh.colors;

			for(int i = 0; i < vertexColors.Length; i++)
			{
				vertexColors[i] = highlightVertexColor;
			}

			highlightedObjectMesh.colors = vertexColors;
		}

		if (objectCreatorRef.lastCreated != highlightedObject) 
		{
			for(int i = 0; i < vertexColors.Length; i++) 
			{
				vertexColors [i] = previousVertexColor;
			}

			highlightedObjectMesh.colors = vertexColors;
			highlightedObject.GetComponent<MeshFilter>().mesh = highlightedObjectMesh;


			highlightedObject = objectCreatorRef.lastCreated;
			highlightedObjectMesh = highlightedObject.GetComponent<MeshFilter>().mesh;
			previousVertexColor = highlightedObjectMesh.colors[0];
			vertexColors = highlightedObjectMesh.colors;
			for(int i = 0; i < vertexColors.Length; i++)
			{
				vertexColors [i] = highlightVertexColor;
			}
			highlightedObjectMesh.colors = vertexColors;
			highlightedObject.GetComponent<MeshFilter>().mesh = highlightedObjectMesh;
			Debug.Log("changed vertex colors");
		}
		*/
	}
}
