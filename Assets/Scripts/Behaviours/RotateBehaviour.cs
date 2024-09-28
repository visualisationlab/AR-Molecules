using UnityEngine;
using System.Collections;

/*
 * Script to add rotation to an object
 * 
 * When a horizontal touch input is registered, the parent object of this script will rotate
 * 
 */

public class RotateBehaviour : MonoBehaviour {

	float f_lastX = 0.0f;
	float f_difX = 0.0f;
	float f_steps = 0.0f;
	int i_direction = 1;

	private Transform rotateTransform;
	public GameObject rotateBody;

	// Use this for initialization
	void Start ()
	{

//		rotatedTransform.re
		//nothing to do here
	}

	// Update is called once per frame
	void Update () 
	{
		
		if (rotateBody != null) {
			rotateTransform = rotateBody.transform;
		} else {
			rotateTransform = transform;
		}


		if (Input.GetMouseButtonDown (0)) {
			f_difX = 0.0f;
		} else if (Input.GetMouseButton (0)) {
			f_difX = Mathf.Abs (f_lastX - Input.GetAxis ("Mouse X"));

			if (f_lastX < Input.GetAxis ("Mouse X")) {
				i_direction = -1;
				rotateTransform.Rotate (Vector3.up, -f_difX);
			}

			if (f_lastX > Input.GetAxis ("Mouse X")) {
				i_direction = 1;
				rotateTransform.Rotate (Vector3.up, f_difX);
			}

			f_lastX = -Input.GetAxis ("Mouse X");
		} else {
			if (f_difX != 0.0f) {
				if (f_difX > 0.5f)
					f_difX -= 0.05f;
				if (f_difX < 0.5f)
					f_difX += 0.05f;
			}

			rotateTransform.Rotate (Vector3.up, f_difX * i_direction);
		}
	}
}