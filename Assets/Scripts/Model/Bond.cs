using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bond : MonoBehaviour {

	// reference to the two atoms this bond is bonding
	public Transform atom1;
	public Transform atom2;

	// reference to the rotation axis prefab
	public GameObject rotationAxis;

	public Molecule parentMolecule;

	// keep a reference to rotation axis for this bond.
	public List<GameObject> rotationAxes = new List<GameObject>();

	// Use this for initialization
	void Start () {
		// bonds are rendered in a gray color.
		GetComponent<MeshRenderer>().material.color = Color.gray;

		//get the parent molecule
		parentMolecule = gameObject.GetComponentInParent<Molecule> ();
	}

	// method to get the atom name
	private string getAtomDescription(Transform atom){
		Atom atomModel = atom.GetComponent<Atom> ();
		return atomModel.data.atomName;
	}

	// return a textual description of this bond.
	public string bondDescription() {
		print (atom1);
		print (getAtomDescription (atom1));

		//get atomnames
		string atom1Description = getAtomDescription (atom1);
		string atom2Description = getAtomDescription (atom2);

		string description = "bond between " + atom1Description + " and " + atom2Description; 
		return description;
	}

	/**
	 * Adds the rotation axes to this atom.
	 * 
	 * TODO there are more possible rotation axes.
	 */
	public void addRotationAxes(){

		// destroy other rotation axes first.
		parentMolecule.destroyRotationAxes ();

		// instantiate the prefab.
		GameObject axis = Instantiate(rotationAxis);

		// set the color and scale.
		axis.GetComponent<MeshRenderer>().material.color = Color.blue;
		Vector3 scale = axis.transform.localScale;
		scale.y = 10.0f;
		scale.x = 0.05f;
		scale.z = 0.05f;
		axis.transform.localScale = scale;

		// set the correct orientation and position
		axis.transform.position = transform.position;
		axis.transform.up = gameObject.transform.right;

		// Connect it to the parent (the molecule)
		axis.transform.parent = transform.parent;
		parentMolecule.rotationAxes.Add (axis);
	}

	public void DestroyRotationAxes(){
		foreach (GameObject axis in rotationAxes) {
			Destroy (axis);
		}
	}

}
