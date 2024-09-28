using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InversionCenterBehaviour : MonoBehaviour {

	public MoleculeBehaviour molecule;


	void Start(){
		molecule = gameObject.GetComponent<MoleculeBehaviour> ();
	}

	public void projectThroughPoint (Vector3 pointLocation) {

		//append the animators
		foreach(GameObject atomInstance in molecule.atoms){
			//destroy previous component.
			DestroyImmediate(atomInstance.GetComponent<PositionAnimator>());

			//append new animator component.
			PositionAnimator animator = atomInstance.AddComponent<PositionAnimator>();
			animator.start = atomInstance.transform.localPosition;
			animator.end = (pointLocation * 2) - atomInstance.transform.localPosition;
		}

		foreach(GameObject bondInstance in molecule.bonds){
			DestroyImmediate(bondInstance.GetComponent<PositionAnimator>());

			PositionAnimator animator = bondInstance.AddComponent<PositionAnimator>();
			animator.start = bondInstance.transform.localPosition;
			animator.end = (pointLocation * 2) - bondInstance.transform.localPosition;
		}
	}

	// Destroy animation on each bond and atom in this molecule.
	void stopAnimation(){

		foreach (GameObject atom in molecule.atoms) {
			Destroy (atom.GetComponent<PositionAnimator> ());
		}

		foreach (GameObject bond in molecule.bonds) {
			Destroy (bond.GetComponent<PositionAnimator> ());
		}
	}
}
