using UnityEngine;
using System.Collections;

public class GestureRecognizer : MonoBehaviour {
	Leap.Controller controller = new Leap.Controller();
	//Leap.Frame frame = new Leap.Frame ();

	// Use this for initialization
	void Start () {
		controller.EnableGesture (Leap.Gesture.GestureType.TYPECIRCLE);
		controller.Config.SetFloat("Gesture.Circle.MinArc", (float)2.0);
		controller.Config.SetFloat("Gesture.Circle.MinRadius", (float)50.0);
		controller.EnableGesture (Leap.Gesture.GestureType.TYPESWIPE);
		controller.Config.SetFloat ("Gesture.Swipe.MinLength", (float)300.0);
		controller.Config.SetFloat ("Gesture.Swipe.MinVelocity", (float)700.0);
		controller.EnableGesture (Leap.Gesture.GestureType.TYPEKEYTAP);
		controller.Config.SetFloat ("Gesture.KeyTap.MinDownVelocity", (float)40.0);
		controller.Config.SetFloat ("Gesture.KeyTap.HistorySeconds", (float)0.3);
		controller.Config.SetFloat ("Gesture.KeyTap.MinDistance", (float)15.0);
		controller.EnableGesture (Leap.Gesture.GestureType.TYPESCREENTAP);
		controller.Config.SetFloat ("Gesture.ScreenTap.MinForwardVelocity", (float)35.0);
		controller.Config.SetFloat ("Gesture.ScreenTap.HistorySeconds", (float)0.3);
		controller.Config.SetFloat ("Gesture.ScreenTap.MinDistance", (float)15.0);
		controller.Config.Save ();
	}
	
	// Update is called once per frame
	void Update () {
	
		Leap.Frame frame = controller.Frame();
		Leap.GestureList gestures = frame.Gestures();
		for (int i = 0; i < gestures.Count; i++)
		{
			Leap.Gesture gesture = gestures[0];
			switch(gesture.Type){
			case Leap.Gesture.GestureType.TYPECIRCLE:
				if(gesture.State == Leap.Gesture.GestureState.STATESTOP){
					Debug.Log("Circle");
				}
				break;
			case Leap.Gesture.GestureType.TYPESWIPE:
				if(gesture.State == Leap.Gesture.GestureState.STATESTOP){
					Debug.Log("Swipe");
				}
				break;
			case Leap.Gesture.GestureType.TYPEKEYTAP:
				Debug.Log("KeyTap");
				break;
			case Leap.Gesture.GestureType.TYPESCREENTAP:
				Debug.Log("ScreenTap");
				break;
			default:
				Debug.Log("Bad gesture type");
				break;
			}
		}
	}
}
