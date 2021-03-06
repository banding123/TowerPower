﻿using UnityEngine;
using System.Collections;

public class ForceDisplayScript : MonoBehaviour {

	// the joint of the tower
	public FixedJoint2D joint;
	// the member of the tower
	public Rigidbody2D rigidbody;
	//Transform before simulating
	private Transform initialTransform;
	// if the tower is simulating
	private bool b_simulating = false;



	// Use this for initialization
	void Start () {
		joint = GetComponent<FixedJoint2D> ();
		rigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (b_simulating) {
			//reads force->ray cast to display it
			Vector3 force = joint.GetReactionForce(0.1f);
			Debug.DrawRay(this.gameObject.transform.position, transform.position - force);
		}
	}
}
