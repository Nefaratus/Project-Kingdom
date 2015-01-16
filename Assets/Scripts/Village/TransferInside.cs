using UnityEngine;
using System.Collections;

public class TransferInside : MonoBehaviour {

	GameObject[] transferPoint;
	GameObject target;
	Vector3 offset = new Vector3(0,0,-3);
	Vector3 rotation = new Vector3(0,180,0);

	// Use this for initialization
	void Start () {
		transferPoint = GameObject.FindGameObjectsWithTag ("Inside");
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject house in transferPoint) {
			if(gameObject.name == house.gameObject.name)
			{
				target = house;
			}
		}
	
	}

	void OnTriggerEnter(Collider col)
	{
		col.gameObject.transform.position = target.gameObject.transform.position + offset;
		col.gameObject.transform.Rotate (rotation);
	}

	void TeleportPlayer()
	{

	}
}
