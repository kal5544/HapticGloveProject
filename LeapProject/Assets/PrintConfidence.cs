using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PrintConfidence : MonoBehaviour {

	public Text confidenceText;

	public void Print(float val)
	{
		confidenceText.text = val.ToString ();
	}
}
