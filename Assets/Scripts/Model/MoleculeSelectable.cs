using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


/*
 * This behaviour allows the selection of atoms and bonds
 * 
 * Upon selection, the connected Text element will be updated with the selection info
 * 
 */

public class MoleculeSelectable : MonoBehaviour {

	public Text selectionText;

	// reference to the molecule script.
	private Molecule parentMolecule;

	void Start (){
	
		parentMolecule = gameObject.GetComponent<Molecule> ();

		//clear the selection text
//		selectionText.text = "";
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			ShootRay(ray);
		}
	}

	void ShootRay (Ray ray)
	{

		RaycastHit rhit;

		if (Physics.Raycast (ray, out rhit, 1000.0f)) {
			GameObject gObjectHit = rhit.collider.gameObject;
			print (gObjectHit.name);

			//selected an atom
			if (gObjectHit.name == "Atom(Clone)") {
				Atom hitAtom = gObjectHit.GetComponent<Atom> ();
				print (hitAtom.describe());
				appendText (hitAtom.describe());
				if (parentMolecule.allowsSelection) {
					hitAtom.addRotationAxes ();
				}
			}

			// selected a bond
			if (gObjectHit.name == "Bond(Clone)") {
				Bond bondModel = gObjectHit.GetComponent<Bond> ();
				if (parentMolecule.allowsSelection) {
					bondModel.addRotationAxes ();
				}
				appendText (bondModel.bondDescription());
			}

			// selected a bond
			if (gObjectHit.name == "SymmetryPoint(Clone)") {
				Molecule molecule = gObjectHit.GetComponentInParent<Molecule> ();
				molecule.projectThroughPoint (gObjectHit.transform.localPosition);
			}

			// selected a bond
			if (gObjectHit.name == "RotationAxis(Clone)") {
				appendText ("Rotation Axis");
				Molecule parentMolecule = gameObject.GetComponentInParent <Molecule> ();
				parentMolecule.selectRotationAxis (gObjectHit);
			}
		}
	}

	void appendText(string text){
//		selectionText.text = "Selected: " + text;
	}

}
