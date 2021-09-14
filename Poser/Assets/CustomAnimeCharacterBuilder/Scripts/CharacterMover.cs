using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour

{
	public float speed = 3.0F;
	public float rotateSpeed = 3.0F;
	public Transform Cam;
	public Animator MyController;
	float CurSpeed;
	void FixedUpdate()
	{
		CharacterController controller = GetComponent<CharacterController>();
		CurSpeed = 1;
		Quaternion RequiredRot;
		if (Input.GetKey (KeyCode.W)) {
			if (Input.GetKey (KeyCode.A)) {
				RequiredRot = Quaternion.Euler (new Vector3 (0, Cam.transform.eulerAngles.y-45));
			}
			else if (Input.GetKey (KeyCode.D)) {
				RequiredRot = Quaternion.Euler (new Vector3 (0, Cam.transform.eulerAngles.y+45));
			}
			else
				RequiredRot = Quaternion.Euler (new Vector3 (0, Cam.transform.eulerAngles.y));
			RequiredRot.x = 0;
			RequiredRot.z = 0;
			MyController.SetInteger ("Walk", 1);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, RequiredRot, Time.fixedDeltaTime * 200*rotateSpeed);
		}else if (Input.GetKey (KeyCode.S)) {
			if (Input.GetKey (KeyCode.A)) {
				RequiredRot = Quaternion.Euler (new Vector3 (0, Cam.transform.eulerAngles.y+45-180));
			}
			else if (Input.GetKey (KeyCode.D)) {
				RequiredRot = Quaternion.Euler (new Vector3 (0, Cam.transform.eulerAngles.y-45-180));
			}
			else
				RequiredRot = Quaternion.Euler (new Vector3 (0, Cam.transform.eulerAngles.y - 180));
			RequiredRot.x = 0;
			RequiredRot.z = 0;
			MyController.SetInteger ("Walk", 1);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, RequiredRot, Time.fixedDeltaTime * 200*rotateSpeed);
		}
		else if (Input.GetKey (KeyCode.A)) {
			RequiredRot = Quaternion.Euler (new Vector3 (0, Cam.transform.eulerAngles.y - 90));
			RequiredRot.x = 0;
			RequiredRot.z = 0;
			MyController.SetInteger ("Walk", 1);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, RequiredRot, Time.fixedDeltaTime * 200*rotateSpeed);
		}  else if (Input.GetKey (KeyCode.D)) {
			RequiredRot = Quaternion.Euler (new Vector3 (0, Cam.transform.eulerAngles.y + 90));
			RequiredRot.x = 0;
			RequiredRot.z = 0;
			MyController.SetInteger ("Walk", 1);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, RequiredRot, Time.fixedDeltaTime * 200*rotateSpeed);
		}

		else {
			CurSpeed = 0;
			MyController.SetInteger ("Walk", 0);
		}
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		controller.SimpleMove(forward * CurSpeed*speed);
	}
}