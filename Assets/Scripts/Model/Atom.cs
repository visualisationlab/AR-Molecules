using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AtomData
{
	public string atomName;
	public float radius;
	public float cov_radius;

	//constructor to generate a data model for an atom.
	public AtomData(string atomName, float radius, float cov_radius)
	{
		this.atomName = atomName;
		this.radius = radius;
		this.cov_radius = cov_radius;
	}

	// returns a general description of the atom model.
	public string Describe()
	{
		return this.atomName + " radius : " + this.cov_radius;
	}
}

public class Atom : MonoBehaviour {
	
	public AtomData data;
	public GameObject rotationAxis;

	public Molecule parentMolecule;

	// keep a reference to the bonds, atoms an rotation axes for this atom.
	public List<GameObject> connectedBonds = new List<GameObject>();
	public List<GameObject> bondedAtoms = new List<GameObject>();
	public List<GameObject> rotationAxes = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
		// if the atomName is not defined (after copying the atom from another molecule), do not set the color.
		if (data.atomName != null) {

			// set the atoms color
			GetComponent<MeshRenderer>().material.color = AtomColors.getColorForAtom(data.atomName);
			parentMolecule = gameObject.GetComponentInParent<Molecule> ();
			
		}
	}

	// return a description of the atoms that this atom is bonded with.
	private string bondedAtomsDescription () {
		string text = "";
		foreach (GameObject atom in bondedAtoms) {
			text = text + atom.GetComponent<Atom> ().data.atomName + ", ";
		}
		return text;
	}

	public string describe() {
		return (data.atomName + " , bonded with: " + bondedAtomsDescription());
	}

	/**
	 * Adds the rotation axes to this atom.
	 * 
	 * TODO there are more possible rotation axes.
	 */
	public void addRotationAxes(){
		if (parentMolecule.allowsSelection) {

			parentMolecule.destroyRotationAxes ();

			foreach (GameObject atom in bondedAtoms) {
				//create bond:
				GameObject axis = Instantiate(rotationAxis);
				
				axis.GetComponent<MeshRenderer>().material.color = Color.blue;
				
				
				//set the corrrect scale:
				float yscale = (gameObject.transform.localPosition - atom.transform.localPosition).magnitude / 2.0f;
				Vector3 scale = axis.transform.localScale;
				scale.y = yscale * 10;
				scale.x = 0.05f;
				scale.z = 0.05f;
				axis.transform.localScale = scale;
				
				axis.transform.position = transform.position;
				axis.transform.parent = transform.parent;
				// set the correct rotation.
				axis.transform.rotation = Quaternion.FromToRotation(Vector3.up, gameObject.transform.position - atom.transform.position);
				
				parentMolecule.rotationAxes.Add (axis);
			}
		}
	}

	public void DestroyRotationAxes(){
		foreach (GameObject axis in rotationAxes) {
			Destroy (axis);
		}
	}

	// Render this atom in a different state than the original atom so it is clear that this is a 'special' (usually a copy) atom.
	public void setTransparent() {
		gameObject.AddComponent<WireFrame> ();
	}
}
