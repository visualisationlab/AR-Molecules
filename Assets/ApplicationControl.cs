using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationControl : MonoBehaviour {

	public Molecule displayedMolecule;

	public GameObject moleculePrefab;
	public GameObject moleculeMarkerBase;

	public void inversionMode(){
		print ("pressed button");
		displayedMolecule.selectSymmetryMode ();
		InstructionTextManager.instance.setText ("Select a point to enable inversion through that point.");
	}

	public void rotationMode(){
	    displayedMolecule.selectRotationMode ();
		InstructionTextManager.instance.setText ("Select a bond or an atom to show the available rotation axes.");	
	}

	public void planeSymmetryMode(){
		InstructionTextManager.instance.setText ("Use the mirror marker to locate symmetry planes.");
	}

	public void resetMoleculeState(){
		InstructionTextManager.instance.setText ("Select an option from the toolset to start.");
		displayedMolecule.resetState();
	}

	public void setMoleculeScale(float scale){

	}

	public void nextMolecule()
	{
//		Instantiate (moleculePrefab, Vector3.zero);

		displayedMolecule.fileName = "ethane";
		displayedMolecule.renderMolecule ();
	}

}
