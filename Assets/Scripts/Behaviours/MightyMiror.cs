using UnityEngine;

public class MightyMirror : MonoBehaviour {

	public Transform originalObject;
	public Transform reflectedObject;

	public Plane reflectionPlane;

	void Start () {
		reflectedObject = originalObject;
	}


	void Update () {
		reflectedObject.position = Vector3.Reflect(originalObject.position, Vector3.right);

//		reflectedObject.position = reflectedObject.position * 2;
	}

	public Vector3 ReflectionOverPlane(Vector3 point, Plane plane) {
		Vector3 N = transform.TransformDirection(plane.normal);
		return point - 2 * N * Vector3.Dot(point, N) / Vector3.Dot(N, N);
	}

}