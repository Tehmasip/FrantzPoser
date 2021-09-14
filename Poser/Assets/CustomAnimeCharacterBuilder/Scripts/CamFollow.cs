using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamFollow : MonoBehaviour {
	public Transform Target;
	public Transform cam;
	private Vector2 currentRotation;
	public float sensitivity = 10f;
	public float maxYAngle = 80f;
	float distance=-1.2f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.mouseScrollDelta.y != 0) {
			distance += 0.1f*Input.mouseScrollDelta.y;
			distance = Mathf.Clamp (distance, -1.8f,-0.4f);
		}
		cam.localPosition=new Vector3(0,0.2f,Mathf.MoveTowards(cam.localPosition.z,distance,Time.deltaTime));
		transform.position = Target.transform.transform.position;
		cam.transform.LookAt (Target);

		currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
		currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
		currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
		currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
		transform.rotation = Quaternion.Euler(currentRotation.y,currentRotation.x,0);
		if (Input.GetMouseButtonDown(0))
			Cursor.lockState = CursorLockMode.Locked;
	}
}
