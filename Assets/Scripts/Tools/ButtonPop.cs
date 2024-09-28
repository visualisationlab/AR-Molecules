using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.Collections;

public class ButtonPop : MonoBehaviour, ITrackableEventHandler {
	
	private TrackableBehaviour trackBehaviour;

	public Text infoText;
	public Molecule molecule;

	private bool showInfo = false;
	private Rect infoFrame = new Rect(50,50,120,30);

	void Start () {
		trackBehaviour = GetComponent<TrackableBehaviour>();
		if (trackBehaviour)
		{
			trackBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED)
		{
			showInfo = true;
		}
		else
		{
			showInfo = false;
		}
	}

	void Update() {
//		if (showInfo) {
//			infoText.text = molecule.fileName;
//		} else {
//			infoText.text = "";
//		}

	}

//	void OnGUI() {
//		if (showInfo) {
//			GUI.TextField (infoFrame, molecule.fileName);
//		}
//	}
}