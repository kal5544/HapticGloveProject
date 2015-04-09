using UnityEngine;
using System.Collections;

public class WanderAI : MonoBehaviour {

	private bool atDestination = true;
	public Vector3 destination = Vector3.zero;
	private NavMeshHit hit;

	void Start()
	{
		SetDestination ();
	}
	// Update is called once per frame
	void Update () {
		atDestination = Vector3.Magnitude (transform.position - GetComponent<NavMeshAgent>().destination) < 15;
		if (atDestination) {
			SetDestination();
		}
	}
	public void SetDestination()
	{
		NavMesh.SamplePosition(75*Random.insideUnitSphere, out hit, 10, 1);
		destination = hit.position;
		GetComponent<NavMeshAgent>().SetDestination(destination);
		GetComponent<NavMeshAgent>().Resume();
		atDestination = false;
	}
}
