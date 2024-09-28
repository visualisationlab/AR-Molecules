using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleObject : MonoBehaviour {

	public Transform A;
	public Transform B;


	// Use this for initialization
	void Start () {
		Vector3 midPoint = (A.position + B.position) * 0.5f;

		Vector3 secondMidPoint = (B.position + A.position) * 0.5f;


		GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sphere.transform.position = midPoint;


		GameObject sphere2 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sphere2.transform.position = secondMidPoint;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
