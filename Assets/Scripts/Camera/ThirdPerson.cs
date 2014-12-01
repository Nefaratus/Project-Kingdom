using UnityEngine;
using System.Collections;

public class ThirdPerson : MonoBehaviour {
	
	public float smooth = 0.3f;
	public float maxdistance = float.PositiveInfinity;
	private bool isDragging;
	private Vector3 pos_one;
	private Vector3 pos_two;
	private float rotation;
	Transform standardPos;
	Transform rotatedPos;
	
	
	// Use this for initialization
	void Start () {
		//transform.parent.gameObject.layer = 5;
		standardPos = GameObject.Find ("CamPos").transform;
		rotatedPos =standardPos;
	}

	
	void FixedUpdate()
	{
		//var allButTriggers = ~(1 << 31);
		Debug.DrawLine(transform.parent.gameObject.transform.position+new Vector3(0.0f,1.0f,0.0f), rotatedPos.position, Color.yellow);
		RaycastHit hit;
		if(Physics.Linecast(transform.parent.gameObject.transform.position+new Vector3(0.0f,1.0f,0.0f), rotatedPos.position, out hit/*, allButTriggers*/)){
			transform.position = Vector3.Lerp(transform.position, hit.point, Time.deltaTime * smooth);
			transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.deltaTime * smooth);
			//Debug.Log(hit.collider.name);
		} else {
			transform.position = Vector3.Lerp(transform.position, rotatedPos.position, Time.deltaTime * smooth);
			transform.forward = Vector3.Lerp(transform.forward, rotatedPos.forward, Time.deltaTime * smooth);
		}

		if (isDragging) {
			if (Input.GetMouseButton (2)) {
				pos_two = Input.mousePosition;
				
				rotation = (pos_one - pos_two).x * 10.0f;
				
				pos_one = Input.mousePosition;
			} else {
				isDragging = false;
				rotation = 0.0f;
			}
		} else {
			if (Input.GetMouseButton (2)) {
				isDragging = true;
				
				pos_one = Input.mousePosition;
			}
		}

		
		rotatedPos.RotateAround(transform.parent.gameObject.transform.position, Vector3.up, this.rotation * Time.deltaTime);
	}
	
	
}
