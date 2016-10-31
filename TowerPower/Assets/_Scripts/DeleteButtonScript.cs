using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeleteButtonScript : MonoBehaviour {

	// the weight of the tower
	public GameObject weight;
	// the button that turns on delete mode
	public Button btn;
	// an array that holds all the segments
	public SegmentPlacingScript[] allSegments;

	// Use this for initialization
	void Start () {
		btn = GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnListener);

	}

	// whenever the button is pressed, it moves into delete mode
	void TaskOnListener(){

		allSegments = FindObjectsOfType<SegmentPlacingScript> ();
		if (allSegments.Length == 0)
			return;
		
		for (int i = 0; i < allSegments.Length; i++) {
			allSegments [i].isAddingMode = false;
		}

	
	}
}
