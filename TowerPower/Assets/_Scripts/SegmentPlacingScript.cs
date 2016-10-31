using UnityEngine;
using System.Collections;

public class SegmentPlacingScript : MonoBehaviour {

	//refernce that the object needs to make
	public GameObject segmentPrefab;
	//the beginning of the of the reference
	private Vector3 startDragPosition;
	//boolean that holds whether a piece is being expanded
	private bool dragging = false;
	//the private segment
	private GameObject segment = null;
	//boolean that holds if the app is in adding mode
	public bool isAddingMode = true;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isAddingMode) {
			if (dragging) {
				DisplayMember ();
			}
		} else {
			//nothing :D
		}
	}

	// displays the placed member
	void DisplayMember()
	{

		Vector3 mousePositionOnWord = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		//angle
		float dy = mousePositionOnWord.y - startDragPosition.y;
		float dx = mousePositionOnWord.x - startDragPosition.x;

		float angy = dx ==0? (dy >0? 90: 270): Mathf.Atan (dy / dx);
		if (angy < 0) {
			angy =angy + Mathf.PI;
		}
		if (dy < 0) {
			angy = angy + Mathf.PI;
		}
		angy *= Mathf.Rad2Deg;
		Vector3 angry = new Vector3 (
			segment.transform.eulerAngles.x,
			segment.transform.eulerAngles.y,
			angy
		);
		segment.transform.eulerAngles = angry;


		//length of segement
		segment.transform.localScale = new Vector3 (
			Vector2.Distance(
				new Vector2(mousePositionOnWord.x, mousePositionOnWord.y),
				new Vector2 (startDragPosition.x, startDragPosition.y)
			)*0.2f + 0.03f,
			segment.transform.localScale.y,
			segment.transform.localScale.z
		);


		//center of segment
		segment.transform.position = new Vector3 (
			((mousePositionOnWord.x + startDragPosition.x)/2),
			((mousePositionOnWord.y + startDragPosition.y)/2),
			((mousePositionOnWord.z + startDragPosition.z)/2)
		);
	}

	//call when the mouse is being clicked
	void OnMouseDown()
	{
		if (!isAddingMode) {
			Destroy (this.gameObject);
			return;
		}
		if (!dragging) {
			segment = Instantiate (segmentPrefab);
			Vector3 mousePositionOnWord = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			segment.transform.position = new Vector3 (
				mousePositionOnWord.x,
				mousePositionOnWord.y,
				this.gameObject.transform.position.z
			);
			startDragPosition = segment.transform.position;
			dragging = true;
		} 

	}
	// called when the mouse isn't being clicked
	void OnMouseUp(){
		if (!isAddingMode)
			return;
		dragging = false;
		FixedJoint2D j = segment.GetComponent<FixedJoint2D> ();
		j.connectedBody = this.GetComponent<Rigidbody2D> ();
	}

}
