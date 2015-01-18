using UnityEngine;
using System.Collections;

public class Collide : MonoBehaviour {

	public GameObject enemies;

	void OnTriggerEnter(Collider col)
	{
		col.GetComponent<Destination> ().destination = gameObject.name;
		if (enemies != null) 
		{
			enemies.SetActive (true);
		}
	}

	void OnTriggerExit(Collider col)
	{
		col.GetComponent<Destination> ().destination = null;
		if (enemies != null) 
		{
			enemies.SetActive (false);
		}
	}
}
