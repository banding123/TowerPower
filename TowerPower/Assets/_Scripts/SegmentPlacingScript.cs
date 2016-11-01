using UnityEngine;
using System.Collections;

public class SegmentPlacingScript : MonoBehaviour {

	// the reference the tower must draw for a member
	public GameObject segmentPrefab;
	// the beginning position of the Prefab
	private Vector3 startDragPosition;
	// whether the mouse is being dragged
	private bool draging = false;
	// segment that will be drawn
	private GameObject segment = null;
	// whether the app is in adding mode
	public bool isAddingMode = true;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isAddingMode) {
			if (draging) {
				DisplayMember ();
			}
		} else {
			//nothing :D
		}
	}
	// draws the member
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
	// called when the mouse is clicked
	void OnMouseDown()
	{
		if (!isAddingMode) {
			Destroy (this.gameObject);
			return;
		}
		if (!draging) {
			segment = Instantiate (segmentPrefab);
			Vector3 mousePositionOnWord = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			segment.transform.position = new Vector3 (
				mousePositionOnWord.x,
				mousePositionOnWord.y,
				this.gameObject.transform.position.z
			);
			startDragPosition = segment.transform.position;
			draging = true;
		} 

	}
	// called when the mouse is not being clicked
	void OnMouseUp(){
		if (!isAddingMode)
			return;
		draging = false;
		FixedJoint2D j = segment.GetComponent<FixedJoint2D> ();
		j.connectedBody = this.GetComponent<Rigidbody2D> ();
	}

}
