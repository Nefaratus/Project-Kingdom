using UnityEngine;
using System.Collections;

public class TransferOutside : MonoBehaviour {

	GameObject[] transferPoint;
	GameObject target;
	Vector3 offset = new Vector3(0,0,-3);
	Vector3 rotation = new Vector3(0,180,0);
	GameObject Player;
	bool showPoints;
	
	// Use this for initialization
	void Start () 
	{
		Player = GameObject.FindGameObjectWithTag ("Player"); 
		transferPoint = GameObject.FindGameObjectsWithTag ("Outside");			
	}
	
	void ShowPoints()
	{
		showPoints = !showPoints;
	}
		
	void OnTriggerEnter(Collider col)
	{
		Player = col.gameObject;
		showPoints = true;
	}

	void OnTriggerExit(Collider col)
	{		
		showPoints = false;
	}
	void OnGUI()
	{
		if(showPoints)
		{
			GUI.BeginGroup (new Rect (Screen.width/2,Screen.height/2,Screen.width/10,Screen.width/10), "");
			foreach (GameObject point in transferPoint) 
			{		
				if(GUILayout.Button(point.transform.root.gameObject.name))
				{
					Player.transform.position = point.transform.position + offset;
					Player.transform.Rotate (rotation);
				}
			}
			GUI.EndGroup();
		}
	}
}
