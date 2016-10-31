using UnityEngine;
using System.Collections;

public class SimulationScript : MonoBehaviour {

	// boolean that holds if the tower is being simulated
	public bool b_simulating = false;
	// if the simulaion is set up
	public bool b_initedSimulation = false;
	// original poistion before the simulation
	private Transform originalTransform;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (b_simulating) {
			if (!b_initedSimulation) {
				originalTransform = this.transform;
				GetComponent<Rigidbody2D> ().isKinematic = true;
				b_initedSimulation = true;
			}

		} else {
			if (b_initedSimulation) {
				if (originalTransform != null) {
					this.transform.position = originalTransform.position;
					this.transform.eulerAngles = originalTransform.eulerAngles;
					this.transform.localScale = originalTransform.localScale;
				}

				GetComponent<Rigidbody2D> ().isKinematic = false;
				b_initedSimulation = false;
			}
		
		}
	}
}
