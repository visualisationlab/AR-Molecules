using System;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Molecule : MonoBehaviour {

	public float bondWidth = 0.05f;
	public float verticalOffset = 5.0f;

	// references to the prefabs
	public GameObject atom;
	public GameObject bond;
	public GameObject symmetryPoint;

	//reference to a copy of the gameObject that can be used to visualize transformations.
	public GameObject moleculeCopy;

	// reference to a copy of the molecule behaviour.
	public Molecule moleculeCopyBehaviour;

	// public properties for the molecule
	public float atomScale = 0.1f;
	public string fileName = "tnt";

	// List with references to the atoms, bonds and symmetrypoints.
	public List<GameObject> atoms = new List<GameObject>();
	public List<GameObject> bonds = new List<GameObject>();
	public List<GameObject> symmetryPoints = new List<GameObject>();
	public List<GameObject> rotationAxes = new List<GameObject>();

	public bool allowsSelection = true;

	// reference for the selected rotation axis
	public GameObject selectedRotationAxis;

	AtomModel model;

	public bool showAtoms = true;

	private float bondErrorMargin = 0.1f;

	// Use this for initialization
	void Start () {
		if (fileName != null) {
			renderMolecule ();
		}
	}

	public void deleteMolecule(){
		//clear any previous data
		atoms.Clear ();
		bonds.Clear ();
		symmetryPoints.Clear ();
		rotationAxes.Clear ();

		//destroy all children.
		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}
	}


	public void renderMolecule(){

		deleteMolecule ();

		model = new AtomModel ();

		readInput (fileName);
		addBonds ();

		// reset the instruction text
		InstructionTextManager.instance.setText ("");

		//we want to scale the object and set it's position just above the marker
		gameObject.transform.localScale = gameObject.transform.localScale * atomScale;

		Vector3 objectPos = new Vector3 (0.0f, verticalOffset * atomScale, 0.0f);
		gameObject.transform.localPosition = objectPos; //gameObject.transform.position + offset;
	
		//reset the copy
		//copyMolecule ();


		// disable it for now.
		setCopyEnabled (false);
	}
		
	void readInput(string fileName)
	{
		string path = "xyz/GeneratedAssets/" + fileName;
		TextAsset file = Resources.Load (path) as TextAsset;

		int index = 0;
		foreach (string line in file.toList()){
			index++;

			//the molecule name
			if (index == 1) {
				print (line);
			}

			//the comment
			if (index == 2){
				print (line);
			}

			//this should be an atom
			if (index > 2) {
				addAtom (line);
			}
		}
	}

	void addAtom(string atomString){
		
		string[] atomDecscription = atomString.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

		//atom should have a name and 3 dimensions.
		if (atomDecscription.Length != 4)
		{
			return;
		}

		//The first entry of a line is the name of the atom, followed by the x, y and z coordinates.
		string atomName = atomDecscription [0];
		float x = float.Parse (atomDecscription [1].ToString ());
		float y = float.Parse (atomDecscription [2].ToString ());
		float z = float.Parse (atomDecscription [3].ToString ());

		//get the atom data.
		AtomData atomData = model.data[atomName];

		Vector3 localPos = new Vector3 (x, y, z);

		GameObject atomInstance = Instantiate(atom, new Vector3 (x, y, z), Quaternion.identity);
		atoms.Add (atomInstance);

		float scale = atomData.radius;
		Vector3 localScale = new Vector3 (scale, scale, scale);

		//get a reference to the script
		Atom atomScript = atomInstance.GetComponent<Atom>();
		atomScript.data = atomData;

		//make it a child of the molecule
		atomInstance.transform.parent = gameObject.transform;

		//ensure the atom is positioned and scaled relative to the parent.
		atomInstance.transform.localPosition = localPos;
		atomInstance.transform.localScale = localScale;
	}


	void addBonds(){

		//calculate the distances between all atoms.
		for (int i = 0; i < atoms.Count; i++) {
			for (int k = i + 1; k < atoms.Count; k++) {

				// references to the transform
				Transform first = transform.GetChild (i);
				Transform second = transform.GetChild (k);
			
				//references to the script component
				Atom firstAtom = first.GetComponent<Atom> ();
				Atom secondAtom = second.GetComponent<Atom> ();

				//create the center points between each of the molecules.
				createCenterPoint (first, second);

				// calculate the distance between the atoms and the sum of their covalent radius.
				float distance = Vector3.Distance(first.position, second.position);
				float summedCovRadius = firstAtom.data.cov_radius + secondAtom.data.cov_radius;

				//bond should be created if the sum of their covalent radius is larger than their distance.
				if (summedCovRadius + bondErrorMargin >= distance) {
					
					GameObject bondInstance = createBond(first, second, distance);

					//add a reference for this bond to both atoms.
					firstAtom.connectedBonds.Add (bondInstance);
					secondAtom.connectedBonds.Add (bondInstance);

					// add a reference for the atoms.
					firstAtom.bondedAtoms.Add(secondAtom.gameObject);
					secondAtom.bondedAtoms.Add (firstAtom.gameObject);
				}
			}
		} 
	}

	GameObject createBond(Transform atom1, Transform atom2, float bondLength){

		// Get the location for the bond: 
		Vector3 bondPosition = (atom2.position - atom1.position) / 2.0f + atom1.position;
		
		//create bond:
		GameObject bondInstance = Instantiate(bond, bondPosition, Quaternion.identity);

		Bond bondScript = bondInstance.GetComponent<Bond>();
		bondScript.atom1 = atom1;
		bondScript.atom2 = atom2;

		//set the corrrect scale:
		float yscale = (atom1.position - atom2.position).magnitude / 2.0f;
		Vector3 scale = bondInstance.transform.localScale;
		scale.y = yscale;
		scale.x = 0.15f;
		scale.z = 0.15f;
		bondInstance.transform.localScale = scale;

		// set the correct rotation.
		bondInstance.transform.rotation = Quaternion.FromToRotation(Vector3.up, atom1.position - atom2.position);

		// make it a child of the molecule
		bondInstance.transform.parent = gameObject.transform;
		
		//add the reference to the bond to this molecule
		bonds.Add(bondInstance);


		return bondInstance;
	}

	void copyMolecule(){

		if (allowsSelection) {
			// create a copy if we haven't done so yet.
			if (moleculeCopy == null) {
				print ("creating copy");
				moleculeCopy = Instantiate (gameObject);
			}

			// Stop the animation if the previous instance was animating.
			moleculeCopy.GetComponent<Molecule> ().stopAnimation ();

			//enable the molecule
			setCopyEnabled (true);

			// set the right transform properties.
			moleculeCopy.transform.parent = transform.parent;
			moleculeCopy.transform.position = gameObject.transform.position;
			moleculeCopy.transform.rotation = gameObject.transform.rotation;
			moleculeCopy.transform.localRotation = gameObject.transform.localRotation;
			moleculeCopy.transform.localScale = gameObject.transform.localScale;

			// get a reference to the molecule behaviour script
			moleculeCopyBehaviour = moleculeCopy.GetComponent<Molecule> ();

			// do not show rotation axes on the copied molecule.
			moleculeCopyBehaviour.destroyRotationAxes();

			// do not show symmetry points on the copied molecule.
			moleculeCopyBehaviour.setSymmetryState(false);

			// do not allow generation of new axes on the copied molecule.
			moleculeCopyBehaviour.allowsSelection = false;

			//do not try to create the molecule again
			moleculeCopyBehaviour.fileName = null;

			// TODO is transparency better?
			// render the copied molecule in wireframe mode.
			foreach(GameObject child in moleculeCopyBehaviour.atoms){
				child.AddComponent<WireFrame> ();
			}

			foreach(GameObject child in moleculeCopyBehaviour.bonds){
				child.AddComponent<WireFrame> ();
			}
		}
	}

	public void setCopyEnabled(bool enabled){
		if (moleculeCopy != null) {
			moleculeCopy.SetActive (enabled);
		}
	}


	public void projectThroughPoint (Vector3 pointLocation) {

		// Make sure there is an up to date copy of the molecule.
		copyMolecule ();

		//append the animators
		foreach(GameObject atomInstance in moleculeCopyBehaviour.atoms){
			//destroy previous component.
			DestroyImmediate(atomInstance.GetComponent<PositionAnimator>());

			//append new animator component.
			PositionAnimator animator = atomInstance.AddComponent<PositionAnimator>();
			animator.start = atomInstance.transform.localPosition;
			animator.end = (pointLocation * 2) - atomInstance.transform.localPosition;
		}

		foreach(GameObject bondInstance in moleculeCopyBehaviour.bonds){
			DestroyImmediate(bondInstance.GetComponent<PositionAnimator>());

			PositionAnimator animator = bondInstance.AddComponent<PositionAnimator>();
			animator.start = bondInstance.transform.localPosition;
			animator.end = (pointLocation * 2) - bondInstance.transform.localPosition;
		}
	}


	Vector3 getCenter(Transform first, Transform second){
		//center between two vectors = (A+B)/2
		return (first.position + second.position) * 0.5f;
	}

	// Creates a symmetry point between two transforms.
	void createCenterPoint(Transform first, Transform second) {
		GameObject point = Instantiate(symmetryPoint, Vector3.zero, Quaternion.identity);

		//deactivate it for now
		point.SetActive(false);

		//add it to our list of symmetry points
		symmetryPoints.Add (point);

		//set the correct scale and location in local space.
		point.transform.localScale = point.transform.localScale * 0.3f;
		point.transform.position = getCenter(first, second);
		point.transform.parent = gameObject.transform;
	}

	// enable or disable the symmetry points.
	private void setSymmetryState(bool state){
		foreach (GameObject point in symmetryPoints) {
			point.SetActive(state);
			stopAnimation ();
		}
	}

	// This function removes the rotation axes and enables the symmetry points.
	public void selectSymmetryMode(){

		//disable the current copy.
		setCopyEnabled (false);

		// destroy rotation axes if they are present (also destroys the copied molecule instance).
		destroyRotationAxes ();
		setSymmetryState (true);

	}

	// This function removes the symmetry points and allows selection of rotation axes.
	public void selectRotationMode(){
		setSymmetryState (false);
		destroyRotationAxes ();
	}


	// Destroy animation on each bond and atom in this molecule.
	void stopAnimation(){
		
		foreach (GameObject atom in atoms) {
			Destroy (atom.GetComponent<PositionAnimator> ());
		}

		foreach (GameObject bond in bonds) {
			Destroy (bond.GetComponent<PositionAnimator> ());
		}
	}

	// Removes the rotation axes from all atoms.
	public void destroyRotationAxes(){

		// disable the molecule copy if there is one.
//		setCopyEnabled(false);

		// TODO reset it.
		Destroy(moleculeCopy);


		//destory all axis gameObjects
		foreach (GameObject axis in rotationAxes) {
			Destroy (axis);
		}

		// reset the list
		rotationAxes.Clear ();
	}

	// Creates a copy of the molecule that can be rotated around the selected axis.
	public void selectRotationAxis(GameObject axis){
		
		setCopyEnabled(false);

		selectedRotationAxis = axis;
		selectedRotationAxis.GetComponent<MeshRenderer> ().material.color = Color.green;

		//destroy all other axis, because we are focussing on the selected one.
		foreach (GameObject otherAxis in rotationAxes) {
			if (otherAxis != selectedRotationAxis) {
				Destroy (otherAxis);
			}
		}

		// make sure the copied molecule is up to date with the current state.
		copyMolecule ();

		//destroy previous rotation behaviour.
		Destroy (moleculeCopy.GetComponent<AxisRotationBehaviour> ());

		//append rotate component
		AxisRotationBehaviour rotator = moleculeCopy.AddComponent<AxisRotationBehaviour>();
		rotator.rotationAxis = axis;
	}

	/**
	 * Resets the molecule to the starting state.
	 */
	public void resetState(){
		destroyRotationAxes ();
		setSymmetryState (false);
		Destroy (moleculeCopy);
	}

}
