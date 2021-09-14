using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionIgnore : MonoBehaviour {
	public CharacterController PlayerController;
	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision (GetComponent<Collider> (), PlayerController);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
