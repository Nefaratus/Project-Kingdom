using UnityEngine;
using System.Collections;

public class ActivateEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter()
	{
		gameObject.GetComponent<AdvanceMovement> ().enabled = true;
		gameObject.GetComponent<EnemyAI> ().enabled = true;
	}

	void OnTriggerExit()
	{
		gameObject.GetComponent<AdvanceMovement> ().enabled = false;
		gameObject.GetComponent<EnemyAI> ().enabled = false;
	}
}
