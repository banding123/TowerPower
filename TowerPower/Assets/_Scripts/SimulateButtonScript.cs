using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimulateButtonScript : MonoBehaviour {

	// the weight oof the tower
	public GameObject weight;
	// the button that switches between deleting mode and adding mode
	public Button btn;
	// array that holds all the segment of the tower
	public SimulationScript[] allSegments;

	// Use this for initialization
	void Start () {
		btn = GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnListener);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//creates the weight of the tower
	void TaskOnListener(){
		
		allSegments = FindObjectsOfType<SimulationScript> ();
		if (allSegments.Length == 0)
			return;



		//finds that longest segment
		//========BROKEN :(===========//
		GameObject max = allSegments [0].gameObject; //errors if no segments

		for (int i = 0; i < allSegments.Length; i++) {
			
			if (getHighestY(max) < getHighestY (allSegments [i].gameObject)) {
				max = allSegments [i].gameObject;
			}
		


			allSegments [i].b_simulating = !allSegments [i].b_simulating;
		}

		Debug.Log (max);

		//create weight yeah;

	}


	// finds the highest y coordinate
	double getHighestY(GameObject obj)
	{
		float angle =  obj.transform.eulerAngles.z;
		return Mathf.Abs(
				(obj.transform.localScale.x - 0.03f)/0.02f*Mathf.Sin(angle)/2f
			) + obj.transform.position.y;
	}
}
