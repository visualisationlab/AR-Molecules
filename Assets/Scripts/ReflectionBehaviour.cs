using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionBehaviour : MonoBehaviour {

	public GameObject originalObject;

	GameObject copiedObject;		

	// Use this for initialization
	void Start () {
	
		copiedObject = Instantiate (originalObject);
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 diff = originalObject.transform.position - gameObject.transform.position;
		copiedObject.transform.position = Vector3.Reflect(diff, gameObject.transform.forward);


		Vector3 scale = copiedObject.transform.localScale;
		copiedObject.transform.localScale = new Vector3(scale.x * -1, scale.y, scale.z);		
	}
}
