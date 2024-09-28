using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisRotationBehaviour : MonoBehaviour {

	float f_lastX = 0.0f;
	float f_difX = 0.0f;
//	float f_steps = 0.0f;
	int i_direction = 1;

	float rotationAngle = 0.0f;

	float rotationSpeed = 0.75f;

	public GameObject rotationAxis;

	// Update is called once per frame
	void Update () 	{
		if (rotationAxis != null) {
			if (Input.GetMouseButtonDown (0)) {
				f_difX = 0.0f;
			} else if (Input.GetMouseButton (0)) {
				f_difX = Mathf.Abs (f_lastX - Input.GetAxis ("Mouse X")) * rotationSpeed;
				rotationAngle += f_lastX;
				
				if (f_lastX < Input.GetAxis ("Mouse X")) {
					i_direction = -1;
					gameObject.transform.RotateAround (rotationAxis.transform.position, rotationAxis.transform.up, -f_difX);
				}
				
				if (f_lastX > Input.GetAxis ("Mouse X")) {
					i_direction = 1;
					gameObject.transform.RotateAround (rotationAxis.transform.position, rotationAxis.transform.up, f_difX);
				}
				
				f_lastX = -Input.GetAxis ("Mouse X");
			} else {
				if (f_difX != 0.0f) {
					if (f_difX > 0.5f)
						f_difX -= 0.05f;
					if (f_difX < 0.5f)
						f_difX += 0.05f;
				}
				gameObject.transform.RotateAround (rotationAxis.transform.position, rotationAxis.transform.up, i_direction * f_difX);
			}
		}
		float angle = Vector3.Angle (rotationAxis.transform.forward, gameObject.transform.forward);

//		string extraText = "";

//		int extraText = (int)(360.0f / angle);

		InstructionTextManager.instance.setText ("Rotation: " + (int)angle + " degrees");// + "\nC" + extraText); 
	}

	void OnDestroy() {
		gameObject.transform.rotation = Quaternion.identity;
	}

	Vector3 rotatePointAroundAxis(Vector3 point, float angle, Vector3 axis)
	{
		Quaternion q = Quaternion.AngleAxis(angle, axis);
		return q * point; //Note: q must be first (point * q wouldn't compile)
	}
}


