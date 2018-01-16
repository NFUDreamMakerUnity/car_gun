using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarwheelScript : MonoBehaviour {

	Rigidbody rb;
	HingeJoint hj;
	[SerializeField] float wheelRot;
	

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		hj = GetComponent<HingeJoint>();
	}
	
	// Update is called once per frame
	void Update () {
		wheelRot = hj.angle/90;
	}
}
