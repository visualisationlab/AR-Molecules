//using UnityEngine;
//using System.Collections;
//
///*
// * Script to add rotation to an object
// * 
// * When a horizontal touch input is registered, the parent object of this script will rotate
// * 
// */
//
//public class MoleculeRotatable : MonoBehaviour {
//
//	float f_lastX = 0.0f;
//	float f_difX = 0.0f;
//	float f_steps = 0.0f;
//	int i_direction = 1;
//
//	public GameObject centerObject;
//
//	// Update is called once per frame
//	void Update () 
//	{
//
//		centerObject = gameObject.GetComponent<Molecule>().selectedObject;
//
//		//Make sure there is an object to rotate around.
//		if (centerObject != null) {
//		
//			if (Input.GetMouseButtonDown(0))
//			{
//				f_difX = 0.0f;
//			}
//			else if (Input.GetMouseButton(0))
//			{
//				f_difX = Mathf.Abs(f_lastX - Input.GetAxis ("Mouse X"));
//				
//				if (f_lastX < Input.GetAxis ("Mouse X"))
//				{
//					i_direction = -1;
//					transform.Rotate(Vector3.up, -f_difX);
//				}
//				
//				if (f_lastX > Input.GetAxis ("Mouse X"))
//				{
//					i_direction = 1;
//					transform.Rotate(Vector3.up, f_difX);
//				}
//				
//				f_lastX = -Input.GetAxis ("Mouse X");
//			}
//			else 
//			{
//				if (f_difX != 0.0f)
//				{
//					if (f_difX > 0.5f) f_difX -= 0.05f;
//					if (f_difX < 0.5f) f_difX += 0.05f;
//				}
//				
//				transform.Rotate(Vector3.up, f_difX * i_direction);
//			}
//		
//		}
//	}
//}