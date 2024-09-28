using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script that can be placed on a gameObject to log the position every second.
 */

public class LocationTracking : MonoBehaviour {

	private float fireRate = 1.0f;
	private float lastFired = 0.0f;

	// Update is called once per frame
	void Update () {
		if (Time.time > fireRate + lastFired) {
			print (transform.position);
			lastFired = Time.time;
		}
	}
}