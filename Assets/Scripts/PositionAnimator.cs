/**
* 
* PositionAnimator.
* 
* Component that can be attached to a transform
* to animate a transition to an endpoint.
* 
* Must provide a start and endpoint in the same coordinate system.
* 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAnimator : MonoBehaviour {
	
	public Vector3 start;
	public Vector3 end;

	public float speed = 3.0f;

	// a private vector to hold the steps that are already taken.
	private Vector3 currentSteps;

	private Vector3 endPoint;
	private Vector3 startPoint;

	void Start (){
		transform.localPosition = start;
	}

	// Update is called once per frame
	void Update () {
		transform.localPosition = Vector3.Lerp(start, end,
			Mathf.SmoothStep(0f,1f,
				Mathf.PingPong(Time.time/ speed, 1f)
			) );
	}

	void OnDestroy() {
		transform.localPosition = start;
	}
}