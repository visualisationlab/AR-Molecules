using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Vuforia;

/*
 * Class that holds a reference to UI elements
 * 
 * Can be added to a image target, when the target is detected or removed,
 * the UI elements will be respectively shown or hidden.
 * 
 */

public class TargetUIManager : MonoBehaviour, ITrackableEventHandler {

	public GameObject molecule;

	public Canvas toolSetCanvas;

	public Text[] textElements;

	private TrackableBehaviour trackBehaviour;
	private bool showElements;

	void Start () {
		trackBehaviour = GetComponent<TrackableBehaviour>();
		if (trackBehaviour)
		{
			trackBehaviour.RegisterTrackableEventHandler(this);
		}

		toolSetCanvas.gameObject.SetActive (false);
	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED)
		{
			//TODO hack to make sure the copy is displayed and hidden when the molecule is.
			if (molecule != null) {
				molecule.GetComponent<Molecule> ().setCopyEnabled (true);
			}
			showElements = true;
			toolSetCanvas.gameObject.SetActive (true);
		}
		else
		{
			//TODO hack to make sure the copy is displayed and hidden when the molecule is.
			if (molecule != null) {
				molecule.GetComponent<Molecule> ().setCopyEnabled (false);
			}
			showElements = false;
			toolSetCanvas.gameObject.SetActive (false);
		}
	}

	void Update() {
		updateTextElements ();
	}

	void updateTextElements(){
		foreach (Text element in textElements)
		{
			element.enabled = showElements;
		}
	}

}
