using UnityEngine;
using System.Collections;

public class TransferOutside : MonoBehaviour {

	GameObject[] transferPoint;
	GameObject target;
	Vector3 offset = new Vector3(0,0,-3);
	
	// Use this for initialization
	void Start () {
		transferPoint = GameObject.FindGameObjectsWithTag ("Outside");
		
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
	}
	
	void TeleportPlayer()
	{
		
	}
}
