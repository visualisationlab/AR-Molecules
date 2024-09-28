using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MegaMirror : MonoBehaviour {
	
	public GameObject originalObject;
	private GameObject reflectedObject;

	public GameObject plane;

	void Start () {
		reflectedObject = Instantiate (originalObject);
	}

	void Update() {
		reflectedObject = Instantiate (originalObject);
		Vector3 normal = plane.transform.position;
		reflectedObject.transform.position = Vector3.Reflect(originalObject.transform.position, normal);
	}

}


